using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniCastleBehaviour : MonoBehaviour {
	public bool spittingMoney = false;
	public float spittingTimerLimit = 0.5f, bumpTimerLimit = 0.7f, minSpittingRate = 0.005f;
	public List<GameObject> moneyPrefabs;

	private CastleRaidHandler _raidHandler;
	private Vector3 _startScale;
	private int _valuables;
	private float _spittingTimer = 0.0f, _bumpTimer = 0.0f, _animationFinish = 0.0f, _moneySpittingRate = 0.1f;

	void Awake()
	{
		audio.volume = Statics.soundVolume;
		_startScale = transform.localScale;
	}

	void Update ()
	{
		if (spittingMoney)
		{
			_spittingTimer += Time.deltaTime;
			_animationFinish -= Time.deltaTime;
			GameObject tempo = null;
			if (_spittingTimer >= _moneySpittingRate && _valuables > 0)
			{
				_spittingTimer -= _moneySpittingRate;
				--_valuables;
				tempo = (GameObject)Instantiate (moneyPrefabs [Random.Range (0, moneyPrefabs.Count)], transform.position, Quaternion.Euler (Vector3.zero));
				tempo.GetComponent<FlyingMoneyBehaviour> ().PlaySound ();
			}
			if (Input.GetKeyDown (KeyCode.Space) || Input.touchCount > 0)
			{
				int tempi = _valuables - 20;
				while (_valuables > tempi)
				{
					tempo = (GameObject)Instantiate (moneyPrefabs [Random.Range (0, moneyPrefabs.Count)], transform.position, Quaternion.Euler (Vector3.zero));
					CheckLifetime (tempo);
					--_valuables;
				}
				_valuables = 0;
			}
			CheckLifetime (tempo);

			if (_valuables == 0 && _animationFinish <= 0.0f)
			{
				_raidHandler.FinishedAnimation ();
			}
		}

		if (_bumpTimer > 0.0f)
		{
			transform.localScale = new Vector3 (_startScale.x + _bumpTimer / 10.0f, _startScale.y + _bumpTimer / 10.0f, _startScale.z + _bumpTimer / 10.0f);
			_bumpTimer -= Time.deltaTime;
			if (_bumpTimer > bumpTimerLimit)
			{
				_bumpTimer -= Mathf.Pow (Time.deltaTime + (_bumpTimer - bumpTimerLimit), 2);
			}
			if (_bumpTimer < 0.0f)
			{
				_bumpTimer = 0.0f;
			}
		}
	}
	void CheckLifetime(GameObject obj)
	{
		if (obj)
			if (obj.GetComponent<FlyingMoneyBehaviour> ().lifetime > _animationFinish)
				_animationFinish = obj.GetComponent<FlyingMoneyBehaviour> ().lifetime;
	}

	public void SetRaidHandler(CastleRaidHandler handler)
	{
		_raidHandler = handler;
	}

	public void RainingMoney()
	{
		_valuables = _raidHandler.GetFlyingStuff();
		_moneySpittingRate = Mathf.Clamp (spittingTimerLimit / _valuables, minSpittingRate, spittingTimerLimit);
		spittingMoney = true;;
	}

	public void Bump()
	{
		_bumpTimer = bumpTimerLimit;
	}
}
