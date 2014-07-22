using UnityEngine;
using System.Collections;

public class MenuSwordScript : MonoBehaviour 
{
	public bool isSwordOn = false;
	public bool canClick = true;
	protected Vector3 startPos;
	protected float moveSpeed;

	// Use this for initialization
	void Start () 
	{
		startPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		moveSpeed = Time.deltaTime * 15.0f;

		if (isSwordOn == true)
		{
			//gameObject.transform.position = Vector3.Lerp(startPos,
			//                                              new Vector3(startPos.x + 100f, startPos.y),
			//                                              moveSpeed);
			if(gameObject.transform.position.x <= -1f)
			{
				gameObject.transform.Translate(new Vector3 (moveSpeed,0f));
			}
			canClick = false;
		} else
		{
			if(gameObject.transform.position.x >= startPos.x)
			{
				gameObject.transform.Translate(new Vector3 (-moveSpeed,0f));
			}
			else
				canClick = true;
		}
	}

	public void swordActivate()
	{
		isSwordOn = true;
	}
	public void swordDeActivate()
	{
		isSwordOn = false;
	}

}
