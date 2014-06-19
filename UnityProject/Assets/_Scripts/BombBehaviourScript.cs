using UnityEngine;
using System.Collections;

public class BombBehaviourScript : BallBehaviour {
	public float fuseLength = 6.0f, explosiveForce = 2.0f;

	private bool _exploding = false;

	void Awake()
	{
		StartCoroutine (LitFuse());
	}

	IEnumerator LitFuse()
	{
		yield return new WaitForSeconds (fuseLength);
		Explode ();
	}

	void Explode()
	{
		if (!_exploding)
		{
			GetComponent<CircleCollider2D> ().isTrigger = true;
			GetComponent<CircleCollider2D> ().radius = explosiveForce;
			rigidbody2D.velocity = Vector2.zero;
			Debug.Log ("Boom.");
			Destroy (gameObject, 0.25f);
		}
	}

	public override void BallDestroy ()
	{
		Explode ();
	}
}
