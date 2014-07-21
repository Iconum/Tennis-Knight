using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CastleRaidHandler : MonoBehaviour {
	public LevelBehaviour level = null;
	public GameObject terrainPrefab, castlePrefab, tinyVillagerPrefab;
	public Vector3 startLocation, endLocation, castleOffset;
	public int villagerBundleAmount = 5;
	public float spawnDelay = 0.25f;

	protected bool _displaying = false;
	protected GameObject _terrain, _castle;
	protected List<GameObject> _villagers = new List<GameObject>();
	protected int _spawnedBundles = 0;

	public void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space) || Input.touchCount > 0)
		{
			_spawnedBundles = int.MaxValue;
			for (int i = 0; i < _villagers.Count; ++i)
			{
				Destroy (_villagers [i]);
			}
			_villagers.Clear ();
		}
	}

	public void Display()
	{
		if (!_displaying)
		{
			_displaying = true;
			_terrain = (GameObject)Instantiate (terrainPrefab, transform.position, transform.rotation);
			_terrain.transform.parent = transform;
			_castle = (GameObject)Instantiate (castlePrefab, endLocation + castleOffset, Quaternion.Euler (Vector3.zero));
			_castle.GetComponent<MiniCastleBehaviour> ().SetRaidHandler (this);
			_castle.transform.parent = transform;
			SpawnBundle ();
		}
	}

	public void SpawnBundle()
	{
		if (villagerBundleAmount * _spawnedBundles < Statics.villagers)
		{
			++_spawnedBundles;
			GameObject tempo = (GameObject)Instantiate (tinyVillagerPrefab, startLocation, Quaternion.Euler (Vector3.zero));
			tempo.GetComponent<BundleBehaviour> ().GetInitialValues (this, _castle.GetComponent<MiniCastleBehaviour>(), endLocation, startLocation);
			_villagers.Add (tempo);
			StartCoroutine (SpawnDelay ());
		} else if (_villagers.Count > 0)
		{
			StartCoroutine (SpawnDelay ());
		} else
		{
			_castle.GetComponent<MiniCastleBehaviour> ().RainingMoney ();
		}
	}
	IEnumerator SpawnDelay()
	{
		yield return new WaitForSeconds (spawnDelay);
		SpawnBundle ();
	}

	public void FinishedAnimation()
	{
		level.BackToMenus ();
	}

	public int GetFlyingStuff()
	{
		return level.GetValuableObjects ();
	}

	public void RemoveFromList(GameObject me)
	{
		_villagers.Remove (me);
	}

	public int GetVillagerCount()
	{
		return _villagers.Count;
	}
}
