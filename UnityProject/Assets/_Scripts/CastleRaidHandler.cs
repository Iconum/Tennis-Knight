﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CastleRaidHandler : MonoBehaviour {
	public LevelBehaviour level = null;
	public GameObject terrainPrefab, castlePrefab, tinyVillagerPrefab;
	public Vector3 startLocation, endLocation, castleOffset;
	public int villagerBundleAmount = 20;

	protected bool _displaying = false;
	protected GameObject _terrain, _castle;
	protected List<GameObject> _villagers;
	protected int _spawnedBundles = 0;

	public void Display()
	{
		_displaying = true;
		_terrain = (GameObject)Instantiate (terrainPrefab, transform.position, transform.rotation);
		_castle = (GameObject)Instantiate (castlePrefab, endLocation + castleOffset, Quaternion.Euler (Vector3.zero));
		SpawnBundle ();
	}

	public void SpawnBundle()
	{
		if (villagerBundleAmount * _spawnedBundles > Statics.villagers)
		{
			++_spawnedBundles;
			GameObject tempo = (GameObject)Instantiate (tinyVillagerPrefab, startLocation, Quaternion.Euler (Vector3.zero));
			tempo.GetComponent<BundleBehaviour> ().GetInitialValues (this, _castle, endLocation, startLocation);
			_villagers.Add (tempo);
			StartCoroutine (SpawnDelay ());
		}
	}
	IEnumerator SpawnDelay()
	{
		yield return new WaitForSeconds (0.5f);
		SpawnBundle ();
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
