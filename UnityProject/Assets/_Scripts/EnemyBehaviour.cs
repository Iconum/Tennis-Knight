using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectilePrefab = null;
	public GameObject levelManager = null;
	public float flickerTimerLimit = 4.0f, spawnLerpLimit = 3.0f;
	public int health = 10;
	public bool specialInvincibility = false, spawning = true;
	public Vector3 targetLocation;
	public Animator anim;
	
	protected float _flickerTimer = 0.0f;
	protected bool _flickerActive = false, _delayedActivation = false;
	protected Vector3 _startLocation;

	protected virtual void Awake()
	{
		_startLocation = transform.position;
	}

	protected virtual void Initialize()
	{

	}

	public virtual void GiveSpawnDelay(Vector3 target, float delay)
	{
		spawning = true;
		targetLocation = target;
		if (delay >= 0.05f)
		{
			_delayedActivation = true;
			StartCoroutine(StartActing(delay));
		}
	}
	IEnumerator StartActing(float t)
	{
		yield return new WaitForSeconds (t);
		_delayedActivation = false;
	}

	// Update is called once per frame
	protected virtual void Update () {

		if (_flickerActive)
		{
			Flicker ();
		}

		if (spawning && !_delayedActivation)
		{
			_flickerTimer += Time.deltaTime / spawnLerpLimit;
			transform.position = Vector3.Lerp (_startLocation, targetLocation, _flickerTimer);
			if (_flickerTimer >= 1.0f)
			{
				spawning = false;
				_flickerTimer = 0.0f;
				Initialize ();
			}
		}

		//Debug Controls
		if (Input.GetKeyDown (KeyCode.End))
		{
			InstantDeath ();
		}
	}

	protected virtual void Flicker()
	{
		_flickerTimer += Time.deltaTime;
		if (_flickerTimer % 0.4f < 0.2f)
		{
			renderer.enabled = false;
		} else
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
			if (!specialInvincibility && !spawning)
			{
				DamageHealth ();
			}
		}
	}
	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Deflected"))
		{
			if (!specialInvincibility && !spawning)
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
			anim.SetTrigger("Damage");

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
