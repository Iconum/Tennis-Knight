using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangedBehaviour : EnemyBehaviour {
	public float shootTimerLimit = 1.0f;
	public bool usesSinModifier = false, notShootingWalls, dividingProjectiles = false;

	protected float _shootTimer = 0.0f, _levelSinTime = 0.0f, _sinModifier = 1.0f;
	protected bool _sinDirection = true;
	protected List<GameObject> _shotProjectiles = new List<GameObject>();

	public List<AudioClip> batSounds = null;

	// Use this for initialization
	protected override void Awake () {
		base.Awake ();
		anim = GetComponent<Animator> ();
		_levelSinTime += transform.position.x;

		audio.volume = Statics.soundVolume;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		_levelSinTime += Time.deltaTime;
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

		if (!isPaused)
			transform.position = new Vector3 (Mathf.Sin (_levelSinTime) * 2.5f * _sinModifier, transform.position.y);

		if (!spawning && !isPaused)
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
					if (notShootingWalls)
					{
						float tempf = ((transform.position.x + 2.5f) / 5.0f) * 0.4f;
						ShootProjectile (new Vector2 (Random.Range (-tempf, 0.4f-tempf), -0.4f));
					}
					else
					{
						ShootProjectile (new Vector2 (Random.Range (-0.2f, 0.2f), -0.4f));
					}
				}
			}
		}
		if (health < 1)
		{
			Debug.Log("health0");
			if(batSounds!=null)
			{
				audio.clip = batSounds [1];
				audio.pitch = Random.Range (0.9f, 1.2f);
				audio.Play ();
			}
		}
		//Debug.Log (health);
	}

	protected virtual GameObject ShootProjectile(Vector3 dir, GameObject projPrefab = null)
	{
		if (!projPrefab)
			projPrefab = projectilePrefab;
		GameObject tempo = (GameObject)Instantiate (projPrefab, transform.position, transform.rotation);
		tempo.GetComponent<BallBehaviour> ().SetStartVelocity (dir);
		tempo.GetComponent<BallBehaviour> ().SetEnemyWave (_waveEnemies);
		_shotProjectiles.Add (tempo);
		if (dividingProjectiles)
			tempo.GetComponent<DividingBehaviour> ().GetShot (_shotProjectiles);
		ListDeflectable (tempo);
		anim.SetTrigger ("Attack");

		if(batSounds!=null)
		{
			audio.clip = batSounds [0];
			audio.pitch = Random.Range (0.9f, 1.2f);
			audio.Play ();
		}

		return tempo;
	}
}