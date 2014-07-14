using UnityEngine;
using System.Collections;

public class TutoBatBehaviour : RangedBehaviour {
	public TutorialBehaviour tutoKnight = null;
	public GameObject tutorialProjectile = null;
	[System.NonSerialized]
	public bool shotOne = false;

	protected float _lerper = 0.0f;
	protected Vector3 _lerpStart;
	protected bool _ready = false, _reLerp = false;

	protected override void Awake ()
	{
		tutoKnight = GameObject.Find ("TutoKnight").GetComponent<TutorialBehaviour> ();
		tutoKnight.tutoBat = this;
		base.Awake ();
	}

	protected override void Initialize ()
	{
		base.Initialize ();
		_lerpStart = transform.position;
	}

	protected override void Update ()
	{
		if (!tutoKnight.tutorialOn && (tutoKnight.currentState == TutorialState.DragSwing || tutoKnight.currentState == TutorialState.FreeSwing || tutoKnight.currentState == TutorialState.KeySwing ||
			tutoKnight.currentState == TutorialState.OppositeSwing || tutoKnight.currentState == TutorialState.TiltSwing || tutoKnight.currentState == TutorialState.Tanking ||
			tutoKnight.currentState == TutorialState.Dodge))
		{
			if (spawning)
			{
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
			} else if (transform.position.x >= tutoKnight.transform.position.x - 0.7f)
			{
				_lerper += Time.deltaTime * 5.0f;
				transform.position = Vector3.Lerp (_lerpStart, new Vector3 (tutoKnight.transform.position.x - 0.8f, transform.position.y), _lerper);
			} else if (!shotOne)
			{
				GameObject tempo = ShootProjectile (new Vector3 (0.0f, -1.0f), tutorialProjectile);
				tempo.GetComponent<TutoBallBehaviour> ().SetInitials (tutoKnight, this);
				shotOne = true;
				_lerper = 0.0f;
			}
		}
		if (!tutoKnight.tutorialOn && tutoKnight.currentState == TutorialState.End)
		{
			if (_ready)
				base.Update ();
			else
			{
				if (!_reLerp)
				{
					_shotProjectiles.Clear ();
					_lerpStart = transform.position;
					_reLerp = true;
				}
				_lerper += Time.deltaTime * 3;
				transform.position = Vector3.Lerp (_lerpStart, new Vector3 (Mathf.Sin (Time.time - _levelStartTime) * 2.5f * _sinModifier, transform.position.y), _lerper);
				if (_lerper > 1.0f)
					_ready = true;
			}
		}
	}

	protected override void OnCollisionEnter2D (Collision2D collision)
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
}
