using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ControlType
{
	keyboard,
	touchpad,
	invertedtouchpad,
	tilting,
	dragging,
	oppositedragging,
	freedragging
}

public static class Statics
{
	public static ControlType selectedControlMethod = ControlType.keyboard;
	public static long valuables = 0;
	public static int villagers = 20, levelsComplete = 0;
	public static GUIStyle menuButtonStyle, menuTextStyle, creditsTextStyle;
	public static float globalVolume = 1.0f, soundVolume = 1.0f, musicVolume = 1.0f;

	public static void setVolume()
	{
		AudioListener.volume = globalVolume;
	}
	public static void setVolume(float volume)
	{
		globalVolume = volume;
		setVolume ();
	}

	public static void PrefLoading()
	{
		if (PlayerPrefs.HasKey ("GlobalVolume"))
		{
			Statics.setVolume (PlayerPrefs.GetFloat ("GlobalVolume"));
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
			Statics.selectedControlMethod = ControlType.freedragging;
			#elif UNITY_EDITOR || UNITY_STANDALONE
			Statics.selectedControlMethod = ControlType.keyboard;
			#else
			Statics.selectedControlMethod = ControlType.freedragging;
			#endif
		}
		if (PlayerPrefs.HasKey ("LevelsComplete"))
		{
			levelsComplete = PlayerPrefs.GetInt ("LevelsComplete");
		}
	}

	public static void PrefStoring()
	{
		PlayerPrefs.SetInt ("ControlMethod", (int)Statics.selectedControlMethod);
		PlayerPrefs.SetFloat ("GlobalVolume", Statics.globalVolume);
		PlayerPrefs.SetFloat ("SoundVolume", Statics.soundVolume);
		PlayerPrefs.SetFloat ("MusicVolume", Statics.musicVolume);
		PlayerPrefs.SetInt ("LevelsComplete", levelsComplete);
		PlayerPrefs.Save ();
	}

	public static void StyleInitialization()
	{
		menuButtonStyle = new GUIStyle (GUI.skin.button);
		menuButtonStyle.fontSize = (Mathf.FloorToInt(Screen.height) / 640) * 26;
		
		menuTextStyle = new GUIStyle (GUI.skin.box);
		menuTextStyle.fontSize = (Mathf.FloorToInt(Screen.height) / 640) * 22;
		menuTextStyle.wordWrap = true;

		creditsTextStyle = new GUIStyle (GUI.skin.label);
		creditsTextStyle.fontSize = (Mathf.FloorToInt(Screen.height) / 640) * 26;
		creditsTextStyle.normal.textColor = Color.black;
	}
}
