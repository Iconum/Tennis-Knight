using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigOssiBehaviour : MonoBehaviour {

	public GameObject projectilePrefab;
	public GameObject shieldBallPrefab;
	public float ballCount = 5f;
	public float shootingSpeed = 0f;

	protected List<GameObject> shieldBalls = new List<GameObject>();
	protected bool isSpawningBalls = true;
	protected float spawnTime = 0f;
	
	protected float _shootTimer = 0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isSpawningBalls == true)
			SpawnBalls ();
		else {
			ShootBalls();
		}
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
		if (spawnTime >= 0.4f)
		{
			Spawn();
			spawnTime = 0f;
		}

		if (shieldBalls.Count >= ballCount)
			isSpawningBalls = false;
	}

	public void ShootBalls()
	{
		_shootTimer += Time.deltaTime;
		if (_shootTimer >= 5.0f)
		{
			_shootTimer = 0.0f;
			GameObject tempo = (GameObject)Instantiate (projectilePrefab, transform.position, transform.rotation);
			tempo.GetComponent<BallBehaviour>().SetStartVelocity(new Vector2(Random.Range(-0.2f, 0.2f), -0.4f));
			//ListDeflectable(tempo);
		}
	}

	public void Delete(GameObject me)
	{
		Destroy (me);
	}
}
