using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicLevel : LevelBehaviour {
	public List<EnemyPackage> enemySpawnPackages = new List<EnemyPackage> ();

	void Start()
	{
		StartCoroutine (DelayedCreation (1.5f));
	}

	public override void EnemyDied()
	{
		StartCoroutine (DelayedCreation(5.0f));
	}

	public void VihunLuonti()
	{
		GameObject[] baddudes = enemySpawnPackages [0].Spawner ();
		enemySpawnPackages.RemoveAt (0);
		for (int i = 0; i < baddudes.Length; ++i)
		{
			baddudes[i].GetComponent<EnemyBehaviour> ().levelManager = gameObject;
		}
		if (enemySpawnPackages.Count == 0)
		{
			ToggleWall();
		}
	}
	IEnumerator DelayedCreation(float t)
	{
		yield return new WaitForSeconds (t);
		VihunLuonti ();
	}
}
