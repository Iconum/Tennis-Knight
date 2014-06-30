using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniCastleBehaviour : MonoBehaviour {
	public bool spittingMoney = false;
	public float spittingTimerLimit = 0.1f, bumpTimerLimit = 0.7f;
	public List<GameObject> moneyPrefabs;

	private CastleRaidHandler _raidHandler;
	private int _villagers;
	private float _spittingTimer = 0.0f, _bumpTimer = 0.0f, _animationFinish = 0.0f;

	void Update ()
	{
		if (spittingMoney)
		{
			_spittingTimer += Time.deltaTime;
			_animationFinish -= Time.deltaTime;
			GameObject tempo = null;
			if (_spittingTimer >= spittingTimerLimit && _villagers > 0)
			{
				_spittingTimer -= spittingTimerLimit;
				--_villagers;
				tempo = (GameObject)Instantiate (moneyPrefabs [Random.Range (0, moneyPrefabs.Count)], transform.position, Quaternion.Euler (Vector3.zero));
				tempo.GetComponent<FlyingMoneyBehaviour> ().PlaySound ();
			}
			if (Input.GetMouseButton (0))
			{
				while (_villagers > 0)
				{
					tempo = (GameObject)Instantiate (moneyPrefabs [Random.Range (0, moneyPrefabs.Count)], transform.position, Quaternion.Euler (Vector3.zero));
					--_villagers;
				}
			}
			if (tempo)
			if (tempo.GetComponent<FlyingMoneyBehaviour> ().lifetime > _animationFinish)
				_animationFinish = tempo.GetComponent<FlyingMoneyBehaviour> ().lifetime;
			if (_villagers == 0 && _animationFinish <= 0.0f)
			{
				_raidHandler.FinishedAnimation ();
			}
		}

		if (_bumpTimer > 0.0f)
		{
			transform.localScale = new Vector3 (1.0f + _bumpTimer / 10.0f, 1.0f + _bumpTimer / 10.0f, 1.0f + _bumpTimer / 10.0f);
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

	public void SetRaidHandler(CastleRaidHandler handler)
	{
		_raidHandler = handler;
	}

	public void RainingMoney()
	{
		_villagers = Statics.villagers;
		spittingMoney = true;
	}

	public void Bump()
	{
		_bumpTimer = bumpTimerLimit;
	}
}
