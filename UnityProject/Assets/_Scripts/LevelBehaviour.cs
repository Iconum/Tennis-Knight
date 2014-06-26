﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemySpawnData
{
	public GameObject enemy = null;
	public Vector3 position = new Vector3(0.0f, 4.0f);
	public float delayTime = 0.0f;
}

[System.Serializable]
public class EnemyPackage
{
	public EnemySpawnData[] prefabPositionCombo;

	public GameObject[] Spawner(bool delayed = false)
	{
		List<GameObject> instantiated = new List<GameObject> ();
		for (int i = 0; i < prefabPositionCombo.Length; ++i)
		{
			if (!delayed)
			{
				instantiated.Add ((GameObject)GameObject.Instantiate (prefabPositionCombo [i].enemy, prefabPositionCombo [i].position, Quaternion.Euler (Vector3.zero)));
			} else
			{
				instantiated.Add ((GameObject)GameObject.Instantiate (prefabPositionCombo [i].enemy, prefabPositionCombo [i].position + new Vector3(0.0f, 5.0f), Quaternion.Euler (Vector3.zero)));
				instantiated[instantiated.Count-1].GetComponent<EnemyBehaviour>().GiveSpawnDelay(prefabPositionCombo[i].position, prefabPositionCombo[i].delayTime);
			}
		}
		return instantiated.ToArray ();
	}
}

public class LevelBehaviour : MonoBehaviour {
	public GameObject topBorder, player = null, villagerManager = null;
	public int loot = 500;
	public int optimalVillagerAmount = 10;
	public float fadeTime = 2.0f;

	protected List<GameObject> deflectableList = new List<GameObject>();
	protected bool _loadingLevel = false;
	protected AsyncOperation _aOperation = null;

	public class DeflectableSorter : Comparer<GameObject>
	{
		public Vector3 _targetPosition;

		public override int Compare (GameObject x, GameObject y)
		{
			return (int)Mathf.Round (Vector3.Distance (x.transform.position, _targetPosition) - Vector3.Distance (y.transform.position, _targetPosition));
		}
	}

	protected virtual void Update()
	{
		if (_loadingLevel)
		{
			//TODO: Kamera haihtuu mustaan
		}
	}

	public virtual void EnemyDied()
	{

	}

	public void AddToDeflectable(GameObject deflectable)
	{
		deflectableList.Add (deflectable);
	}

	public void RemoveFromDeflectable(GameObject deflectable)
	{
		deflectableList.Remove (deflectable);
	}

	public GameObject FindClosestDeflectable(Vector3 pos)
	{
		if (deflectableList.Count != 0)
		{
			DeflectableSorter sorter = new DeflectableSorter ();
			sorter._targetPosition = pos;
			List<GameObject> templist = deflectableList.FindAll (x => x.transform.position.y > (pos.y - 0.3f));
			templist.Sort (sorter);
			return templist [0];
		} else
			return null;
	}

	public void ClearDeflectables()
	{
		for (int i = 0; i < deflectableList.Count; ++i)
		{
			Destroy(deflectableList[i]);
		}
		deflectableList.Clear ();
	}

	public virtual void ClearTheLevel()
	{
		villagerManager.GetComponent<VillagerHandler> ().GetVillagers ();
		float ratio = Mathf.Clamp (Statics.villagers / optimalVillagerAmount, 0.0f, 1.0f);
		Statics.valuables += Mathf.FloorToInt (loot / ratio);
		_loadingLevel = true;
		_aOperation = Application.LoadLevelAsync (0);
		_aOperation.allowSceneActivation = false;
		StartCoroutine (MinFadeTime ());
	}
	IEnumerator MinFadeTime()
	{
		yield return new WaitForSeconds (fadeTime);
		_aOperation.allowSceneActivation = true;
	}

	public void ToggleWall()
	{
		SetWall (topBorder.CompareTag ("Removal"));
	}
	public void SetWall(bool solidity)
	{
		if (solidity)
		{
			topBorder.tag = "Border";
			topBorder.collider2D.isTrigger = false;
		} else
		{
			topBorder.tag = "Removal";
			topBorder.collider2D.isTrigger = true;
		}
	}
}

