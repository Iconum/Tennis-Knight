using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelTester : LevelBehaviour {
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
		GameObject pahis = enemySpawnPackages [0].Spawner () [0];
		enemySpawnPackages.RemoveAt (0);
		pahis.GetComponent<EnemyBehaviour> ().levelManager = gameObject;
	}
	IEnumerator DelayedCreation(float t)
	{
		yield return new WaitForSeconds (t);
		VihunLuonti ();
	}
}
