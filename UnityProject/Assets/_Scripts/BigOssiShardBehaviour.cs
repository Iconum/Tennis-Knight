using UnityEngine;
using System.Collections;

public class BigOssiShardBehaviour : MonoBehaviour 
{
	public BigOssiBallBehaviour shieldBall;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.Translate (new Vector3(0,-Time.deltaTime, 0));
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Paddle"))
		{
			Destroy(gameObject);
		}
	}
}
