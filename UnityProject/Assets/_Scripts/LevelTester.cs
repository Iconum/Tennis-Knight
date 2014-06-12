using UnityEngine;
using System.Collections;

public class LevelTester : MonoBehaviour {
	public GameObject vihu = null;
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
		GameObject pahis = (GameObject)Instantiate(vihu, vihuPaikka, Quaternion.Euler(Vector3.zero));
		pahis.GetComponent<OssiBehaviour> ().level = gameObject;
		pahis.GetComponent<OssiBehaviour> ().player = GameObject.Find ("Knight");
	}
	IEnumerator DelayedCreation()
	{
		yield return new WaitForSeconds (5.0f);
		VihunLuonti ();
	}
}
