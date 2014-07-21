using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigOssiBallBehaviour : MonoBehaviour {

	public BigOssiBehaviour bigOssi;
	public GameObject bigOssiBallShardPrefab;
	public float shootTimeLimit = 0;
	public float spinningRadius = 1.0f;
	public float enlargeSpeed = 1.0f;
	public float speed = 2.24f;
	public float realRadius = 0;

	protected Vector3 startPos;
	protected float spawnTime;
	protected bool radiusDir = true;

	protected BigOssiShardBehaviour ballShard;
	public Vector3 shardSpawnDir;
	protected List<GameObject> ballShards = new List<GameObject>();
	protected List<Vector3> shardSpawnDirections = new List<Vector3>();

	public GameObject explosionPrefab;
	protected ParticleSystem explosion;

	public delegate void Listing(GameObject deflectable);
	public GameObject levelManager = null;
	public Listing ListDefs;

	public List<AudioClip> sounds = new List<AudioClip> ();
	public GameObject deathSounds = null;
	
	// Use this for initialization
	void Start () {
		explosion = explosionPrefab.GetComponent<ParticleSystem> ();
		ballShard = bigOssiBallShardPrefab.GetComponent<BigOssiShardBehaviour> ();
		startPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
		if (spinningRadius > 2f)
		{
			Debug.Log("Please don't use more than 2f");
			spinningRadius = 2f;
		}
		realRadius = bigOssi.renderer.bounds.size.x/2 + spinningRadius;
		gameObject.transform.position = new Vector3(startPos.x - realRadius, startPos.y);
		gameObject.transform.parent = bigOssi.transform;

		addShardDirections ();

		if (audio)
		{
			audio.volume = Statics.soundVolume;

			audio.clip = sounds [0];
			audio.pitch = Random.Range (0.9f, 1.2f);
			audio.Play ();
		}
	}
	
	// Update is called once per frame
	void Update () {

		spawnTime += Time.deltaTime;
		SpinBalls ();
		changeLayer ();
		if(!bigOssi.isSpawningBalls)
			changeRadius ();
	}

	protected void SpinBalls()
	{
		gameObject.transform.position = (new Vector3(
			(Mathf.Sin((spawnTime*spinningRadius*speed)/(spinningRadius))*enlargeSpeed + bigOssi.transform.position.x),
			Mathf.Cos((spawnTime*spinningRadius*speed)/(spinningRadius))*enlargeSpeed + bigOssi.transform.position.y));
	}

	protected void changeRadius()
	{
		if (radiusDir)
			enlargeSpeed += Time.deltaTime/3;
		else
			enlargeSpeed -= Time.deltaTime/3;

		if (enlargeSpeed <= 0.9f)
			radiusDir = true;
		if (enlargeSpeed >= 1.5f)
			radiusDir = false;

	}

	protected void changeLayer()
	{
		if (gameObject.transform.position.y > bigOssi.transform.position.y+0.3f)
		{
			gameObject.renderer.sortingLayerName = "Background";
		}
		else
			renderer.sortingLayerName = "Characters";
	}

	protected void spawnShards()
	{
		for (int i = 0; i < 5; ++i)
		{
			ballShards.Add ((GameObject)Instantiate (bigOssiBallShardPrefab, transform.position, transform.rotation));
			ballShards [i].GetComponent<BigOssiShardBehaviour> ().Velocity = shardSpawnDirections [i];
			ballShards [i].GetComponent<BigOssiShardBehaviour> ().transform.Rotate (new Vector3 (0, 0, 72f * i));
			ListDefs (ballShards [i]);

			if (audio)
			{
				audio.clip = sounds [1];
				audio.pitch = Random.Range (0.9f, 1.2f);
				audio.Play ();
			}
		}

	}

	protected void addShardDirections()
	{
		//Up
		shardSpawnDirections.Add(new Vector3 (0, 1f));
		//UpLeft
		shardSpawnDirections.Add(new Vector3 (-0.8f,0.5f));
		//DownLeft
		shardSpawnDirections.Add(new Vector3 (-0.5f,-0.5f));
		//DownRight
		shardSpawnDirections.Add(new Vector3 (0.5f,-0.5f));
		//UpRight
		shardSpawnDirections.Add(new Vector3 (0.8f,0.5f));
	}

	public virtual void DeleteObject()
	{
		Instantiate (explosionPrefab, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
		Destroy (gameObject);
	}

	public void SetListing (Listing listing, GameObject level)
	{
		ListDefs = listing;
		levelManager = level;
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		//deathSounds.GetComponent<DeathSound>().

		if (collision.gameObject.CompareTag ("Deflected"))
		{
			spawnShards();
			Destroy(collision.gameObject);
			bigOssi.Delete(gameObject);
			DeleteObject();
		}
	}
}