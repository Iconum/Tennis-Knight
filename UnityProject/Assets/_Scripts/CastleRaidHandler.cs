using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CastleRaidHandler : MonoBehaviour {
	public LevelBehaviour level = null;
	public GameObject terrainPrefab, castlePrefab, tinyVillagerPrefab;
	public List<GameObject> moneyPrefabs;
	public Vector3 startLocation, endLocation;

	protected bool _displaying = false;
	protected GameObject _terrain, _castle;
	protected List<GameObject> _villagers;

	// Update is called once per frame
	void Update () {
		if (_displaying)
		{

		}	
	}

	public void Display()
	{
		_displaying = true;

	}
}
