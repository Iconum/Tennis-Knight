using UnityEngine;
using System.Collections;

public class ButtonFunctionality : MonoBehaviour {
	protected AsyncOperation _asyncOp = null;
	
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
		if (hasInput && _asyncOp == null)
		{
			if (GetComponent<BoxCollider2D> ().OverlapPoint (new Vector2 (inputPos.x, inputPos.y)))
			{
				PressButton ();
			}
		}
		if (_asyncOp != null && audio)
		if (!audio.isPlaying)
			_asyncOp.allowSceneActivation = true;
	}

	protected virtual void PressButton()
	{
		if (audio)
		{
			audio.Play();
			if (_asyncOp != null)
				_asyncOp.allowSceneActivation = false;
		}
	}
}
