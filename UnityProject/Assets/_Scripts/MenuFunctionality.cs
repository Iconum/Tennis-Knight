using UnityEngine;
using System.Collections;

public class MenuFunctionality : MonoBehaviour {
	float width, height;

	// Use this for initialization
	void Start () {
		width = Screen.width;
		height = Screen.height;
		if (PlayerPrefs.HasKey ("ControlMethod"))
		{
			Statics.selectedControlMethod = (ControlType)PlayerPrefs.GetInt ("ControlMethod");
		} else
		{
#if UNITY_ANDROID
			Statics.selectedControlMethod = ControlType.tilting;
#elif UNITY_EDITOR
			Statics.selectedControlMethod = ControlType.keyboard;
#elif UNITY_STANDALONE
			Statics.selectedControlMethod = ControlType.keyboard;
#endif
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void StyleInitialization()
	{
		Statics.menuStyle = new GUIStyle (GUI.skin.button);
		Statics.menuStyle.fontSize = (Mathf.FloorToInt(height) / 640) * 20;
	}

	void OnGUI()
	{
		StyleInitialization ();
		GUILayout.BeginArea (new Rect (50.0f, 50.0f, width - 100.0f, height - 100.0f));
		{
			if (GUILayout.Button("Start", Statics.menuStyle))
			{
				PlayerPrefs.SetInt("ControlMethod", (int)Statics.selectedControlMethod);
				Application.LoadLevel("TestLevel");
			}
			if (GUILayout.Button("Keyboard Controls", Statics.menuStyle))
			{
				Statics.selectedControlMethod = ControlType.keyboard;
			}
			if (GUILayout.Button("Touchpad Controls", Statics.menuStyle))
			{
				Statics.selectedControlMethod = ControlType.touchpad;
			}
			if (GUILayout.Button("Inverted Controls", Statics.menuStyle))
			{
				Statics.selectedControlMethod = ControlType.invertedtouchpad;
			}
			if (GUILayout.Button("Tilting Controls", Statics.menuStyle))
			{
				Statics.selectedControlMethod = ControlType.tilting;
			}
		}
		GUILayout.EndArea ();
	}
}
