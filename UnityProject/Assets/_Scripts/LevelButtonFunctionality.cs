using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelButtonFunctionality : ButtonFunctionality {
	public ButtonTypes buttonType;
	public enum ButtonTypes
	{
		Back,
		Shop,
		Begin,
		Cancel,
		Tutorial,
		First,
		Second,
		Third,
		OffScreen
	}

	public static ButtonTypes lastButton;

	public GameObject menuPrefab;
	public GameObject swordPrefab;
	public AsyncOperation _asyncOp = null;
	public GameObject wayPointHandler = null;

	protected bool isSwordOn = false;
	protected Vector3 startPos;

	protected override void Start()
	{
	}

	protected override void PressButton ()
	{
		switch (buttonType)
		{
		case ButtonTypes.Back:
			Application.LoadLevel("Menu");
			break;
		case ButtonTypes.Tutorial:
			if(swordPrefab.gameObject.GetComponent<MenuSwordScript> ().canClick)
			{
				menuPrefab.GetComponent<LevelSelFunctionality>().setPoint(0);
				activateSword();
				lastButton = ButtonTypes.Tutorial;
			}
			break;
		case ButtonTypes.First:
			if(swordPrefab.GetComponent<MenuSwordScript> ().canClick && !isLevellocked(1))
			{
				menuPrefab.GetComponent<LevelSelFunctionality>().setPoint(1);
				activateSword();
				lastButton = ButtonTypes.First;
			}
			break;
		case ButtonTypes.Second:
			if(swordPrefab.GetComponent<MenuSwordScript> ().canClick && !isLevellocked(2))
			{
				menuPrefab.GetComponent<LevelSelFunctionality>().setPoint(2);
				activateSword();
				lastButton = ButtonTypes.Second;
			}
			break;
		case ButtonTypes.Third:
			if(swordPrefab.GetComponent<MenuSwordScript> ().canClick && !isLevellocked(3))
			{
				menuPrefab.GetComponent<LevelSelFunctionality>().setPoint(3);
				activateSword();
				lastButton = ButtonTypes.Third;
			}
			break;
		case ButtonTypes.Begin:
			if(swordPrefab.GetComponent<MenuSwordScript> ().isSwordOn)
			{
				//var selectedLevel = menuPrefab.GetComponent<LevelSelFunctionality>().getPoint();
				switch (lastButton)
				{
				case ButtonTypes.Tutorial:
					Application.LoadLevel("TutorialLevel");
					break;
					
				case ButtonTypes.First:
					Application.LoadLevel("FirstLevel");
					break;
					
				case ButtonTypes.Second:
					Application.LoadLevel("SecondLevel");
					break;
					
				case ButtonTypes.Third:
					Application.LoadLevel("ThirdLevel");
					break;
				}
			}
			break;
		case ButtonTypes.Cancel:
			if(swordPrefab.GetComponent<MenuSwordScript> ().isSwordOn)
				deActivateSword();
			break;
		case ButtonTypes.OffScreen:
			if(swordPrefab.GetComponent<MenuSwordScript> ().isSwordOn &&
			   !swordPrefab.GetComponent<MenuSwordScript> ().canClick)
				deActivateSword();
			break;
		}

		base.PressButton ();
	}

	bool isLevellocked(int ID)
	{
		bool isItLocked = false;
		for (int i = 0; i < menuPrefab.GetComponent<LevelSelFunctionality>()._levels.Count; ++i)
		{
			if(menuPrefab.GetComponent<LevelSelFunctionality>()._levels[i].GetComponent<PointLevel>().levelID == ID)
				isItLocked =  menuPrefab.GetComponent<LevelSelFunctionality>()._levels[i].GetComponent<PointLevel>().isLocked;
		}
		return isItLocked;
	}

	protected void deActivateSword()
	{
		swordPrefab.GetComponent<MenuSwordScript> ().swordDeActivate ();
	}
	protected void activateSword()
	{
		swordPrefab.GetComponent<MenuSwordScript> ().swordActivate ();
	}
}
