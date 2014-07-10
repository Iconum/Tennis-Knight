using UnityEngine;
using System.Collections;

public class MenuSwordScript : MonoBehaviour 
{
	public bool isSwordOn = false;
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
		moveSpeed = Time.deltaTime * 10.0f;

		if (isSwordOn == true)
		{
			//gameObject.transform.position = Vector3.Lerp(startPos,
			//                                              new Vector3(startPos.x + 100f, startPos.y),
			//                                              moveSpeed);
			if(gameObject.transform.position.x <= -1f)
			gameObject.transform.Translate(new Vector3 (moveSpeed,0f));
		} else
		{
			if(gameObject.transform.position.x >= startPos.x)
				gameObject.transform.Translate(new Vector3 (-moveSpeed,0f));
		}
	}

	public void swordActivate()
	{
		Debug.Log("activate");
		isSwordOn = true;
	}
	public void swordDeActivate()
	{
		Debug.Log("deactivate");
		isSwordOn = false;
	}

}
