using UnityEngine;
using System.Collections;

public class BOBTrail : MonoBehaviour {

	public TrailRenderer tr;
	public BigOssiBehaviour bigOssi;
	// Use this for initialization
	void Start () 
	{

		tr.sortingLayerName = "Characters";
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (tr.transform.position.y > bigOssi.transform.position.y)
			tr.sortingLayerName = "Background";
		else
			tr.sortingLayerName = "Characters";
	
	}
}
