using UnityEngine;
using System.Collections;

public class BigOssiBallBehaviour : MonoBehaviour {

	public BigOssiBehaviour bigOssi;

	protected bool isInPosition = false;
	protected Vector3 startPos;
	protected Vector3 endPos;
	protected float spawnTime;
	// Use this for initialization
	void Start () {
		bigOssi = GameObject.Find ("BigOssi").GetComponent<BigOssiBehaviour> ();
		startPos = bigOssi.transform.position;
		gameObject.transform.position = new Vector3(startPos.x - 1.4f,startPos.y);
		endPos = new Vector3 (gameObject.transform.position.x, 1.0f);

	}
	
	// Update is called once per frame
	void Update () {
		spawnTime += Time.deltaTime;
//
//		if (gameObject.transform.position.y > endPos.y)
//		{
//			gameObject.transform.position = Vector3.Lerp(startPos, endPos, spawnTime);
//		} 
//		else
//		{
//			isInPosition = true;
//		}
		//if (isInPosition == true)
		{
			gameObject.transform.Translate(new Vector3((Mathf.Sin(spawnTime*2.24f)/20.0f),
			                                           Mathf.Cos(spawnTime*2.24f)/20.0f));


			//transform.Translate(Time.deltaTime*5.5f,Time.deltaTime*5.5f,0); // move forward
			//transform.Rotate(0,0,Time.deltaTime*10.5f); // turn a little
		}

	}
}
