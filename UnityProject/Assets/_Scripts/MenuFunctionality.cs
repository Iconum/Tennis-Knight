using UnityEngine;
using System.Collections;

public class MenuFunctionality : MonoBehaviour {
	float width, height;
	GUIStyle menuStyle;

	// Use this for initialization
	void Start () {
		width = Screen.width;
		height = Screen.height;
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
		menuStyle = new GUIStyle (GUI.skin.button);
		menuStyle.fontSize = (Mathf.FloorToInt(height) / 640) * 20;
	}

	void OnGUI()
	{
		StyleInitialization ();
		GUILayout.BeginArea (new Rect (50.0f, 50.0f, width - 100.0f, height - 100.0f));
		{
			if (GUILayout.Button("Start", menuStyle))
			{
				Application.LoadLevel("TestLevel");
			}
			if (GUILayout.Button("Keyboard Controls", menuStyle))
			{
				Statics.selectedControlMethod = ControlType.keyboard;
			}
			if (GUILayout.Button("Touchpad Controls", menuStyle))
			{
				Statics.selectedControlMethod = ControlType.touchpad;
			}
			if (GUILayout.Button("Inverted Controls", menuStyle))
			{
				Statics.selectedControlMethod = ControlType.invertedtouchpad;
			}
			if (GUILayout.Button("Tilting Controls", menuStyle))
			{
				Statics.selectedControlMethod = ControlType.tilting;
			}
		}
		GUILayout.EndArea ();
	}
}
