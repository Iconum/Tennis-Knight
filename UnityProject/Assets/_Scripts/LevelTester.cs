using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelTester : LevelBehaviour {
	public List<GameObject> enemySpawnPrefabs = new List<GameObject> ();
	public Vector3 vihuPaikka;

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
		GameObject pahis = (GameObject)Instantiate(enemySpawnPrefabs[Random.Range(0, enemySpawnPrefabs.Count)], vihuPaikka, Quaternion.Euler(Vector3.zero));
		pahis.GetComponent<EnemyBehaviour> ().levelManager = gameObject;
	}
	IEnumerator DelayedCreation(float t)
	{
		yield return new WaitForSeconds (t);
		VihunLuonti ();
	}
}
