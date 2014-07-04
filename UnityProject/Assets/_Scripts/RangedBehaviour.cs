using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangedBehaviour : EnemyBehaviour {
	public float shootTimerLimit = 1.0f;
	public bool usesSinModifier = false;

	private float _shootTimer = 0.0f, _levelStartTime = 0.0f, _sinModifier = 1.0f;
	private bool _sinDirection = true;
	private List<GameObject> _shotProjectiles = new List<GameObject>();

	// Use this for initialization
	protected override void Awake () {
		base.Awake ();
		_levelStartTime = Time.time;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		
		if (usesSinModifier)
		{
			if (_sinDirection)
			{
				_sinModifier -= Time.deltaTime;
				if (_sinModifier < 0.0f)
				{
					_sinModifier = 0.0f;
					_sinDirection = false;
				}
			} else
			{
				_sinModifier += Time.deltaTime;
				if (_sinModifier > 1.0f)
				{
					_sinModifier = 1.0f;
					_sinDirection = true;
				}
			}
		}

		transform.position = new Vector3 (Mathf.Sin (Time.time - _levelStartTime) * 2.5f * _sinModifier, transform.position.y);

		if (!spawning)
		{
			_shootTimer += Time.deltaTime;
			if (_shootTimer > shootTimerLimit)
			{
				if (projectileLimit != 0)
				{
					for (int i = 0; i < _shotProjectiles.Count; ++i)
					{
						if (!_shotProjectiles [i])
						{
							_shotProjectiles.RemoveAt (i);
							--i;
						}
					}
				}
				if (projectileLimit == 0 || _shotProjectiles.Count < projectileLimit)
				{
					_shootTimer = 0.0f;
					GameObject tempo = (GameObject)Instantiate (projectilePrefab, transform.position, transform.rotation);
					tempo.GetComponent<BallBehaviour> ().SetStartVelocity (new Vector2 (Random.Range (-0.2f, 0.2f), -0.4f));
					tempo.GetComponent<BallBehaviour> ().SetEnemyWave (_waveEnemies);
					_shotProjectiles.Add (tempo);
					ListDeflectable (tempo);
					anim.SetTrigger ("Attack");
				}
			}
		}
	}
}