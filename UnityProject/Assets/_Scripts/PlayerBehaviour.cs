using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour
{
	public GameObject leftPaddle = null, rightPaddle = null, levelManager = null;
	public bool paddleActive = false, stunned = false;
	public float strafeSpeed = 0.1f;
	public float sixty = 60.0f, eighty = 80.0f, onefourty = 140.0f;
	public Vector3 targetPosition = new Vector3(0.0f, -2.5f);
	private List<GameObject> _collisionList = new List<GameObject> ();
	private float _currentSpeed, _lerpTime = 0.0f;
	private float _horizontalTouch = 0.0f, _touchTime = 0.0f;
	private ControlType _usedControls = ControlType.keyboard;
	private bool _tappedPaddle = false, _dragging = false, _startLevel = true, _endLevel = false;
	private Vector2 _touchPosition = Vector2.zero, _touchStartPosition = Vector2.zero;
	private Vector3 _playerPosition = Vector3.zero;

	// Use this for initialization
	void Start ()
	{
		sixty = (60.0f / 640.0f) * Screen.height;
		eighty = (80.0f / 400.0f) * Screen.width;
		onefourty = (140.0f / 640.0f) * Screen.height;

		leftPaddle.SetActive (false);
		rightPaddle.SetActive (false);
		_playerPosition = transform.position;
		levelManager = GameObject.Find ("Level");

		_usedControls = Statics.selectedControlMethod;
	}
	
	// Update is called once per frame
	void Update ()
	{
		_currentSpeed = strafeSpeed * Time.deltaTime;
		float deltaPosition;
		if (((Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift) && _usedControls == ControlType.keyboard) || _usedControls != ControlType.keyboard) && !stunned)
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

		//Absolutely massive else-if block with all the control methods and the level end movement
		if (_endLevel)
		{
			transform.position += new Vector3 (Mathf.Clamp (_touchPosition.x - transform.position.x, -_currentSpeed, _currentSpeed),
			                                   Mathf.Clamp (_touchPosition.y - transform.position.y, -_currentSpeed, _currentSpeed));
		} else if (_startLevel)
		{
			_lerpTime += Time.deltaTime;
			transform.position = Vector3.Lerp(_playerPosition, targetPosition, Mathf.SmoothStep(0.0f, 1.0f, _lerpTime));
			if (_lerpTime >= 1.0f)
			{
				_startLevel = false;
				_lerpTime = 0.0f;
			}
		} else if (_usedControls == ControlType.keyboard)
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
			deltaPosition = 0.0f;
			deltaPosition = Mathf.Clamp (Input.acceleration.x / 4.2f, -_currentSpeed, _currentSpeed);
			transform.position += new Vector3 (deltaPosition, 0.0f);
			if (Input.touchCount != 0)
			{
				if (!_tappedPaddle)
				{
					_tappedPaddle = true;
					if (((Input.touches [0].position.x) / (Screen.width / 5.4f) - 2.7f) < 0.0f)
					{
						PaddleActivate (leftPaddle);
					} else
					{
						PaddleActivate (rightPaddle);
					}
				}
			} else
			{
				_tappedPaddle = false;
			}
		} else if (_usedControls == ControlType.dragging)
		{
			deltaPosition = 0.0f;
			bool isPaddling = false;
			if (Input.touchCount != 0)
			{
				if ((Input.touches [0].position.y) / (Screen.height / 10.0f) - 5.0f < -2.0f)
				{
					_horizontalTouch = (Input.touches [0].position.x) / (Screen.width / 5.4f) - 2.7f;
				} else if (!_tappedPaddle)
				{
					_tappedPaddle = true;
					isPaddling = true;
					AutomaticPaddle ();
				}

				if (Input.touchCount > 1)
				{
					if ((Input.touches [1].position.y) / (Screen.height / 10.0f) - 5.0f < -2.0f && isPaddling)
					{
						_horizontalTouch = (Input.touches [0].position.x) / (Screen.width / 5.4f) - 2.7f;
					} else if (!_tappedPaddle)
					{
						_tappedPaddle = true;
						AutomaticPaddle ();
					}
				} else if (!isPaddling)
				{
					_tappedPaddle = false;
				}
			}

			deltaPosition = _horizontalTouch - transform.position.x;
			deltaPosition = Mathf.Clamp (deltaPosition, -_currentSpeed, _currentSpeed);
			transform.position += new Vector3 (deltaPosition, 0.0f);
		} else if (_usedControls == ControlType.oppositedragging)
		{
			deltaPosition = 0.0f;
			bool isPaddling = false;
			if (Input.touchCount != 0)
			{
				if ((Input.touches [0].position.y) / (Screen.height / 10.0f) - 5.0f > -2.0f)
				{
					_horizontalTouch = (Input.touches [0].position.x) / (Screen.width / 5.4f) - 2.7f;
				} else if (!_tappedPaddle)
				{
					_tappedPaddle = true;
					isPaddling = true;
					AutomaticPaddle ();
				}
				
				if (Input.touchCount > 1)
				{
					if ((Input.touches [1].position.y) / (Screen.height / 10.0f) - 5.0f > -2.0f && isPaddling)
					{
						_horizontalTouch = (Input.touches [0].position.x) / (Screen.width / 5.4f) - 2.7f;
					} else if (!_tappedPaddle)
					{
						_tappedPaddle = true;
						AutomaticPaddle ();
					}
				} else if (!isPaddling)
				{
					_tappedPaddle = false;
				}
			}			
			
			deltaPosition = _horizontalTouch - transform.position.x;
			deltaPosition = Mathf.Clamp (deltaPosition, -_currentSpeed, _currentSpeed);
			transform.position += new Vector3 (deltaPosition, 0.0f);
		} else if (_usedControls == ControlType.freedragging)
		{
			deltaPosition = 0.0f;
			bool isPaddling = false;
			if (Input.touchCount != 0)
			{
				_touchTime += Time.deltaTime;
				if (!_dragging)
				{
					_dragging = true;
					_touchStartPosition = new Vector2((Input.touches [0].position.x / (Screen.width / 5.4f)), (Input.touches [0].position.y / (Screen.height / 10.0f))) + new Vector2 (-2.7f, -5.0f);
					_playerPosition = transform.position;
				}
				_touchPosition = new Vector2((Input.touches [0].position.x / (Screen.width / 5.4f)), (Input.touches [0].position.y / (Screen.height / 10.0f))) + new Vector2 (-2.7f, -5.0f);
				if (Input.touchCount > 1)
				{
					if (!_tappedPaddle)
					{
						AutomaticPaddle ();
						_tappedPaddle = true;
					}
				} else
				{
					_tappedPaddle = false;
				}
			} else if (_dragging)
			{
				if (_touchTime < 0.18f)
				{
					AutomaticPaddle ();
				}
				_touchTime = 0.0f;
				_dragging = false;
			}

			deltaPosition = (_touchPosition.x - _touchStartPosition.x) - (transform.position.x - _playerPosition.x);
			deltaPosition = Mathf.Clamp (deltaPosition, -_currentSpeed, _currentSpeed);
			transform.position += new Vector3 (deltaPosition, 0.0f);
		}
		if (!_endLevel && !_startLevel)
		{
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.7f, 2.7f), -2.5f);
		}


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

	//Automatically determines the best paddle to use
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

	public void StartTheEnd()
	{
		_endLevel = true;
		_touchPosition = new Vector3 (1.5f, transform.position.y);
		StartCoroutine (WalkOff ());
	}

	IEnumerator WalkOff()
	{
		yield return new WaitForSeconds (1.5f);
		_touchPosition = new Vector3 (1.5f, 8.0f);
	}

	void OnGUI ()
	{
		if (_usedControls != ControlType.keyboard)
		{
			if (_usedControls == ControlType.touchpad || _usedControls == ControlType.invertedtouchpad)
			{
				if (GUI.RepeatButton (new Rect (0, Screen.height - sixty, eighty, sixty), "<----", Statics.menuButtonStyle))
				{
					transform.position -= new Vector3 (_currentSpeed, 0.0f);
				}
				if (GUI.RepeatButton (new Rect (Screen.width - eighty, Screen.height - sixty, eighty, sixty), "---->", Statics.menuButtonStyle))
				{
					transform.position += new Vector3 (_currentSpeed, 0.0f);
				}
			}

			if (_usedControls == ControlType.touchpad)
			{
				if (GUI.RepeatButton (new Rect (0, Screen.height - onefourty, eighty, sixty), "Vasen", Statics.menuButtonStyle))
				{
					PaddleActivate (leftPaddle);
				}
				if (GUI.RepeatButton (new Rect (Screen.width - eighty, Screen.height - onefourty, eighty, sixty), "Oikea", Statics.menuButtonStyle))
				{
					PaddleActivate (rightPaddle);
				}
			}
			if (_usedControls == ControlType.invertedtouchpad)
			{
				if (GUI.RepeatButton (new Rect (Screen.width - eighty, Screen.height - onefourty, eighty, sixty), "Vasen", Statics.menuButtonStyle))
				{
					PaddleActivate (leftPaddle);
				}
				if (GUI.RepeatButton (new Rect (0, Screen.height - onefourty, eighty, sixty), "Oikea", Statics.menuButtonStyle))
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
			collision.gameObject.GetComponent<BallBehaviour> ().BallDestroy ();
		}
		if (collision.gameObject.CompareTag ("Enemy"))
		{
			stunned = true;
			StartCoroutine (StunRemove (5.0f));
		}
	}
}
