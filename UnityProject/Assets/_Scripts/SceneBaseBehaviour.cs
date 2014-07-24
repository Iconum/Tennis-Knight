using UnityEngine;
using System.Collections;

public class SceneBaseBehaviour : MonoBehaviour {
	public static SceneBaseBehaviour curSceneBase = null;
	public static bool hasLoadedPrefs = false;
	public string prevSceneName = "";
	public AsyncOperation _asyncOp = null;
	public bool loadPrefs = false;
	public bool quit = false;

	void Start()
	{
		if (loadPrefs && !hasLoadedPrefs)
		{
			Statics.PrefLoading ();
			hasLoadedPrefs = true;
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
			_asyncOp = Application.LoadLevelAsync (prevSceneName);
			if (audio)
			{
				_asyncOp.allowSceneActivation = false;
				audio.Play ();
			}
		} else if (Input.GetKeyDown (KeyCode.Escape) && prevSceneName == "" && quit)
		{
			Statics.PrefStoring ();
			Application.Quit ();
		}
		if (_asyncOp != null && audio)
		if (!audio.isPlaying)
			_asyncOp.allowSceneActivation = true;
	}
}
