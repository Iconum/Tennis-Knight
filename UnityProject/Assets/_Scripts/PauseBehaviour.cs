using UnityEngine;
using System.Collections;

public class PauseBehaviour : MonoBehaviour {
	public LevelBehaviour level = null;
	public OptionsFunctionality optionsMenu;

	protected void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			if (optionsMenu.gameObject.activeSelf)
			{
				optionsMenu.gameObject.SetActive (false);
			} else
				level.SetPause (false);
		}
	}
}
