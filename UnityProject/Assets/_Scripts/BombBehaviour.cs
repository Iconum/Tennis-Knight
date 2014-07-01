using UnityEngine;
using System.Collections;

public class BombBehaviour : BallBehaviour {
	public float fuseLength = 6.0f, explosiveForce = 2.0f;
	public AudioClip explosion;

	private bool _exploding = false;

	private Animator anim;

	void Awake()
	{
		StartCoroutine (LitFuse());
		anim = GetComponent<Animator> ();
	}

	IEnumerator LitFuse()
	{
		yield return new WaitForSeconds (fuseLength);
		Explode ();
	}
	IEnumerator StopExploding()
	{
		yield return new WaitForSeconds (0.3f);
		collider2D.enabled = false;

	}

	void Explode()
	{
		if (!_exploding)
		{
			GetComponent<CircleCollider2D> ().isTrigger = true;
			GetComponent<CircleCollider2D> ().radius = explosiveForce;
			rigidbody2D.velocity = Vector2.zero;
			if (!audio.isPlaying)
			{
				audio.clip = explosion;
				audio.volume = 0.6f;
				audio.Play ();

			}
			StartCoroutine (StopExploding ());

			Destroy (gameObject, 1.2f);
			anim.SetTrigger("explode");
			//animation.Play();
		}

	}

	protected override void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Removal"))
		{
			Explode ();

		}
	}

	public override void BallDestroy ()
	{
		Explode ();
		//anim.SetTrigger("explode");
	}
}
