using UnityEngine;
using System.Collections;

public class KingBall : BallBehaviour
{
	public KingProto theKing;
	public GameObject player;

	protected override void OnDestroy ()
	{
		base.OnDestroy ();
		if (theKing)
		{
			theKing.NoBall ();
		}
	}

	protected override void OnCollisionExit2D (Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Paddle") || collision.gameObject.CompareTag ("Enemy"))
		{
			if (!audio.isPlaying)
			{
				if (audio.clip != paddleHit)
				{
					audio.clip = paddleHit;
				}
				Home ();
				audio.Play ();
			}
		}
	}

	public void GetTheKingAndLink(KingProto king, GameObject link)
	{
		theKing = king;
		player = link;
	}

	public void SpeedUp(float speed)
	{
		constantSpeed += speed;
	}

	protected override void Home()
	{
		if (CompareTag ("Deflectable"))
		{
			rigidbody2D.velocity += new Vector2 ((player.transform.position - transform.position).normalized.x, (player.transform.position - transform.position).normalized.y) * 2;
		} else if (CompareTag ("Deflected"))
		{
			rigidbody2D.velocity += new Vector2 ((theKing.transform.position - transform.position).normalized.x, (theKing.transform.position - transform.position).normalized.y) * 2;
		}
	}
}