using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour
{
	public GameObject leftPaddle = null, rightPaddle = null, levelManager = null;
	public bool paddleActive = false, stunned = false;
	public float strafeSpeed = 0.1f;
	public float sixty = 60.0f, eighty = 80.0f, onefourty = 140.0f;
	private List<GameObject> _collisionList = new List<GameObject> ();
	private float _currentSpeed;
	private float _touchPosition = 0.0f;
	private ControlType _usedControls = ControlType.keyboard;
	private bool tappedPaddle = false;

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
			Application.LoadLevel (0);
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
			if (Input.GetKeyDown (KeyCode.Space))
			{
				AutomaticPaddle ();
			}
		} else if (_usedControls == ControlType.tilting)
		{
			float deltaPosition = 0.0f;
			deltaPosition = Mathf.Clamp (Input.acceleration.x / 4.2f, -_currentSpeed, _currentSpeed);
			transform.position += new Vector3 (deltaPosition, 0.0f);
			if (Input.touchCount != 0 || Input.GetMouseButton (0))
			{
				if (!tappedPaddle)
				{
					tappedPaddle = true;
					AutomaticPaddle ();
				}
			} else
			{
				tappedPaddle = false;
			}
		} else if (_usedControls == ControlType.dragging)
		{
			float deltaPosition = 0.0f;
			bool isPaddling = false;
			if (Input.touchCount != 0)
			{
				if ((Input.touches [0].position.y) / (Screen.width / 10.0f) - 5.0f < -2.0f)
				{
					_touchPosition = (Input.touches [0].position.x) / (Screen.width / 5.4f) - 2.7f;
				} else if (!tappedPaddle)
				{
					tappedPaddle = true;
					isPaddling = true;
					AutomaticPaddle ();
				}

				if (Input.touchCount > 1)
				{
					if ((Input.touches [1].position.y) / (Screen.width / 10.0f) - 5.0f < -2.0f && isPaddling)
					{
						_touchPosition = (Input.touches [0].position.x) / (Screen.width / 5.4f) - 2.7f;
					} else if (!tappedPaddle)
					{
						tappedPaddle = true;
						AutomaticPaddle ();
					}
				} else if (!isPaddling)
				{
					tappedPaddle = false;
				}
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
			paddle.GetComponent<PaddleBehaviour> ().PaddleHit ();
		}
	}

	IEnumerator StunRemove (float t)
	{
		yield return new WaitForSeconds (t);
		stunned = false;
	}

	void AutomaticPaddle ()
	{
		GameObject tempo = levelManager.GetComponent<LevelBehaviour> ().FindClosestDeflectable (transform.position);
		if (tempo != null)
		{
			if (IntersectionPoint (new Vector2 (tempo.transform.position.x, tempo.transform.position.y), tempo.rigidbody2D.velocity).x < transform.position.x)
			{
				PaddleActivate (leftPaddle);
			} else
			{
				PaddleActivate (rightPaddle);
			}
		} else
		{
			PaddleActivate (rightPaddle);
		}

	}

	Vector2 IntersectionPoint (Vector2 pos, Vector2 vel)
	{
		if (vel.y != 0.0f)
		{
			float tempf = (transform.position.y - pos.y) / vel.y;
			return new Vector2 (pos.x + tempf * vel.x, transform.position.y);
		} else
			return pos;
	}

	void OnGUI ()
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

			if (_usedControls == ControlType.touchpad)
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

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Deflectable"))
		{
			stunned = true;
			StartCoroutine (StunRemove (2.0f));
			collision.gameObject.GetComponent<BallBehaviour>().BallDestroy();
		}
		if (collision.gameObject.CompareTag ("Enemy"))
		{
			stunned = true;
			StartCoroutine (StunRemove (5.0f));
		}
	}
}
