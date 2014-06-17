using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour
{
	public GameObject leftPaddle = null, rightPaddle = null;
	public bool paddleActive = false, stunned = false;
	public float strafeSpeed = 0.1f;
	public float sixty = 60.0f, eighty = 80.0f, onefourty = 140.0f;

	private List<GameObject> _collisionList = new List<GameObject> ();
	private float _currentSpeed;
	private float _touchPosition = 0.0f, _touchTimer = 0.0f;
	private ControlType _usedControls = ControlType.keyboard;

	// Use this for initialization
	void Start ()
	{
		sixty = (60.0f / 640.0f) * Screen.height;
		eighty = (80.0f / 400.0f) * Screen.width;
		onefourty = (140.0f / 640.0f) * Screen.height;

		leftPaddle.SetActive (false);
		rightPaddle.SetActive (false);

		_usedControls = Statics.selectedControlMethod;
	}
	
	// Update is called once per frame
	void Update ()
	{
		_currentSpeed = strafeSpeed * Time.deltaTime;
		if ((Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift) || _usedControls == ControlType.tilting || _usedControls == ControlType.dragging) && !stunned)
		{
			_currentSpeed *= 2;
		} else if (stunned)
		{
			//_currentSpeed *= 0.2f;
		}

		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.LoadLevel(0);
		}

		if (_usedControls == ControlType.keyboard)
		{
			if (Input.GetKey (KeyCode.RightArrow))
			{
				transform.position += new Vector3 (_currentSpeed, 0.0f);
			}
			if (Input.GetKey (KeyCode.LeftArrow))
			{
				transform.position -= new Vector3 (_currentSpeed, 0.0f);
			}
			if (Input.GetKeyDown (KeyCode.Z))
			{
				PaddleActivate (leftPaddle);
			}
			if (Input.GetKeyDown (KeyCode.X))
			{
				PaddleActivate (rightPaddle);
			}
		} else if (_usedControls == ControlType.tilting)
		{
			float deltaPosition = 0.0f;
			deltaPosition = Mathf.Clamp (Input.acceleration.x / 4.2f, -_currentSpeed, _currentSpeed);
			transform.position += new Vector3 (deltaPosition, 0.0f);
		} else if (_usedControls == ControlType.dragging)
		{
			float deltaPosition = 0.0f;
			if (Input.touchCount != 0 || Input.GetMouseButton (0))
			{
				_touchTimer += Time.deltaTime;
			}
			else
			{
				if (_touchTimer > 0.0f && _touchTimer <= 0.1f)
				{
					AutomaticPaddle();
				}
				_touchTimer = 0.0f;
			}
			if (_touchTimer > 0.1f)
			{			
				_touchPosition = (Input.mousePosition.x) / (Screen.width / 5.4f) - 2.7f;
			}

			deltaPosition = _touchPosition - transform.position.x;
			deltaPosition = Mathf.Clamp (deltaPosition, -_currentSpeed, _currentSpeed);
			transform.position += new Vector3 (deltaPosition, 0.0f);
		}
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.7f, 2.7f), -2.5f);


		//Debug näppäimet
		if (Input.GetKeyDown (KeyCode.KeypadPlus))
		{
			Time.timeScale += 0.1f;
		}
		if (Input.GetKeyDown (KeyCode.KeypadMinus))
		{
			if (Time.timeScale > 0.1f)
				Time.timeScale -= 0.1f;
		}
	}

	void PaddleActivate (GameObject paddle)
	{
		if (!paddleActive)
		{
			paddleActive = true;
			paddle.SetActive (true);
			paddle.GetComponent<PaddleBehaviour>().PaddleHit();
		}
	}

	IEnumerator StunRemove()
	{
		yield return new WaitForSeconds (3.0f);
		stunned = false;
	}

	void AutomaticPaddle()
	{
		Debug.Log ("Derp Herp.");
	}

	void OnGUI()
	{
		if (_usedControls != ControlType.keyboard)
		{
			if (_usedControls == ControlType.touchpad || _usedControls == ControlType.invertedtouchpad)
			{
				if (GUI.RepeatButton (new Rect (0, Screen.height - sixty, eighty, sixty), "<----", Statics.menuStyle))
				{
					transform.position -= new Vector3 (_currentSpeed, 0.0f);
				}
				if (GUI.RepeatButton (new Rect (Screen.width - eighty, Screen.height - sixty, eighty, sixty), "---->", Statics.menuStyle))
				{
					transform.position += new Vector3 (_currentSpeed, 0.0f);
				}
			}

			if (_usedControls == ControlType.touchpad || _usedControls == ControlType.tilting)
			{
				if (GUI.RepeatButton (new Rect (0, Screen.height - onefourty, eighty, sixty), "Vasen", Statics.menuStyle))
				{
					PaddleActivate (leftPaddle);
				}
				if (GUI.RepeatButton (new Rect (Screen.width - eighty, Screen.height - onefourty, eighty, sixty), "Oikea", Statics.menuStyle))
				{
					PaddleActivate (rightPaddle);
				}
			}
			if (_usedControls == ControlType.invertedtouchpad)
			{
				if (GUI.RepeatButton (new Rect (Screen.width - eighty, Screen.height - onefourty, eighty, sixty), "Vasen", Statics.menuStyle))
				{
					PaddleActivate (leftPaddle);
				}
				if (GUI.RepeatButton (new Rect (0, Screen.height - onefourty, eighty, sixty), "Oikea", Statics.menuStyle))
				{
					PaddleActivate (rightPaddle);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Deflectable"))
		{
			stunned = true;
			StartCoroutine(StunRemove ());
			Destroy(collision.gameObject);
		}
	}
}
