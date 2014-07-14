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
		Third
	}

	public static ButtonTypes lastButton;

	public GameObject menuPrefab;
	public GameObject swordPrefab;
	public AsyncOperation _asyncOp = null;
	
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
			Application.LoadLevel("ModeSelect");
			break;
		case ButtonTypes.Shop:
			Debug.Log("click");
			break;
		case ButtonTypes.Tutorial:
			if(!swordPrefab.gameObject.GetComponent<MenuSwordScript> ().isSwordOn)
			{
				menuPrefab.GetComponent<LevelSelFunctionality>().setPoint(0);
				activateSword();
				lastButton = ButtonTypes.Tutorial;
			}
			break;
		case ButtonTypes.First:
			if(!swordPrefab.GetComponent<MenuSwordScript> ().isSwordOn)
			{
				menuPrefab.GetComponent<LevelSelFunctionality>().setPoint(1);
				activateSword();
				lastButton = ButtonTypes.First;
			}
			break;
		case ButtonTypes.Second:
			if(!swordPrefab.GetComponent<MenuSwordScript> ().isSwordOn)
			{
				menuPrefab.GetComponent<LevelSelFunctionality>().setPoint(2);
				activateSword();
				lastButton = ButtonTypes.Second;
			}
			break;
		case ButtonTypes.Third:
			if(!swordPrefab.GetComponent<MenuSwordScript> ().isSwordOn)
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

					break;
				}
			}
			break;
		case ButtonTypes.Cancel:
			if(swordPrefab.GetComponent<MenuSwordScript> ().isSwordOn)
				deActivateSword();
			break;
		}

		base.PressButton ();
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
