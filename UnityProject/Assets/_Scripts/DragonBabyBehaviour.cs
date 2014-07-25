using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragonBabyBehaviour : EnemyBehaviour {

	protected bool isOnLimitDistance = false;
	protected bool spawned = false;
	protected float moveTimer = 0f;
	protected float shootTimer = 0f;
	protected bool shoot = false;

	public float shootingSpeed = 0f;
	protected float _shootTimer = 0f;
	protected List<GameObject> ownProjectiles = new List<GameObject> ();
	protected bool babyDeath = false;

	public GameObject motherPrefab = null;
	protected bool motherSpawn = false;
	protected bool isMotherSpawned = false;

	public List<AudioClip> sounds = new List<AudioClip> ();
	private int prevHealth;

	void Start()
	{
		//base.Start ();
		prevHealth = health;

		if (audio)
			audio.volume = Statics.soundVolume;
	}

	// Update is called once per frame
	void Update () 
	{
		if (health < prevHealth)
		{
			anim.SetTrigger ("SDDamage");
			prevHealth = health;
			//Debug.Log(prevHealth);
			if (sounds.Count > 0 && audio)
			{
				audio.clip = sounds [0];
				audio.pitch = Random.Range (1.5f, 1.8f);
				audio.Play ();
			}
		}

		shootTimer += Time.deltaTime;

		base.Update ();

		if (!babyDeath)
		bossMoving ();

		if(shoot && !babyDeath) 
		ShootBalls ();

	
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
			if (ownProjectiles.Count < projectileLimit || projectileLimit == 0)
			{
				//set timer to 0
				_shootTimer = 0.0f;
				//Shoot projectile
				GameObject tempo = (GameObject)Instantiate (projectilePrefab, transform.position, transform.rotation);
				//Set velocity to it
				tempo.GetComponent<BallBehaviour> ().SetStartVelocity (new Vector2 (Random.Range (-0.2f, 0.2f), -0.4f));
				//animation Attack
				//list projectile ball
				ownProjectiles.Add (tempo);
				ListDeflectable (tempo);

				anim.SetTrigger ("SDAttack");

				if (sounds.Count > 0 && audio)
				{
					audio.clip = sounds [1];
					audio.pitch = Random.Range (1.5f, 1.8f);
					audio.Play ();
				}
			}
		}
	}

	protected void bossMoving()
	{	
		if (!spawned && !spawning)
		{
			moveTimer = 0f;
			spawned = true;
		} else if (spawned)
		{
			moveTimer += Time.deltaTime * 2;
			shoot = true;
		}

		if (isOnLimitDistance == true)
		{
			gameObject.transform.Translate (new Vector3 (-Time.deltaTime/4, 0, 0));
			if(!spawning)
			gameObject.transform.position = new Vector3 (transform.position.x,
			                                           targetLocation.y + Mathf.Sin(moveTimer)/6);
			if (gameObject.transform.position.x <= -0.3f)
			{
				isOnLimitDistance = false;
			}
		} else
		{
			gameObject.transform.Translate (new Vector3 (Time.deltaTime/4,0, 0));
			if(!spawning)
			gameObject.transform.position = new Vector3 (transform.position.x,
				                                       targetLocation.y + Mathf.Sin(moveTimer)/6);
			if (gameObject.transform.position.x >= 0.3f)
			{
				isOnLimitDistance = true;
			}
		}
	}
}
