using UnityEngine;
using System.Collections;

public class IceBallBehaviour : BallBehaviour {
	
	public Vector3 bounceVelocity;
	public float gravity = 2f;
	public float rotationSpeed = 10f;

	protected Vector3 _velocity;
	protected int _bounceCount = 0;
	protected bool _changedDirection = false;


	protected override void Start () 
	{
		base.Start ();
		rigidbody2D.AddTorque (rotationSpeed);
		particleColour = new Color (0.0f, 0.5f, 1.0f);
		
	}

	protected override void FixedUpdate ()
	{
		rigidbody2D.velocity -= new Vector2(0, gravity * Time.fixedDeltaTime);
		if (rigidbody2D.velocity.magnitude > constantSpeed)
			base.FixedUpdate ();
	}

	protected virtual void Bounce()
	{
		if (gravity >= 2.0f)
		{
			gravity -= 2.0f;
		}
		if (gravity <= 1f)
			gravity = 1f;
	}

	protected void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Paddle"))
		{
			Bounce ();
		}
	}

}
