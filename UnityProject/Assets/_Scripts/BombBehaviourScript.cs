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
			GetComponent<CircleCollider2D> ().radius = explosiveForce;
			Debug.Log ("Boom.");
			Destroy (gameObject, 0.2f);
		}
	}

	public override void BallDestroy ()
	{
		Explode ();
	}
}
