using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectilePrefab = null;
	public GameObject levelManager = null;
	public float flickerTimerLimit = 4.0f;
	public int health = 10;
	public bool specialInvincibility = false;
	
	protected float _flickerTimer = 0.0f;
	protected bool _flickerActive = false;
	
	// Update is called once per frame
	protected virtual void Update () {

		if (_flickerActive)
		{
			_flickerTimer += Time.deltaTime;
			if (_flickerTimer % 0.4f < 0.2f)
			{
				renderer.enabled = false;
			}
			else
			{
				renderer.enabled = true;
			}
			if (_flickerTimer > flickerTimerLimit)
			{
				_flickerActive = false;
				renderer.enabled = true;
				_flickerTimer = 0.0f;
			}
		}
	}

	protected void ListDeflectable(GameObject deflectable)
	{
		levelManager.GetComponent<LevelBehaviour> ().AddToDeflectable (deflectable);
		deflectable.GetComponent<BallBehaviour> ().levelManager = levelManager;
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Deflected"))
		{
			collision.gameObject.GetComponent<BallBehaviour>().BallDestroy();
			if (!specialInvincibility)
			{
				DamageHealth ();
			}
		}
	}
	protected virtual void DamageHealth()
	{
		if (!_flickerActive)
		{
			--health;
			_flickerActive = true;
			if (health <= 0)
			{
				levelManager.GetComponent<LevelBehaviour>().EnemyDied();
				Destroy(gameObject);
			}
		}
	}
	protected virtual void InstantDeath()
	{
		levelManager.GetComponent<LevelBehaviour> ().EnemyDied ();
		Destroy (gameObject);
	}
}
