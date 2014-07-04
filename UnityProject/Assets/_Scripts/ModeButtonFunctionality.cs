using UnityEngine;
using System.Collections;

public class ModeButtonFunctionality : ButtonFunctionality {
	public ButtonTypes buttonType;
	public enum ButtonTypes
	{
		Campaign,
		Endless,
		Back
	}
	
	protected override void PressButton ()
	{
		switch (buttonType)
		{
		case ButtonTypes.Campaign:
			_asyncOp = Application.LoadLevelAsync("LevelSelect");
			break;
		case ButtonTypes.Endless:
			break;
		case ButtonTypes.Back:
			break;
		}
		base.PressButton ();
	}
}
