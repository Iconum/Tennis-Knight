using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigOssiBehaviour : MonoBehaviour {

	public GameObject shieldBallPrefab;
	public float ballCount = 10f;
	
	protected List<GameObject> shieldBalls = new List<GameObject>();
	protected bool isSpawningBalls = true;
	protected float spawnTime = 0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(isSpawningBalls == true)
		SpawnBalls ();


	}

	public void Spawn()
	{
		
		shieldBalls.Add((GameObject)Instantiate(shieldBallPrefab,
		                        gameObject.transform.position,
		                        gameObject.transform.rotation));
		shieldBalls [shieldBalls.Count - 1].GetComponent<BigOssiBallBehaviour> ().bigOssi = this;
	}

	public void SpawnBalls()
	{
		spawnTime += Time.deltaTime;
		if (spawnTime >= 0.2f)
		{
			Spawn();
			spawnTime = 0f;
		}
		if (shieldBalls.Count >= ballCount)
			isSpawningBalls = false;
	}
}
