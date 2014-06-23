using UnityEngine;
using System.Collections;

public class BigOssiBallBehaviour : MonoBehaviour {

	public BigOssiBehaviour bigOssi;
	public float shootTimeLimit = 0;
	public float spinningRadius = 1.0f;
	
	protected Vector3 startPos;
	protected float spawnTime;
	protected bool radiusDir = true;
	
	// Use this for initialization
	void Start () {
		startPos = new Vector3(gameObject.transform.position.x,
		            gameObject.transform.position.y);
		//gameObject.transform.position = new Vector3(startPos.x - 1.4f,startPos.y);
		if (spinningRadius > 2f)
		{
			Debug.Log("Please don't use more than 2f");
			spinningRadius = 2f;
		}
		gameObject.transform.position = new Vector3(startPos.x 
		                                            - bigOssi.renderer.bounds.size.x/2
		                                            + spinningRadius,
		                                            startPos.y);

		gameObject.transform.parent = bigOssi.transform;

	}
	
	// Update is called once per frame
	void Update () {

		spawnTime += Time.deltaTime;
		SpinBalls ();

		if(!bigOssi.isSpawningBalls)
			changeRadius ();
		//gameObject.transform.Translate(new Vector3((Mathf.Sin(spawnTime*2.24f)/20.0f),Mathf.Cos(spawnTime*2.24f)/20.0f));

	}

	protected void SpinBalls()
	{
		gameObject.transform.position = (new Vector3(
			(Mathf.Sin(spawnTime*spinningRadius*2.24f)/(spinningRadius) +  bigOssi.transform.position.x),
			 Mathf.Cos(spawnTime*spinningRadius*2.24f)/(spinningRadius)  + bigOssi.transform.position.y));
	}

	protected void changeRadius()
	{
		if (radiusDir)
			spinningRadius += 0.01f;
		else
			spinningRadius -= 0.01f;

		if (spinningRadius <= 0f)
			radiusDir = true;
		if (spinningRadius >= 2f)
			radiusDir = false;

	}

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Deflected"))
		{
			Destroy(collision.gameObject);
			bigOssi.Delete(gameObject);
			Destroy(gameObject);
		}
	}
}
