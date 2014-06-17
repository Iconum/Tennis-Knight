using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelTester : LevelBehaviour {
	public List<GameObject> enemySpawnPrefabs = new List<GameObject> ();
	public Vector3 vihuPaikka;


	public void OssiKuoli()
	{
		StartCoroutine (DelayedCreation());
	}

	public void VihunLuonti()
	{
		GameObject pahis = (GameObject)Instantiate(enemySpawnPrefabs[Random.Range(0, enemySpawnPrefabs.Count)], vihuPaikka, Quaternion.Euler(Vector3.zero));
		pahis.GetComponent<OssiBehaviour> ().levelManager = gameObject;
	}
	IEnumerator DelayedCreation()
	{
		yield return new WaitForSeconds (5.0f);
		VihunLuonti ();
	}
}
