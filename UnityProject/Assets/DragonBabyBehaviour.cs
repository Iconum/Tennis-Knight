using UnityEngine;
using System.Collections;

public class DragonBabyBehaviour : EnemyBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Update ();
	
	}

	protected void bossPhaseSetter()
	{
		switch (health)
		{
		case 5:

			break;
		case 4:

			break;
		case 3:

			break;
		case 2:

			break;
		case 1:

			break;
		default:

			break;
		}
	}

}
