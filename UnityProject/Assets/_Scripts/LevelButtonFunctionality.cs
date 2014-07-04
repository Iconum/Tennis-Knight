using UnityEngine;
using System.Collections;

public class LevelButtonFunctionality : ButtonFunctionality {
	public ButtonTypes buttonType;
	public enum ButtonTypes
	{
		Back,
		Shop,
		Begin,
		Tutorial,
		First,
		Second,
		Third
	}

	protected override void PressButton ()
	{
		switch (buttonType)
		{
		case ButtonTypes.Back:
			Debug.Log ("Working!!");
			break;
		case ButtonTypes.Shop:
			break;
		case ButtonTypes.Tutorial:
			break;
		case ButtonTypes.First:
			break;
		case ButtonTypes.Second:
			break;
		case ButtonTypes.Third:
			break;
		}
		base.PressButton ();
	}
}
