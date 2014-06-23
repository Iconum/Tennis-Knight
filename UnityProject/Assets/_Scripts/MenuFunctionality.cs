using UnityEngine;
using System.Collections;

public class MenuFunctionality : MonoBehaviour {
	float width, height;
	string menuText;

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
		menuText = GetMenuText ();
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
		Statics.menuStyle.fontSize = (Mathf.FloorToInt(height) / 640) * 24;
		GUI.skin.box.wordWrap = true;
	}

	void OnGUI()
	{
		StyleInitialization ();
		GUILayout.BeginArea (new Rect (50.0f, 50.0f, width - 100.0f, height - 100.0f));
		{
			if (GUILayout.Button ("Start", Statics.menuStyle))
			{
				PlayerPrefs.SetInt ("ControlMethod", (int)Statics.selectedControlMethod);
				Application.LoadLevel ("TestLevel");
			}
			if (GUILayout.Button ("Keyboard Controls", Statics.menuStyle))
			{
				Statics.selectedControlMethod = ControlType.keyboard;
				menuText = GetMenuText ();
			}
			/*
			if (GUILayout.Button("Touchpad Controls", Statics.menuStyle))
			{
				Statics.selectedControlMethod = ControlType.touchpad;
			}
			if (GUILayout.Button("Inverted Controls", Statics.menuStyle))
			{
				Statics.selectedControlMethod = ControlType.invertedtouchpad;
			}
			*/
			if (GUILayout.Button ("Tilting Controls", Statics.menuStyle))
			{
				Statics.selectedControlMethod = ControlType.tilting;
				menuText = GetMenuText ();
			}
			if (GUILayout.Button ("Dragging Controls", Statics.menuStyle))
			{
				Statics.selectedControlMethod = ControlType.dragging;
				menuText = GetMenuText ();
			}
			if (GUILayout.Button ("Freeform Dragging Controls", Statics.menuStyle))
			{
				Statics.selectedControlMethod = ControlType.freedragging;
				menuText = GetMenuText ();
			}
			GUILayout.Box(menuText, GUI.skin.box);
		}
		GUILayout.EndArea ();
	}

	string GetMenuText()
	{
		if (Statics.selectedControlMethod == ControlType.keyboard)
		{
			return "Left and Right arrow to move, Z and X to swing with left and right racket.";
		} else if (Statics.selectedControlMethod == ControlType.tilting)
		{
			return "Tilt the phone left and right to move the player, tap anywhere to swing the racket.";
		} else if (Statics.selectedControlMethod == ControlType.dragging)
		{
			return "Press or drag your finger at the bottom of the screen to move the player, tap anywhere else to swing the racket.";
		} else if (Statics.selectedControlMethod == ControlType.freedragging)
		{
			return "Drag your finger anywhere at the screen to move the player, tap anywhere to swing teh racket.";
		} else
		{
			return ":/   :\\   :|";
		}
	}
}
