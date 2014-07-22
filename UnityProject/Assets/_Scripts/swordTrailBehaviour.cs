using UnityEngine;
using System.Collections;

public class swordTrailBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameObject.particleSystem.isStopped)
		{
			Destroy(gameObject);
		}
	}
}
