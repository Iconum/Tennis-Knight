using UnityEngine;
using System.Collections;

public class MenuFunctionality : MonoBehaviour {
	float width, height, thirty, sixty, eighty, onefourty;
	string menuText;

	// Use this for initialization
	void Start () {
		thirty = (30.0f / 640.0f) * Screen.height;
		sixty = (60.0f / 640.0f) * Screen.height;
		eighty = (80.0f / 400.0f) * Screen.width;
		onefourty = (140.0f / 640.0f) * Screen.height;
		width = Screen.width;
		height = Screen.height;

		if (PlayerPrefs.HasKey ("GlobalVolume"))
		{
			Statics.setVolume(PlayerPrefs.GetFloat("GlobalVolume"));
		}
		if (PlayerPrefs.HasKey ("SoundVolume"))
		{
			Statics.soundVolume = PlayerPrefs.GetFloat ("SoundVolume");
		}
		if (PlayerPrefs.HasKey ("MusicVolume"))
		{
			Statics.musicVolume = PlayerPrefs.GetFloat ("MusicVolume");
		}
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

		if (Input.GetKeyDown (KeyCode.Insert))
		{
			Statics.villagers += 10;
		}
	}

	void StyleInitialization()
	{
		Statics.menuButtonStyle = new GUIStyle (GUI.skin.button);
		Statics.menuButtonStyle.fontSize = (Mathf.FloorToInt(height) / 640) * 32;

		Statics.menuTextStyle = new GUIStyle (GUI.skin.box);
		Statics.menuTextStyle.fontSize = (Mathf.FloorToInt(height) / 640) * 24;
		Statics.menuTextStyle.wordWrap = true;
	}

	void OnGUI()
	{
		StyleInitialization ();
		GUILayout.BeginArea (new Rect (20.0f, 20.0f, width - 40.0f, height - 40.0f));
		{
			if (GUILayout.Button ("Start", Statics.menuButtonStyle))
			{
				PrefStoring();
				Application.LoadLevel ("TestLevel");
			}
			if (GUILayout.Button("King Test", Statics.menuButtonStyle))
			{
				PrefStoring();
				Application.LoadLevel ("kingProto");
			}
			if (GUILayout.Button("Ossi Test", Statics.menuButtonStyle))
			{
				PrefStoring();
				Application.LoadLevel ("asko_level");
			}
			GUILayout.Label("Master Volume:", Statics.menuTextStyle);
			Statics.setVolume(GUILayout.HorizontalSlider(Statics.globalVolume, 0.0f, 1.0f, Statics.menuTextStyle, Statics.menuButtonStyle));
			GUILayout.Label("Sound Volume:", Statics.menuTextStyle);
			Statics.soundVolume = GUILayout.HorizontalSlider(Statics.soundVolume, 0.0f, 1.0f, Statics.menuTextStyle, Statics.menuButtonStyle);
			GUILayout.Label("Music Volume:", Statics.menuTextStyle);
			Statics.musicVolume = GUILayout.HorizontalSlider(Statics.musicVolume, 0.0f, 1.0f, Statics.menuTextStyle, Statics.menuButtonStyle);
			GUILayout.Space(thirty);
			if (GUILayout.Button ("Keyboard Controls", Statics.menuButtonStyle))
			{
				Statics.selectedControlMethod = ControlType.keyboard;
				menuText = GetMenuText ();
			}
			/*
			if (GUILayout.Button("Touchpad Controls", Statics.menuButtonStyle))
			{
				Statics.selectedControlMethod = ControlType.touchpad;
			}
			if (GUILayout.Button("Inverted Controls", Statics.menuButtonStyle))
			{
				Statics.selectedControlMethod = ControlType.invertedtouchpad;
			}
			*/
			if (GUILayout.Button ("Tilting Controls", Statics.menuButtonStyle))
			{
				Statics.selectedControlMethod = ControlType.tilting;
				menuText = GetMenuText ();
			}
			if (GUILayout.Button ("Dragging Controls", Statics.menuButtonStyle))
			{
				Statics.selectedControlMethod = ControlType.dragging;
				menuText = GetMenuText ();
			}
			if (GUILayout.Button ("Inverted Dragging Controls", Statics.menuButtonStyle))
			{
				Statics.selectedControlMethod = ControlType.oppositedragging;
				menuText = GetMenuText ();
			}
			if (GUILayout.Button ("Freeform Dragging Controls", Statics.menuButtonStyle))
			{
				Statics.selectedControlMethod = ControlType.freedragging;
				menuText = GetMenuText ();
			}
			GUILayout.Box(menuText, Statics.menuTextStyle);
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
			return "Tilt the phone left and right to move the player, tap on the left side to swing the racket left and tap on the right to swing right.";
		} else if (Statics.selectedControlMethod == ControlType.dragging)
		{
			return "Press or drag your finger at the bottom of the screen to move the player, tap anywhere else to swing the racket.";
		} else if (Statics.selectedControlMethod == ControlType.oppositedragging)
		{
			return "Tap the bottom of the screen to swing the racket, press or drag anywhere else to move the player.";
		} else if (Statics.selectedControlMethod == ControlType.freedragging)
		{
			return "Drag your finger anywhere at the screen to move the player, tap anywhere to swing the racket.";
		} else
		{
			return ":/   :\\   :|";
		}
	}

	public void PrefStoring()
	{
		PlayerPrefs.SetInt ("ControlMethod", (int)Statics.selectedControlMethod);
		PlayerPrefs.SetFloat ("GlobalVolume", Statics.globalVolume);
		PlayerPrefs.SetFloat ("SoundVolume", Statics.soundVolume);
		PlayerPrefs.SetFloat ("MusicVolume", Statics.musicVolume);
		PlayerPrefs.Save ();
	}
}
