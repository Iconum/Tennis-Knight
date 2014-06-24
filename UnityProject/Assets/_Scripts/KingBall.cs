using UnityEngine;
using System.Collections;

public class KingBall : MonoBehaviour
{
	public Vector2 startVelocity = new Vector2 (0.1f, -1.0f);
	public float constantSpeed = 2.0f;
	public KingProto King;
	//public AudioClip paddleHit;
	//public GameObject King = null;
	
	
	protected virtual void Start ()
	{
		rigidbody2D.velocity = constantSpeed * startVelocity.normalized;
		//audio.clip = paddleHit;
		
	}
	
	protected virtual void FixedUpdate ()
	{
		rigidbody2D.velocity = constantSpeed * rigidbody2D.velocity.normalized;
	}
	
	protected virtual void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Paddle"))
		{
			//rigidbody2D.velocity.Set(-0.1f,0.5f);
			//rigidbody2D.AddForce(new Vector2(-20.0f,0.0f));
		}
	}
	
	protected virtual void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Removal"))
		{
			//King.GetComponent<KingProto>().ballUsed = false;
			//Debug.Log(King.GetComponent<KingProto>().ballUsed);

			Destroy (gameObject);
			King.ballUsed = false;
			Debug.Log(King.ballUsed);
		}
	}
	
	public void SetStartVelocity (Vector2 velocity)
	{
		startVelocity = velocity;
	}
}