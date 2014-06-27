using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigOssiShardBehaviour : BallBehaviour 
{
	
	public GameObject shieldBallPrefab;
	protected BigOssiBallBehaviour shieldBall;

	public Vector3 Velocity;
	protected Vector3 endPosition;
	protected float VelocityX = 0;
	protected float VelocityY = 0;
	protected bool isSpawning = true;
	protected float timer;

	// Use this for initialization
	void Start () 
	{
		shieldBall = shieldBallPrefab.GetComponent<BigOssiBallBehaviour> ();
		VelocityX = Velocity.x;
		VelocityY = Velocity.y;
		endPosition = new Vector3 (gameObject.transform.position.x, -10f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isSpawning)
			spawnMovement ();
		else 
			targetingMovement ();
	}

	protected void spawnMovement()
	{
		//VelocityY -= Time.deltaTime*2;
		timer += Time.deltaTime;
		
		//if (VelocityX < 0)
		//{
		//	VelocityX += Time.deltaTime*2;
		//}
		//else
		//{
		//	VelocityX -= Time.deltaTime*2;
		//}
		
		gameObject.transform.position += new Vector3( VelocityX * Time.deltaTime*2f, VelocityY * Time.deltaTime*2f, 0 );

		if (timer >= 0.1f)
		{
			VelocityX = 0f; VelocityY = 0f;
		}
		if (timer >= 0.5f)
			gameObject.transform.RotateAround(new Vector3 (0,0, 1f),Time.deltaTime*(timer*4));
		if (timer >= 2f)
			isSpawning = false;
	}

	protected void targetingMovement()
	{
		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPosition, Time.deltaTime );
		gameObject.transform.RotateAround(new Vector3 (0,0, 1f),Time.deltaTime*15);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Paddle"))
		{
			Destroy(gameObject);
		}
	}


}
