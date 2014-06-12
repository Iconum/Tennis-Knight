using UnityEngine;
using System.Collections;

public class OssiBehaviour : EnemyBehaviour {
	public float shootTimerLimit = 1.0f;
	public GameObject player = null, level = null;

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
			GameObject.Instantiate (projectilePrefab, transform.position, transform.rotation);
		}
	}

	void OnDestroy()
	{
		level.GetComponent<LevelTester> ().OssiKuoli ();
	}
}
