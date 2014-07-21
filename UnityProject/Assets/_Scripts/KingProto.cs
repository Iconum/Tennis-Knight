using UnityEngine;
using System.Collections;

public class KingProto : EnemyBehaviour {
	public GameObject player = null;
	public int maxDeflections = 10;
	
	private float _shootTimer = 0.0f, _levelSinTime = 0.0f;
	private Vector2 knightPos;
	private Vector3 kbipos;
	private int deflections, _projectileLayer;
	bool temp;


	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		player = levelManager.GetComponent<LevelBehaviour> ().player;
		deflections = maxDeflections - health;
		StartCoroutine (BallSpawning (spawnLerpLimit));
	}
	
	// Update is called once per frame
	void Update ()
	{
		base.Update ();
		if (!isPaused)
		{
			_levelSinTime += Time.deltaTime;
			transform.position = new Vector3 (Mathf.Cos (_levelSinTime * 4), transform.position.y);
		}
		//transform.position = new Vector3(Mathf.Sin(Time.time - _levelSinTime) * 2.5f, transform.position.y);
		//knightPos = (player.GetComponent<KnightBossProto>().transform.position);
		//Debug.Log (player.GetComponent<KnightBossProto> ().transform.position);

		/*
		GameObject asd;
		if (ballUsed == false)
		{
			asd = (GameObject)Instantiate (projectilePrefab, transform.position+new Vector3(0,-1.2f,0), transform.rotation);
			asd.GetComponent<KingBall> ().SetStartVelocity (new Vector2 (0.1f,-0.5f));
			
			//(Random.Range (-0.2f, 0.2f), -0.4f)
			//asd.
			ballUsed = true;
			Debug.Log (ballUsed);
			
			//temp=asd.GetComponent<KingBall> ().King.GetComponent<KingProto>().ballUsed;
			
		}
		//ballUsed = temp;
		//kbipos = GameObject.Find("KingBall(Clone)").transform.position;
		//Debug.Log (kbipos);

		//temp = projectilePrefab.GetComponent<KingBall>().GetComponent<KingProto>().ballUsed;
		//Debug.Log (temp);
		//GameObject.
		*/
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
				//anim.SetTrigger("Damage");

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
	}
	IEnumerator BallSpawning(float t)
	{
		yield return new WaitForSeconds (t);
		InstantBalls ();
	}
}