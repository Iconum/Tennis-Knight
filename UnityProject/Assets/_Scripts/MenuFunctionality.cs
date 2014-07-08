using UnityEngine;
using System.Collections;

public class MenuFunctionality : MonoBehaviour {
	public string prevSceneName = "";
	public AsyncOperation _asyncOp = null;
	public bool loadPrefs = false;

	void Start()
	{
		if (loadPrefs)
		{
			Statics.PrefLoading ();
		}
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && prevSceneName != "" && _asyncOp != null)
		{
			_asyncOp = Application.LoadLevelAsync(prevSceneName);
			_asyncOp.allowSceneActivation = false;
			if (audio)
				audio.Play();
		}
		if (_asyncOp != null && audio)
		if (!audio.isPlaying)
			_asyncOp.allowSceneActivation = true;
	}
}
