using UnityEngine;
using System.Collections;

public class SpearBehaviour : BallBehaviour {

	public bool stopped;
	public float deflectedSpeed = 7.0f;

	protected override void Start()
	{
		base.Start ();
		Turn ();
	}

	protected override void FixedUpdate ()
	{
		if (!stopped)
		{
			rigidbody2D.velocity = constantSpeed * startVelocity.normalized;
		} else
		{
			rigidbody2D.velocity = Vector2.zero;
		}
	}

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		base.OnCollisionEnter2D (collision);
		if (collision.gameObject.CompareTag ("Border"))
		{
			stopped = true;
			Destroy(gameObject, 2.0f);
		}
		else if (collision.gameObject.CompareTag ("Paddle"))
		{
			startVelocity = new Vector2(0.0f, 1.0f);
			constantSpeed = deflectedSpeed;
			Turn();
		}
	}

	void Turn()
	{
		//transform.Rotate (-transform.rotation.eulerAngles);
		transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(startVelocity.x, startVelocity.y));
	}
}
