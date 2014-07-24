using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour
{
	public GameObject leftPaddle = null, rightPaddle = null, levelManager = null,visualRacket=null;
	public bool paddleActive = false, isPaused = false;
	public float strafeSpeed = 0.1f, heatLimit = 25.0f, runawayRate = 1.0f, walkOffSpeed = 4.0f;
	public float sixty = 60.0f, eighty = 80.0f, onefourty = 140.0f;
	public Vector3 targetPosition = new Vector3(0.0f, -2.5f);

	protected List<GameObject> _collisionList = new List<GameObject> ();
	protected float _currentSpeed, _lerpTime = 0.0f, _heat = 0.0f, _runawaySpeed = 0.0f;
	protected float _horizontalTouch = 0.0f, _touchTime = 0.0f;
	protected ControlType _usedControls = ControlType.keyboard;
	protected bool _tappedPaddle = false, _dragging = false, _startLevel = true, _endLevel = false, _gameOver = false;
	protected Vector2 _touchPosition = Vector2.zero, _touchStartPosition = Vector2.zero;
	protected Vector3 _playerPosition = Vector3.zero;

	protected Animator anim;

	public ParticleSystem swordTrailPrefab = null;
	public ParticleSystem swordTrail = null;
	public List<AudioClip> sounds = new List<AudioClip> ();


	// Use this for initialization
	protected virtual void Start ()
	{
		sixty = (60.0f / 640.0f) * Screen.height;
		eighty = (80.0f / 400.0f) * Screen.width;
		onefourty = (140.0f / 640.0f) * Screen.height;
		
		_playerPosition = transform.position;
		levelManager = GameObject.Find ("Level");

		_usedControls = Statics.selectedControlMethod;
		if (audio)
		audio.volume = Statics.soundVolume;

		anim = GetComponent<Animator> ();
	}

	protected void OnApplicationPause(bool paused)
	{
		if (paused)
		{
			Statics.PrefStoring ();
		} else
		{

		}
	}
	
	// Update is called once per frame
	protected virtual void Update ()
	{
		anim.SetBool("MovingLeft",false);
		anim.SetBool("MovingRight",false);

		_currentSpeed = strafeSpeed * Time.deltaTime;
		float deltaPosition;
		if (((Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) && _usedControls == ControlType.keyboard) || _usedControls != ControlType.keyboard)
		{
			_currentSpeed *= 2;
		}

		if (_heat > 0.0f)
		{
			_heat -= Time.deltaTime * 4.0f;
			if (_heat < 0.0f)
			{
				_heat = 0.0f;
			}
			if (_heat > heatLimit)
			{
				_heat = heatLimit;
			}
			_currentSpeed /= (1.0f + (_heat / (heatLimit / 4.0f)));
		}

		GetComponent<SpriteRenderer>().color = new Color(1f,1f - _heat/heatLimit,1f - _heat/heatLimit);

		//Absolutely massive else-if block with all the control methods and the level transition movement
		if (_gameOver)
		{
			_runawaySpeed += Time.deltaTime / runawayRate;
			transform.position += new Vector3 (0.0f, -_runawaySpeed);
		} else if (isPaused)
		{

		} else if (_endLevel)
		{
			_heat = 0.0f;
			transform.position += new Vector3 (Mathf.Clamp (_touchPosition.x - transform.position.x, -walkOffSpeed * Time.deltaTime, walkOffSpeed * Time.deltaTime),
			                                   Mathf.Clamp (_touchPosition.y - transform.position.y, -walkOffSpeed * Time.deltaTime, walkOffSpeed * Time.deltaTime));
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
				anim.SetBool("MovingRight",true);
			}
			if (Input.GetKey (KeyCode.LeftArrow))
			{
				transform.position -= new Vector3 (_currentSpeed, 0.0f);
				anim.SetBool("MovingLeft",true);
			}
			if (Input.GetKeyDown (KeyCode.Z))
			{
				PaddleActivate (leftPaddle);
				anim.SetTrigger ("LeftSwing");

			}
			if (Input.GetKeyDown (KeyCode.X))
			{
				PaddleActivate (rightPaddle);
				anim.SetTrigger ("RightSwing");


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
						anim.SetTrigger ("LeftSwing");
					} else
					{
						PaddleActivate (rightPaddle);
						anim.SetTrigger ("RightSwing");
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
		if (!_endLevel && !_startLevel && !_gameOver)
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

	protected virtual void PaddleActivate (GameObject paddle)
	{
		if (!paddleActive)
		{
			paddleActive = true;
			paddle.SetActive (true);
			paddle.GetComponent<PaddleBehaviour> ().PaddleHit ();
			visualRacket.SetActive(false);
			swordTrail = (ParticleSystem)Instantiate(swordTrailPrefab, paddle.transform.position, paddle.transform.rotation);
			if (sounds.Count > 0 && audio)
			{
				audio.clip = sounds [2];
				audio.pitch = Random.Range (0.9f, 1.2f);
				audio.Play ();
			}
		}
	}

	//Automatically determines the best paddle to use
	protected virtual void AutomaticPaddle ()
	{
		GameObject tempo = levelManager.GetComponent<LevelBehaviour> ().FindClosestDeflectable (transform.position);
		if (tempo != null)
		{
			if (IntersectionPoint (new Vector2 (tempo.transform.position.x, tempo.transform.position.y), tempo.rigidbody2D.velocity).x < transform.position.x)
			{
				PaddleActivate (leftPaddle);
				anim.SetTrigger ("LeftSwing");

			} else
			{
				PaddleActivate (rightPaddle);
				anim.SetTrigger ("RightSwing");

			}
		} else
		{
			PaddleActivate (rightPaddle);
			anim.SetTrigger ("RightSwing");

		}
		
	}
	protected Vector2 IntersectionPoint (Vector2 pos, Vector2 vel)
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

	public void GameOver()
	{
		_gameOver = true;
	}

	public void SetPause(bool paused)
	{
		isPaused = paused;
		_usedControls = Statics.selectedControlMethod;
	}

	protected virtual void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Deflectable") || collision.gameObject.CompareTag("AllDamaging"))
		{
			_heat += collision.gameObject.GetComponent<BallBehaviour> ().heatGeneration;
			if (_heat > heatLimit)
				_heat = heatLimit;
			collision.gameObject.GetComponent<BallBehaviour> ().BallDestroy ();
			if (sounds.Count > 0 && audio)
			{
				audio.clip = sounds [0];
				audio.pitch = Random.Range (0.9f, 1.2f);
				audio.Play ();
			}
		}
		if (collision.gameObject.CompareTag ("Enemy"))
		{
			_heat = heatLimit;
			if (sounds.Count > 0 && audio)
			{
				audio.clip = sounds [1];
				audio.pitch = Random.Range (0.9f, 1.2f);
				audio.Play ();
			}
		}
	}

	protected virtual void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Deflectable"))
		{
			_heat += other.GetComponent<BallBehaviour> ().heatGeneration;
			if (_heat > heatLimit)
				_heat = heatLimit;
		}
		if (other.CompareTag ("Enemy"))
		{
			_heat = heatLimit;
		}
	}
}
