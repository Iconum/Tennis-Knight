using UnityEngine;
using System.Collections;

public class KingProto : EnemyBehaviour {
	public float shootTimerLimit = 1.0f;
	public GameObject player = null, level = null;
	
	private float _shootTimer = 0.0f, _levelStartTime = 0.0f;
	
	private Animator anim;

	public bool ballUsed;
	private Vector2 knightPos;
	private Vector3 kbipos;
	private int deflections;
	bool temp;


	// Use this for initialization
	void Start ()
	{
		_levelStartTime = Time.time;
		anim = GetComponent<Animator> ();
		ballUsed = false;
		health = 3;
		deflections = 3;
	}
	
	// Update is called once per frame
	void Update ()
	{
		base.Update ();
		transform.position = new Vector3(Mathf.Cos(Time.time*4 - _levelStartTime) , transform.position.y);
		//transform.position = new Vector3(Mathf.Sin(Time.time - _levelStartTime) * 2.5f, transform.position.y);
		
		_shootTimer += Time.deltaTime;
		//if (_shootTimer > shootTimerLimit)
		//{
		//	_shootTimer = 0.0f;
		//	GameObject tempo = (GameObject)Instantiate (projectilePrefab, transform.position, transform.rotation);
		//	tempo.GetComponent<BallBehaviour>().SetStartVelocity(new Vector2(Random.Range(-0.2f, 0.2f), -0.4f));
			//anim.SetTrigger("LeftShot");
		//}

		//knightPos = (player.GetComponent<KnightBossProto>().transform.position);
		//Debug.Log (player.GetComponent<KnightBossProto> ().transform.position);
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
	}
	
	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Deflected"))
		{
			//Destroy(collision.gameObject);
			deflections--;
			//ballUsed = false;

			if (deflections>0)
			{
				anim.SetTrigger("Deflect");
			}

			if(deflections == 0)
			{
				Destroy(collision.gameObject);
				ballUsed = false;
				--health;
				deflections =5;
				anim.SetTrigger("Damage");
				//_flickerActive = true;
				//anim.SetTrigger("Damage");

				if (health == 1)
				{
					deflections =12;
				}
				if (health <= 0)
				{
					//level.GetComponent<LevelTester> ().OssiKuoli ();
					Destroy(gameObject);
				}

			}
		}
		if (collision.gameObject.CompareTag ("KINGBALL"))
		{
			anim.SetTrigger("Deflect");
		}
	}
}