using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectilePrefab = null, player = null;
	public float shootTimerLimit = 1.0f;
	public float flickerTimerLimit = 5.0f;

	private int _health = 10;
	private float _shootTimer = 0.0f, _flickerTimer = 0.0f;
	private bool _flickerActive = false;

	// Use this for initialization
	void Start () {
		GameObject.Instantiate (projectilePrefab, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(-player.transform.position.x, transform.position.y);

		_shootTimer += Time.deltaTime;
		if (_shootTimer > shootTimerLimit)
		{
			_shootTimer = 0.0f;
			GameObject.Instantiate (projectilePrefab, transform.position, transform.rotation);
		}

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

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Deflected"))
		{
			Destroy(collision.gameObject);
			if (!_flickerActive)
			{
				--_health;
				_flickerActive = true;
				if (_health <= 0)
					Destroy(gameObject);
			}
		}
	}
}
