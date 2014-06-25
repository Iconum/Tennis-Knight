using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigOssiBehaviour : EnemyBehaviour {

	public GameObject shieldBallPrefab;
	public float ballCount = 5f;
	public float shootingSpeed = 3f;
	public bool isSpawningBalls = true;
	
	protected BigOssiBallBehaviour shieldBall;
	protected List<GameObject> shieldBalls = new List<GameObject>();
	protected float spawnInterval = 0f;
	protected float spawnTime = 0f;
	protected GameObject bigOssiReference;
	protected bool isOnLimitDistance = false;
	protected float _shootTimer = 0f;

	private Animator anim;
	// Use this for initialization
	void Start ()
	{   //Get the animator
		anim = GetComponent<Animator> ();
		shieldBall = shieldBallPrefab.GetComponent<BigOssiBallBehaviour> ();
		//calculation for spawning for first time
		calculateCircumference ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		//Check, if boss is spawning shield. No shooting until they are ready
		if (isSpawningBalls == true) SpawnBalls ();
		else ShootBalls();
		//Move right when touched the left corner
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
		//Move left when touched the right corner
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
	//Spawn shield balls in calculated time.
	public void SpawnBalls()
	{
		spawnTime += Time.deltaTime;
		//spawn ball when it is in desired distance away from previous ball
		if (spawnTime >= spawnInterval)
		{
			Spawn();
			spawnTime = 0f;
		}
		//check if there is desired amount of shieldballs in the field
		if (shieldBalls.Count >= ballCount)
			isSpawningBalls = false;
	}
	//Spawn shield balls to position related to boss function
	public void Spawn()
	{
		//spawn ball
		shieldBalls.Add((GameObject)Instantiate(shieldBallPrefab,
		                        new Vector3(gameObject.transform.position.x,
		                                    gameObject.transform.position.y + gameObject.renderer.bounds.size.y/2),
		                                    gameObject.transform.rotation));
		//set that ball to list
		shieldBalls [shieldBalls.Count - 1].GetComponent<BigOssiBallBehaviour> ().bigOssi = this;
	}
	//Shoot projectile balls like every other ranged enemy.
	public void ShootBalls()
	{
		_shootTimer += Time.deltaTime;
		if (_shootTimer >= shootingSpeed)
		{
			//set timer to 0
			_shootTimer = 0.0f;
			//Shoot projectile
			GameObject tempo = (GameObject)Instantiate (projectilePrefab, transform.position, transform.rotation);
			//Set velocity to it
			tempo.GetComponent<BallBehaviour>().SetStartVelocity(new Vector2(Random.Range(-0.2f, 0.2f), -0.4f));
			//animation Attack
			anim.SetTrigger("BOAttack");
			//list projectile ball
			ListDeflectable(tempo);
		}
	}
	//Boss took damage, do function with stuff in it
	protected override void DamageHealth ()
	{
		_shootTimer = 0.0f;

		if (!_flickerActive)
		{   //take damage to health
			--health;
			//start flickering -> invureable for a while
			_flickerActive = true; 
			//clear all shield balls
			DeleteAll();
			//animation Take Damage
			anim.SetTrigger("BODamage");

			if (health <= 0)
			{
				//Destroy boss when health is 0
				levelManager.GetComponent<LevelBehaviour>().EnemyDied();
				Destroy(gameObject);
			}
			else
			{
				//Start spawning new balls
				isSpawningBalls = true;
			}
		}
	}
	//Destroy desired game object. (This time it is shield ball)
	public void Delete(GameObject me)
	{
		Destroy (me);
	}

	//Destroys and clears all balls from the field
	public void DeleteAll()
	{   //Destroy all shield balls from a list
		for(int i = 0; i < shieldBalls.Count; ++i)
		{
			Delete(shieldBalls[i]);
		}
		//clear all shieldBalls from a list
		shieldBalls.Clear();
	}
	//Calculates circumference for spawning the balls correclty in circle
	protected void calculateCircumference()
	{		
		//Get speed from ball object
		var speed = shieldBall.speed;
		//Get radius from shield ball and add it to boss sprite width
		var radius = renderer.bounds.size.x/2 + shieldBall.spinningRadius;
		//Calculate circumference
		var circumference = 2 * radius * Mathf.PI;
		//Calculate time with cirumference and speed
		var ballTime = circumference / speed;
		//Calculate spawn interval
		spawnInterval = ballTime / ballCount;
		//Lastly divide spawn interval with 2 and you have rigth spawning rate for balls!
		spawnInterval /= 2;
	}
	//Test ienumerator
	IEnumerator joku()
	{
		yield return new WaitForSeconds (4.0f);
	}
}
