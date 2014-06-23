using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillagerBehaviour : MonoBehaviour {

	public VillagerHandler handler;
	//Death variables
	public List<AudioClip> deathSounds = null;
	public float flyingHeight = 2.0f;
	public float deathTime = 2.0f;
	public float rotationSpeed = 5.0f;
	public float flyingSpeed = 5.0f;
	protected float rotation = 0.0f;
	protected float height = 0.0f;
	protected bool isDead = false;
	protected bool isGoingup = true;
	//Spawn variables
	protected bool isSpawning = true;
	protected bool hasScreamed = false;
	protected Vector3 spawnEndPos;
	protected Vector3 spawnStartPos;
	protected float startTime;
	protected float spawnLength;
	protected float spawnTime = 0.0f;
	// Use this for initialization
	void Start () {
		handler = GameObject.Find ("VillagerManager").GetComponent<VillagerHandler> (); //get component from handler
		spawnEndPos = new Vector3 (gameObject.transform.position.x, -4.6f); // set spawning start position
		spawnStartPos = new Vector3 (gameObject.transform.position.x, -6.0f); // set spawning end position
		gameObject.transform.position = spawnStartPos; // set villager to spawning start position
		spawnLength = Vector3.Distance (spawnStartPos, spawnEndPos); // check how long is the distance of spawning positions
		startTime = Time.time; //check when spawn started
	}
	
	// Update is called once per frame
	void Update () {
		//update for spawning movement
		if (isSpawning){ 
			spawning();
		}
		//update for death movement
		if (isDead) {
			death ();
		}
	}
	//When hit by "Deflectable", villager dies
	protected virtual void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.CompareTag ("Deflectable") && isDead == false ||
		    collision.gameObject.CompareTag ("Deflected") && isDead == false) {
			handler.spawnPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
			handler.spawnPositions.Add(handler.spawnPos);
			isDead = true;
		}

		collision.gameObject.GetComponent<BallBehaviour> ().BallDestroy ();
	}
	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Deflectable") && isDead == false ||
		    other.CompareTag ("Deflected") && isDead == false) {
			handler.spawnPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
			handler.spawnPositions.Add(handler.spawnPos);
			isDead = true;
		}
	}

	//Death "animation"
	protected virtual void death(){
		if (deathTime > 0)
		{
			if(!hasScreamed && deathSounds != null)
			{
				audio.clip = deathSounds[Random.Range(0, deathSounds.Count)];
				audio.Play();
				hasScreamed = true;
			}

			if(gameObject.transform.position.y < flyingHeight && isGoingup == true)
			{
				height = flyingSpeed/100;
			}
			else 
				isGoingup = false;

			if (isGoingup == false)
				height = -flyingSpeed/50;

			gameObject.transform.Rotate( new Vector3(0.0f,0.0f,rotation));
			gameObject.transform.position += new Vector3 (flyingSpeed/500, height);
			deathTime -= Time.deltaTime;
			rotation += Time.deltaTime*rotationSpeed;
		} 
		else {
			handler.Delete(gameObject);
			Destroy(gameObject);
		}

	}
	//Spawn movement using with lerp function
	protected virtual void spawning()
	{
		if (spawnEndPos.y > gameObject.transform.position.y)
		{
			spawnTime += Time.deltaTime;
			gameObject.transform.position = Vector3.Lerp(spawnStartPos, spawnEndPos, Mathf.SmoothStep(0.0f, 1.0f, spawnTime));
		} else
		{
			isSpawning = false;
			spawnTime = 0.0f;
		}
			//gameObject.transform.position.y = targetPos.y;
	}
}
