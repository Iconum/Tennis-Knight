using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigOssiBehaviour : EnemyBehaviour {

	public GameObject shieldBallPrefab;
	public float ballCount = 5f;
	public float shootingSpeed = 3f;



	protected List<GameObject> shieldBalls = new List<GameObject>();
	public bool isSpawningBalls = true;
	protected float spawnTime = 0f;
	protected GameObject bigOssiReference;
	protected bool isOnLimitDistance = false;
	protected float _shootTimer = 0f;
	protected float spawnDistance = 0f;

	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		var speed = 2.24f;

		var radius = renderer.bounds.size.x/2
			+ shieldBallPrefab.GetComponent<BigOssiBallBehaviour>().spinningRadius;

		Debug.Log (radius);
		var circumference = 2 * radius * Mathf.PI;

		var ballTime = circumference / speed;

		spawnDistance = ballTime / ballCount;
		Debug.Log (spawnDistance);
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();

		if (isSpawningBalls == true)
			SpawnBalls ();
		else {
			ShootBalls();
		}

		if (isOnLimitDistance == false)
		{
			gameObject.transform.Translate (new Vector3 (Time.deltaTime, 0, 0));

			if(gameObject.transform.position.x >= 2.5f)
			{
				isOnLimitDistance = true;
				anim.SetBool("BOMovingLeft", true);
				anim.SetBool("BOMovingRight", false);
			}
		}
		else 
		{
			gameObject.transform.Translate (new Vector3 (-Time.deltaTime, 0, 0));
			if(gameObject.transform.position.x <= -2.5f)
			{
				isOnLimitDistance = false;
				anim.SetBool("BOMovingRight",true);
				anim.SetBool("BOMovingLeft", false);
			}
		}
	}

	public void Spawn()
	{
		shieldBalls.Add((GameObject)Instantiate(shieldBallPrefab,
		                        new Vector3(gameObject.transform.position.x,
		                                    gameObject.transform.position.y + gameObject.renderer.bounds.size.y/2),
		                                    gameObject.transform.rotation));
		shieldBalls [shieldBalls.Count - 1].GetComponent<BigOssiBallBehaviour> ().bigOssi = this;
	}

	public void SpawnBalls()
	{
		spawnTime += Time.deltaTime;
		//if (spawnTime >= 0.4f)
		if (spawnTime >= spawnDistance)
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
			anim.SetTrigger("BOAttack");
			ListDeflectable(tempo);
		}
	}

	protected override void DamageHealth ()
	{
		_shootTimer = 0.0f;

		if (!_flickerActive)
		{
			--health;
			_flickerActive = true;
			DeleteAll();
			shieldBalls.Clear();
			anim.SetTrigger("BODamage");

			if (health <= 0)
			{
				levelManager.GetComponent<LevelBehaviour>().EnemyDied();
				Destroy(gameObject);
			}
			else
			{
				isSpawningBalls = true;
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

	IEnumerator joku()
	{
		yield return new WaitForSeconds (4.0f);
	}
}
