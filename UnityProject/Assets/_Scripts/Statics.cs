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
	freedragging
}

public static class Statics
{
	public static ControlType selectedControlMethod = ControlType.keyboard;
	public static long valuables = 0;
	public static int villagers = 20;
	public static GUIStyle menuStyle;
}
