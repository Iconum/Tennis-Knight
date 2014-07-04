using UnityEngine;
using System.Collections;

public class ButtonFunctionality : MonoBehaviour {
	
	protected virtual void Start () {

	}

	protected virtual void Update () {
		Vector2 inputPos = Vector2.zero;
		bool hasInput = false;
		if (Input.touchCount > 0)
		{
			if (Input.touches[0].phase == TouchPhase.Ended)
			{
				inputPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
				hasInput = true;
			}
		}
#if UNITY_STANDALONE
		if (Input.GetMouseButtonUp (0))
		{
			inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			hasInput = true;
		}
#endif
		if (hasInput)
		{
			if (GetComponent<BoxCollider2D> ().OverlapPoint (new Vector2 (inputPos.x, inputPos.y)))
			{
				PressButton ();
			}
		}
	}

	protected virtual void PressButton()
	{

	}
}
