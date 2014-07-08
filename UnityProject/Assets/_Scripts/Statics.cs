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
	public static int villagers = 20;
	public static GUIStyle menuButtonStyle;
	public static GUIStyle menuTextStyle;
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
			Statics.selectedControlMethod = ControlType.freedragging;
			#elif UNITY_EDITOR
			Statics.selectedControlMethod = ControlType.keyboard;
			#elif UNITY_STANDALONE
			Statics.selectedControlMethod = ControlType.keyboard;
			#endif
		}
	}

	public static void PrefStoring()
	{
		PlayerPrefs.SetInt ("ControlMethod", (int)Statics.selectedControlMethod);
		PlayerPrefs.SetFloat ("GlobalVolume", Statics.globalVolume);
		PlayerPrefs.SetFloat ("SoundVolume", Statics.soundVolume);
		PlayerPrefs.SetFloat ("MusicVolume", Statics.musicVolume);
		PlayerPrefs.Save ();
	}
}
