using UnityEngine;
using System.Collections;

public class DividingBehaviour : BallBehaviour {
	public GameObject selfPrefab = null;

	private int _deflectPhase = 1;

	protected override void Start()
	{
		base.Start ();
		transform.localScale = new Vector3 ((_deflectPhase + 1.0f) / 2.0f, (_deflectPhase + 1.0f) / 2.0f, (_deflectPhase + 1.0f) / 2.0f);
	}

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		base.OnCollisionEnter2D (collision);
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
		tempo.GetComponent<DividingBehaviour> ().DownPhase (_deflectPhase, new Vector2(Mathf.Sin(20)+rigidbody2D.velocity.x, Mathf.Cos(20)+rigidbody2D.velocity.y), isPaddle);
		levelManager.GetComponent<LevelBehaviour> ().AddToDeflectable (tempo);
		tempo = (GameObject)Instantiate (selfPrefab, transform.position, transform.rotation);
		tempo.GetComponent<DividingBehaviour> ().DownPhase (_deflectPhase, new Vector2(Mathf.Sin(-20)+rigidbody2D.velocity.x, Mathf.Cos(-20)+rigidbody2D.velocity.y));
		levelManager.GetComponent<LevelBehaviour> ().AddToDeflectable (tempo);
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
	}
}
