using UnityEngine;
using System.Collections;

public class OssiBehaviour : EnemyBehaviour {
	public float shootTimerLimit = 1.0f;

	private float _shootTimer = 0.0f, _levelStartTime = 0.0f;

	// Use this for initialization
	void Start () {
		_levelStartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();

		transform.position = new Vector3(Mathf.Sin(Time.time - _levelStartTime) * 2.5f, transform.position.y);
		
		_shootTimer += Time.deltaTime;
		if (_shootTimer > shootTimerLimit)
		{
			_shootTimer = 0.0f;
			GameObject tempo = (GameObject)Instantiate (projectilePrefab, transform.position, transform.rotation);
			tempo.GetComponent<BallBehaviour>().SetStartVelocity(new Vector2(Random.Range(-0.2f, 0.2f), -0.4f));
			ListDeflectable(tempo);
		}
	}

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Deflected"))
		{
			Destroy(collision.gameObject);
			if (!_flickerActive)
			{
				--health;
				_flickerActive = true;
				if (health <= 0)
				{
					levelManager.GetComponent<LevelTester> ().OssiKuoli ();
					Destroy(gameObject);
				}
			}
		}
	}
}