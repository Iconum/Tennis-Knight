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
	
	public float shootingSpeed = 0f;
	protected float _shootTimer = 0f;
	protected List<GameObject> ownProjectiles = new List<GameObject> ();
	
	// Update is called once per frame
	void Update () 
	{
		shootTimer += Time.deltaTime;

		base.Update ();
		if(!spawning)
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
