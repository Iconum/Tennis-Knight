using UnityEngine;
using System.Collections;

public class MenuButtonFunctionality : ButtonFunctionality {
	public ButtonTypes buttonType;
	public enum ButtonTypes
	{
		Start,
		Options,
		Credits
	}

	protected override void PressButton ()
	{
		switch (buttonType)
		{
		case ButtonTypes.Start:
			break;
		case ButtonTypes.Options:
			break;
		case ButtonTypes.Credits:
			break;
		}
		base.PressButton ();
	}
}
