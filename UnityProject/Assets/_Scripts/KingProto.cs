using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KingProto : EnemyBehaviour {
	public GameObject player = null;
	public int maxDeflections = 10;
	
	private float _shootTimer = 0.0f, _levelSinTime = 0.0f;
	private Vector2 knightPos;
	private Vector3 kbipos;
	private int deflections, _projectileLayer;
	bool temp;

	public List<AudioClip> sounds = new List<AudioClip> ();

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		player = levelManager.GetComponent<LevelBehaviour> ().player;
		deflections = maxDeflections - health;
		StartCoroutine (BallSpawning (spawnLerpLimit));

		if (audio)
		{
			audio.volume = Statics.soundVolume;

			audio.clip = sounds [0];
			audio.pitch = Random.Range (0.9f, 1.2f);
			audio.Play ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		base.Update ();
		if (!isPaused)
		{
			_levelSinTime += Time.deltaTime;
			transform.position = new Vector3 (Mathf.Cos (_levelSinTime ), transform.position.y);


		}

	}
	
	protected void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Deflected"))
		{
			//Destroy(collision.gameObject);
			deflections--;
			Debug.Log("Herp Derp: " + deflections.ToString());
			collision.gameObject.GetComponent<KingBall>().SpeedUp(0.4f);
			collision.gameObject.tag = "Deflectable";
			//ballUsed = false;

			if (deflections>0)
			{
				anim.SetTrigger("Deflect");
			}

			if(deflections == 0)
			{
				Destroy(collision.gameObject);
				--health;
				deflections = maxDeflections - health;
				anim.SetTrigger("Damage");
				_flickerActive = true;

				if (audio)
				{
					audio.clip = sounds [1];
					audio.pitch = Random.Range (0.9f, 1.2f);
					audio.Play ();
				}

				if (health <= 0)
				{
					Destroy(gameObject);
				}
			}
		}
	}
	public void NoBall()
	{
		StartCoroutine (BallSpawning (4.0f));
	}
	void InstantBalls()
	{
		GameObject tempo = (GameObject)Instantiate (projectilePrefab, transform.position, transform.rotation);
		tempo.GetComponent<BallBehaviour> ().SetStartVelocity (new Vector2 (Random.Range (-0.2f, 0.2f), -0.4f));
		tempo.GetComponent<KingBall> ().GetTheKingAndLink (this, player);
		ListDeflectable (tempo);

		if (audio)
		{
			audio.clip = sounds [0];
			audio.pitch = Random.Range (0.9f, 1.2f);
			audio.Play ();
		}
	}
	IEnumerator BallSpawning(float t)
	{
		yield return new WaitForSeconds (t);
		InstantBalls ();
	}
}