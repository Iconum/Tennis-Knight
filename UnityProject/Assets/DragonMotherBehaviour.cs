using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragonMotherBehaviour : EnemyBehaviour {
	
	protected bool isOnLimitDistance = false;
	protected bool spawned = false;
	protected float moveTimer = 0f;
	protected float shootTimer = 0f;
	protected bool shoot = false;
	
	public GameObject[] Projectiles = null;
	protected GameObject curProjectile;

	public GameObject explosionPrefab = null;

	protected bool isDead = false;
	protected float deathTimer = 0f;
	protected float explosiontimer = 0f;

	public float shootingSpeed = 0f;
	protected float _shootTimer = 0f;
	protected List<GameObject> ownProjectiles = new List<GameObject> ();
	
	// Update is called once per frame
	void Update () 
	{
		shootTimer += Time.deltaTime;

		base.Update ();
		if(!spawning && !isDead)
		ShootBalls ();
		if (isDead)
			deathAnim ();
		
	}

	protected override void DamageHealth()
	{
		if (!_flickerActive && !isDead)
		{
			--health;
			_flickerActive = true;
			if (anim)
				anim.SetTrigger ("Damage");
			
			if (health <= 0)
			{
				isDead = true;
				//Destroy (gameObject);
			}
		}
	}

	protected override void Flicker ()
	{
		_flickerTimer += Time.deltaTime;
		if (_flickerTimer % 0.1f < 0.05f)
		{
			GetComponent<SpriteRenderer>().color = new Color(0.7f,0.7f,1f);
		} else
		{
			GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f);
		}
		if (_flickerTimer > flickerTimerLimit)
		{
			_flickerActive = false;
			GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f);
			_flickerTimer = 0.0f;
		}
	}

	protected void deathAnim()
	{
		deathTimer += Time.deltaTime;
		explosiontimer += Time.deltaTime;
		transform.position = new Vector2 (transform.position.x + Random.Range (0, 1), transform.position.y + Random.Range (0, 1));
		if (explosiontimer >= 0.15f)
		{
			var explosion = Instantiate(explosionPrefab,
			                            new Vector2(transform.position.x + Random.Range (-3, 3), transform.position.y + Random.Range (-3, 3)),
			                            transform.rotation);
			explosiontimer = 0;
		}
		if (deathTimer >= 5f)
			Destroy (gameObject);

	}
	
	public void ShootBalls()
	{
		_shootTimer += Time.deltaTime;
		if (_shootTimer >= shootingSpeed)
		{
			for (int i = 0; i < ownProjectiles.Count; ++i)
			{
				if (!ownProjectiles [i])
				{
					ownProjectiles.RemoveAt (i);
					--i;
				}
			}
			if ((ownProjectiles.Count < projectileLimit || projectileLimit == 0) && Projectiles != null)
			{
				//set timer to 0
				_shootTimer = 0.0f;
				//Shoot projectile
				curProjectile = Projectiles[Random.Range(0, Projectiles.Length)];
				GameObject tempo = (GameObject)Instantiate (curProjectile, transform.position, transform.rotation);
				//Set velocity to it
				tempo.GetComponent<BallBehaviour> ().SetStartVelocity (new Vector2 (Random.Range (-0.2f, 0.2f), -0.4f));
				//animation Attack
				//list projectile ball
				ownProjectiles.Add (tempo);
				ListDeflectable (tempo);
			}
		}
	}
}
