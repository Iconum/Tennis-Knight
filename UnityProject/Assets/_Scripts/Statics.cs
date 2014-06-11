using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ControlType
{
	keyboard,
	touchpad,
	invertedtouchpad,
	tilting
}

public static class Statics
{
	public static ControlType selectedControlMethod = ControlType.keyboard;
	public static long valuables = 0;
}
