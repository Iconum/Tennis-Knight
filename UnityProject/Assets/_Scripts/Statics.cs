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
	public static int villagers = 20, levelsComplete = 1, bestEndless = 0;
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
		if (PlayerPrefs.HasKey ("BestEndless"))
		{
			bestEndless = PlayerPrefs.GetInt ("BestEndless");
		}
	}

	public static void PrefStoring()
	{
		PlayerPrefs.SetInt ("ControlMethod", (int)Statics.selectedControlMethod);
		PlayerPrefs.SetFloat ("GlobalVolume", Statics.globalVolume);
		PlayerPrefs.SetFloat ("SoundVolume", Statics.soundVolume);
		PlayerPrefs.SetFloat ("MusicVolume", Statics.musicVolume);
		PlayerPrefs.SetInt ("LevelsComplete", levelsComplete);
		PlayerPrefs.SetInt ("BestEndless", bestEndless);
		PlayerPrefs.Save ();
	}

	public static void StyleInitialization()
	{
		#if UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8 || UNITY_WEBPLAYER
		Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(Vector3.zero), new Vector3(Screen.width / 1080f, Screen.height / 1920f, 1.0f));
#else
		Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(Vector3.zero), new Vector3(Screen.width / 1920f, Screen.height / 1080f, 1.0f));
#endif
		GUI.matrix = matrix;

		menuButtonStyle = new GUIStyle (GUI.skin.button);
		menuButtonStyle.fontSize = 50;
		
		menuTextStyle = new GUIStyle (GUI.skin.box);
		menuTextStyle.fontSize = 44;
		menuTextStyle.wordWrap = true;

		creditsTextStyle = new GUIStyle (GUI.skin.label);
		creditsTextStyle.fontSize = 52;
		creditsTextStyle.fontStyle = FontStyle.Bold;
		creditsTextStyle.normal.textColor = Color.white;
	}

	public static float resolution(bool horizontal)
	{
		#if UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8 || UNITY_WEBPLAYER
		if (horizontal)
			return 1040.0f;
		else
			return 1720.0f;
		#else
		if (horizontal)
			return 1880.0f;
		else
			return 960.0f;
		#endif
	}
}
