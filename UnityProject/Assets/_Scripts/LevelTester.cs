using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelTester : MonoBehaviour {
	public List<GameObject> enemySpawnPrefabs = new List<GameObject> ();
	public Vector3 vihuPaikka;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OssiKuoli()
	{
		StartCoroutine (DelayedCreation());
	}

	public void VihunLuonti()
	{
		GameObject pahis = (GameObject)Instantiate(enemySpawnPrefabs[Random.Range(0, enemySpawnPrefabs.Count)], vihuPaikka, Quaternion.Euler(Vector3.zero));
		pahis.GetComponent<OssiBehaviour> ().level = gameObject;
	}
	IEnumerator DelayedCreation()
	{
		yield return new WaitForSeconds (5.0f);
		VihunLuonti ();
	}
}
