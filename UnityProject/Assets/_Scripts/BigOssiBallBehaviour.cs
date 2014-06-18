using UnityEngine;
using System.Collections;

public class BigOssiBallBehaviour : MonoBehaviour {

	public BigOssiBehaviour bigOssi;
	public float shootTimeLimit = 0;
	
	protected Vector3 startPos;
	protected float spawnTime;
	
	// Use this for initialization
	void Start () {
		bigOssi = GameObject.Find ("BigOssi").GetComponent<BigOssiBehaviour> ();
		startPos = bigOssi.transform.position;
		gameObject.transform.position = new Vector3(startPos.x - 1.4f,startPos.y);

	}
	
	// Update is called once per frame
	void Update () {

		spawnTime += Time.deltaTime;
		gameObject.transform.Translate(new Vector3((Mathf.Sin(spawnTime*2.24f)/20.0f),Mathf.Cos(spawnTime*2.24f)/20.0f));

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
