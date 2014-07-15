using UnityEngine;
using System.Collections;

public class SceneBaseBehaviour : MonoBehaviour {
	public static SceneBaseBehaviour curSceneBase = null;
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

	void Awake()
	{
		curSceneBase = this;
	}

	void OnApplicationPause(bool paused)
	{
		if (paused)
		{
			Statics.PrefStoring ();
		} else
		{
			
		}
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && prevSceneName != "" && _asyncOp == null)
		{
			_asyncOp = Application.LoadLevelAsync(prevSceneName);
			if (audio)
			{
				_asyncOp.allowSceneActivation = false;
				audio.Play();
			}
		}
		if (_asyncOp != null && audio)
		if (!audio.isPlaying)
			_asyncOp.allowSceneActivation = true;
	}
}
