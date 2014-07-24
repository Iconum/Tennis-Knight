using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreditsBehaviour : MonoBehaviour {
	public List<string> creditList = null;
	public float scrollingRate = 2.0f, creditSeperation = 20.0f, scrollingState = 0.0f;

	protected float _scrollingActualSpeed = 2.0f, _creditSeperationActual = 20.0f, _scrollingStateActual;

	public GameObject oldMusic;

	protected void Start ()
	{
		_scrollingActualSpeed = scrollingRate;
		_creditSeperationActual = creditSeperation;
		_scrollingStateActual = scrollingState;

		oldMusic = GameObject.Find("MenuMusic");
		Destroy (oldMusic);
	}

	protected void Update ()
	{
		_scrollingStateActual += _scrollingActualSpeed * Time.deltaTime;

		if (Input.touchCount > 0)
		{
			Application.LoadLevel ("Menu");
		}
	}

	protected void OnGUI()
	{
		Statics.StyleInitialization ();
		Vector2 startvec = GUIUtility.ScreenToGUIPoint (Camera.main.WorldToScreenPoint (new Vector3 (-2.5f, 3.0f))),
		endvec = GUIUtility.ScreenToGUIPoint (Camera.main.WorldToScreenPoint (new Vector3 (2.5f, 1.0f - 1.5f * creditList.Count)));
		Rect guiRect = new Rect (startvec.x, startvec.y - _scrollingStateActual, endvec.x - startvec.x, startvec.y - endvec.y);
		GUILayout.BeginArea (guiRect);
		{
			for (int i = 0; i < creditList.Count; ++i)
			{
				GUILayout.Label (creditList [i], Statics.creditsTextStyle);
				GUILayout.Space (creditSeperation);
			}
		}
		GUILayout.EndArea ();
		if (guiRect.y + guiRect.height < 0.0f)
		{
			_scrollingStateActual = scrollingState;
		}
	}
}