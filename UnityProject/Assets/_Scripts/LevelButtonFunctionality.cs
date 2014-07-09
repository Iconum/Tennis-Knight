using UnityEngine;
using System.Collections;

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
	
	public GameObject menuPrefab;
	public GameObject swordPrefab;
	
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
			Debug.Log ("Working!!");
			break;
		case ButtonTypes.Shop:
			Debug.Log("click");
			break;
		case ButtonTypes.Tutorial:
			if(!swordPrefab.gameObject.GetComponent<MenuSwordScript> ().isSwordOn)
			{
				Debug.Log("click");
				menuPrefab.GetComponent<LevelSelFunctionality>().setPoint(0);
				activateSword();
			}
			break;
		case ButtonTypes.First:
			if(!swordPrefab.GetComponent<MenuSwordScript> ().isSwordOn)
			{
				Debug.Log("click");
				menuPrefab.GetComponent<LevelSelFunctionality>().setPoint(1);
				activateSword();
			}
			break;
		case ButtonTypes.Second:
			if(!swordPrefab.GetComponent<MenuSwordScript> ().isSwordOn)
			{
				Debug.Log("click");
				menuPrefab.GetComponent<LevelSelFunctionality>().setPoint(2);
				activateSword();
			}
			break;
		case ButtonTypes.Third:
			if(!swordPrefab.GetComponent<MenuSwordScript> ().isSwordOn)
			{
				Debug.Log("click");
				menuPrefab.GetComponent<LevelSelFunctionality>().setPoint(3);
				activateSword();
			}
			break;
		case ButtonTypes.Begin:
			if(swordPrefab.GetComponent<MenuSwordScript> ().isSwordOn)
			{
				var selectedLevel = menuPrefab.GetComponent<LevelSelFunctionality>().getPoint();
				switch (selectedLevel)
				{
				case 0:
					break;
					
				case 1:
					break;
					
				case 2:
					break;
					
				case 3:
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
