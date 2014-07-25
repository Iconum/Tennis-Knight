using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DividingBehaviour : BallBehaviour {
	public GameObject selfPrefab = null;
	public List<GameObject> shotProjectiles = null;
	public float rotationspeed = 3f;
	private float rotTimer = 0f;

	private int _deflectPhase = 1;

	protected override void Start()
	{
		base.Start ();
		transform.localScale = new Vector3 ((_deflectPhase + 1.0f) / 2.0f, (_deflectPhase + 1.0f) / 2.0f, (_deflectPhase + 1.0f) / 2.0f);
		particleColour = new Color (1f, 1f, 0f);
	}

	protected void FixedUpdate()
	{
		base.FixedUpdate ();
		rotTimer = Time.deltaTime + rotationspeed;
		gameObject.renderer.transform.Rotate (new Vector3(0,0,rotTimer));// =  new Quaternion (0,0,1,rotTimer);
		//gameObject.renderer.transform.rotation = Quaternion.FromToRotation(Vector3.down, new Vector3(rigidbody2D.velocity.x, rigidbody2D.velocity.y));
	}

	protected override void OnCollisionExit2D(Collision2D collision)
	{
		base.OnCollisionExit2D (collision);
		if (collision.gameObject.CompareTag ("Border") && _deflectPhase != 0)
		{
			GetDeflected();
		}
		else if ((collision.gameObject.CompareTag ("Paddle")) && _deflectPhase != 0)
		{
			GetDeflected(true);
		}
	}

	void GetDeflected(bool isPaddle = false)
	{
		GameObject tempo = (GameObject)Instantiate (selfPrefab, transform.position, transform.rotation);
		tempo.GetComponent<DividingBehaviour> ().SetEnemyWave (_enemies);
		tempo.GetComponent<DividingBehaviour> ().DownPhase (_deflectPhase, new Vector2(Mathf.Sin(20)+rigidbody2D.velocity.x, Mathf.Cos(20)+rigidbody2D.velocity.y), isPaddle);
		levelManager.GetComponent<LevelBehaviour> ().AddToDeflectable (tempo);
		shotProjectiles.Add (tempo);
		tempo = (GameObject)Instantiate (selfPrefab, transform.position, transform.rotation);
		tempo.GetComponent<DividingBehaviour> ().SetEnemyWave (_enemies);
		tempo.GetComponent<DividingBehaviour> ().DownPhase (_deflectPhase, new Vector2(Mathf.Sin(-20)+rigidbody2D.velocity.x, Mathf.Cos(-20)+rigidbody2D.velocity.y));
		levelManager.GetComponent<LevelBehaviour> ().AddToDeflectable (tempo);
		shotProjectiles.Add (tempo);
		Destroy (gameObject);
	}

	public void DownPhase(int i, Vector2 tempvec, bool playSound = false)
	{
		_deflectPhase = i-1;
		startVelocity = tempvec;
		if (playSound)
		{
			audio.Play ();
		}
		Home ();
	}

	public void GetShot(List<GameObject> list)
	{
		shotProjectiles = list;
	}
}
