using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallBehaviour : MonoBehaviour
{
	public Vector2 startVelocity = new Vector2 (0.1f, -1.0f);
	public float constantSpeed = 2.0f, heatGeneration = 1.5f;
	public AudioClip paddleHit;
	public GameObject levelManager = null;
	public bool isPaused = false;
	public GameObject particlePrefab = null;

	protected List<GameObject> _enemies = new List<GameObject>();
	protected Color particleColour = new Color (1f, 0.7f, 0f);

	protected virtual void Start ()
	{
		rigidbody2D.velocity = constantSpeed * startVelocity.normalized;
		audio.clip = paddleHit;
		audio.volume = Statics.soundVolume;
	}

	protected virtual void Update()
	{
		if (isPaused)
			audio.volume = Statics.soundVolume;
	}

	protected virtual void FixedUpdate ()
	{
		rigidbody2D.velocity = constantSpeed * rigidbody2D.velocity.normalized;
	}

	protected virtual void OnDestroy()
	{
		if (levelManager)
		{
			levelManager.GetComponent<LevelBehaviour> ().RemoveFromDeflectable (gameObject);
		}
	}

	protected virtual void OnCollisionExit2D (Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Paddle"))
		{
			//rigidbody2D.velocity = new Vector2(Mathf.Clamp(rigidbody2D.velocity.normalized.x,-0.7f, 0.7f), rigidbody2D.velocity.normalized.y).normalized;
			if (!audio.isPlaying)
			{
				if (audio.clip != paddleHit)
				{
					audio.clip = paddleHit;
				}
				Home();
				particlePrefab.GetComponent<ParticleSystem>().startColor = particleColour;
				var poof = Instantiate(particlePrefab, transform.position, transform.rotation);
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

	protected virtual void Home()
	{
		for (int i = 0; i < _enemies.Count; ++i)
		{
			if (!_enemies [i])
			{
				_enemies.RemoveAt (i);
				--i;
			}
		}
		if (_enemies.Count != 0)
		{
			int ind = Random.Range (0, _enemies.Count);
			rigidbody2D.velocity += new Vector2 ((_enemies [ind].transform.position - transform.position).normalized.x, (_enemies [ind].transform.position - transform.position).normalized.y);
		}
	}

	public void SetPause(bool paused)
	{
		isPaused = paused;
	}

	public virtual void BallDestroy()
	{
		var poof = Instantiate(particlePrefab, transform.position, transform.rotation);
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

	public void SetEnemyWave(List<GameObject> list)
	{
		_enemies = list;
	}

	public void SetStartParameters (Vector2 velocity, float speed, List<GameObject> list)
	{
		SetConstantSpeed (speed);
		SetStartVelocity (velocity);
		SetEnemyWave (list);
	}

}