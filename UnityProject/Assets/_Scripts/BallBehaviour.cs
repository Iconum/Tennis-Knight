using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour
{
	public Vector2 startVelocity = new Vector2 (0.1f, -1.0f);
	public float constantSpeed = 2.0f;
	public AudioClip paddleHit;
	public GameObject levelManager = null;

	protected virtual void Start ()
	{
		rigidbody2D.velocity = constantSpeed * startVelocity.normalized;
		audio.clip = paddleHit;
	}

	protected virtual void FixedUpdate ()
	{
		rigidbody2D.velocity = constantSpeed * rigidbody2D.velocity.normalized;
	}

	protected virtual void OnDestroy()
	{
		if (levelManager != null)
		{
			levelManager.GetComponent<LevelBehaviour> ().RemoveFromDeflectable (gameObject);
		}
	}

	protected virtual void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Paddle"))
		{
			rigidbody2D.velocity = new Vector2(Mathf.Clamp(rigidbody2D.velocity.normalized.x,-0.7f, 0.7f), rigidbody2D.velocity.normalized.y).normalized;
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

	public virtual void BallDestroy()
	{
		Destroy (gameObject);
	}

	public void SetConstantSpeed(float speed)
	{
		constantSpeed = speed;
	}
		
	public void SetStartVelocity (Vector2 velocity)
	{
		startVelocity = velocity;
	}

	public void SetStartParameters (Vector2 velocity, float speed)
	{
		SetConstantSpeed (speed);
		SetStartVelocity (velocity);
	}

}