using UnityEngine;
using System.Collections;

public class PauseButtonFunctionality : ButtonFunctionality {
	public PauseBehaviour pauseMenu = null;
	public ButtonTypes buttonType;
	public enum ButtonTypes
	{
		Resume,
		Options,
		ReturnMenu
	}

	protected override void PressButton ()
	{
		switch (buttonType)
		{
		case ButtonTypes.Resume:
			pauseMenu.level.SetPause (false);
			break;
		case ButtonTypes.Options:
			pauseMenu.optionsMenu.gameObject.SetActive(true);
			break;
		case ButtonTypes.ReturnMenu:
			Time.timeScale = 1.0f;
			_asyncOp = Application.LoadLevelAsync ("LevelSelectMenu");
			break;
		}
		base.PressButton ();
	}
	
	protected override void Update ()
	{
		if (!pauseMenu.optionsMenu.gameObject.activeSelf)
			base.Update ();
	}
}
