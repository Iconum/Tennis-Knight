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
	public static float globalVolume = 1.0f;

	public static void setVolume()
	{
		AudioListener.volume = globalVolume;
	}
	public static void setVolume(float volume)
	{
		globalVolume = volume;
		setVolume ();
	}
}
