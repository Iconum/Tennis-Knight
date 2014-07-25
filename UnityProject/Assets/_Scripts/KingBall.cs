using UnityEngine;
using System.Collections;

public class KingBall : BallBehaviour
{
	public KingProto theKing;
	public GameObject player;
	

	protected override void Start ()
	{
		base.Start ();
		GetComponent<SpriteRenderer>().color = new Color(1f,0.2f,0.2f);
	}

	protected override void OnDestroy ()
	{
		base.OnDestroy ();
		if (theKing)
		{
			theKing.NoBall ();
		}
	}

	protected override void OnCollisionExit2D(Collision2D collsion)
	{

	}

	protected void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Paddle") || collision.gameObject.CompareTag ("Enemy"))
		{
			if (!audio.isPlaying)
			{
				if (audio.clip != paddleHit)
				{
					audio.clip = paddleHit;
				}
				StartCoroutine(PurkkaHome ());
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

	IEnumerator PurkkaHome()
	{
		yield return new WaitForSeconds (0.01f);
		if (CompareTag ("Deflectable"))
		{
			GetComponent<SpriteRenderer>().color = new Color(1f,0.2f,0.2f);
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, -Mathf.Abs(rigidbody2D.velocity.y));
			rigidbody2D.velocity += new Vector2 ((player.transform.position - transform.position).normalized.x, (player.transform.position - transform.position).normalized.y) * 2;
			Debug.Log("JEE");
		} else if (CompareTag ("Deflected"))
		{
			GetComponent<SpriteRenderer>().color = new Color(0.2f,1f,0.2f);
			rigidbody2D.velocity += new Vector2 ((theKing.transform.position - transform.position).normalized.x, (theKing.transform.position - transform.position).normalized.y) * 2;
			rigidbody2D.velocity = new Vector2 (Mathf.Clamp (rigidbody2D.velocity.x, -0.7f, 0.7f), rigidbody2D.velocity.y);
			Debug.Log("YAY");
		}
	}
}