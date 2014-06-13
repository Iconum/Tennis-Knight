﻿using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour
{
	public Vector2 startVelocity = new Vector2 (0.1f, -1.0f);
	public float constantSpeed = 2.0f;
	public AudioClip paddleHit;
	

	protected virtual void Start ()
	{
		rigidbody2D.velocity = constantSpeed * startVelocity.normalized;
		audio.clip = paddleHit;
	}

	protected virtual void FixedUpdate ()
	{
		rigidbody2D.velocity = constantSpeed * rigidbody2D.velocity.normalized;
	}

	protected virtual void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Paddle"))
		{
			if (!audio.isPlaying)
			{
				if (audio.clip != paddleHit)
				{
					audio.clip = paddleHit;
				}
				audio.Play();
			}
		}
	}

	protected virtual void OnTriggerEnter2D (Collider2D other)
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