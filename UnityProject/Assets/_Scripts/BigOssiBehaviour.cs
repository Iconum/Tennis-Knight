using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigOssiBehaviour : MonoBehaviour {

	public GameObject projectilePrefab;
	public GameObject shieldBallPrefab;
	public float ballCount = 5f;
	public float shootingSpeed = 3f;
	public int health = 3;

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
		if (_shootTimer >= shootingSpeed)
		{
			_shootTimer = 0.0f;
			GameObject tempo = (GameObject)Instantiate (projectilePrefab, transform.position, transform.rotation);
			tempo.GetComponent<BallBehaviour>().SetStartVelocity(new Vector2(Random.Range(-0.2f, 0.2f), -0.4f));
			//ListDeflectable(tempo);
		}
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Deflected"))
		{
			_shootTimer = 0.0f;

			Destroy(collision.gameObject);

			if(health > 0)
			{
				health--;
				DeleteAll();
				shieldBalls.Clear();
				isSpawningBalls = true;

			}
			else 
			{
				Destroy(gameObject);
				DeleteAll();
				shieldBalls.Clear();
			}
		}
	}

	public void Delete(GameObject me)
	{
		Destroy (me);
	}
	public void DeleteAll()
	{
		for(int i = 0; i < shieldBalls.Count; ++i)
		{
			Delete(shieldBalls[i]);
		}
	}
}
