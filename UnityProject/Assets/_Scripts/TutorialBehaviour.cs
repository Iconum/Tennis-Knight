﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TutorialState
{
	Start,
	End,
	Move,
	Swing,
	Dodge,
	Villagers
}

[System.Serializable]
public class TutorialTexts
{
	public List<string> tutorialText = new List<string>();
	public int currentText = 0;

	public void DrawText()
	{
		Statics.StyleInitialization ();
		Vector3 startvec = Camera.main.WorldToScreenPoint (new Vector3 (-2.5f, 4.0f)), endvec = Camera.main.WorldToScreenPoint(new Vector3(2.5f, -2.0f));
		GUILayout.BeginArea (new Rect (startvec.x, Screen.height - startvec.y, endvec.x - startvec.x, Screen.height - endvec.y), Statics.menuTextStyle);
		{
			GUILayout.TextArea (tutorialText [currentText], Statics.menuTextStyle);
		}
		GUILayout.EndArea ();
	}

	public bool AdvanceText()
	{
		++currentText;
		if (currentText >= tutorialText.Count)
		{
			--currentText;
			return false;
		}
		return true;
	}
}

public class TutorialBehaviour : PlayerBehaviour {
	public TutorialState currentState = TutorialState.Start;
	public Dictionary<TutorialState, TutorialTexts> tutorialPhases = new Dictionary<TutorialState, TutorialTexts> ();
	public TutorialState[] tutoStates;
	public TutorialTexts[] tutoTexts;

	protected bool _tutorialOn = true;

	protected override void Start ()
	{
		for (int i = 0; i < tutoStates.Length && i < tutoTexts.Length; ++i)
		{
			tutorialPhases.Add (tutoStates [i], tutoTexts [i]);
		}
		base.Start ();
	}

	protected override void Update ()
	{
		_currentSpeed = strafeSpeed * Time.deltaTime;
		float deltaPosition;
		if (((Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) && _usedControls == ControlType.keyboard) || _usedControls != ControlType.keyboard)
		{
			_currentSpeed *= 2;
		}
		
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.LoadLevel (0);
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
		
		if (_endLevel)
		{
			_heat = 0.0f;
			transform.position += new Vector3 (Mathf.Clamp (_touchPosition.x - transform.position.x, -_currentSpeed, _currentSpeed),
			                                   Mathf.Clamp (_touchPosition.y - transform.position.y, -_currentSpeed, _currentSpeed));
		} else if (_startLevel)
		{
			_lerpTime += Time.deltaTime;
			transform.position = Vector3.Lerp (_playerPosition, targetPosition, Mathf.SmoothStep (0.0f, 1.0f, _lerpTime));
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
		if (!_endLevel && !_startLevel && !_tutorialOn)
		{
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.7f, 2.7f), -2.5f);
		}
	}

	protected void OnGUI()
	{
		if (_tutorialOn)
			tutorialPhases [currentState].DrawText ();
	}

	protected override void PaddleActivate (GameObject paddle)
	{
		if (_tutorialOn)
		{
			if (!tutorialPhases [currentState].AdvanceText ())
			{
				_tutorialOn = false;
			}
		} else
		{
			if (currentState == TutorialState.Move)
			{
				currentState = TutorialState.Swing;
				_tutorialOn = true;
			}
			base.PaddleActivate (paddle);
		}
	}
}
