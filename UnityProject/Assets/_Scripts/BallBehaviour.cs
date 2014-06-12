using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour
{
	public Vector2 startVelocity = new Vector2 (0.1f, -1.0f);
	public float constantSpeed = 2.0f;
	public AudioClip paddleHit;

	// Use this for initialization
	void Start ()
	{
		rigidbody2D.velocity = constantSpeed * startVelocity.normalized;
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	void FixedUpdate ()
	{
		rigidbody2D.velocity = constantSpeed * rigidbody2D.velocity.normalized;
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Paddle"))
		{
			if (!audio.isPlaying)
			{
				audio.clip = paddleHit;
				audio.Play();
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Removal"))
		{
			Destroy (gameObject);
		}
	}
		
	public void SetStartVelocity (Vector2 velocity)
	{
		startVelocity = velocity;
	}
}