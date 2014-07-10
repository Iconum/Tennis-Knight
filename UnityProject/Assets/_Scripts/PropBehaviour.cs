using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PropBehaviour : MonoBehaviour {

	public BG3 bg;
	public float pspeed;
	// Use this for initialization
	void Start ()
	{
		bg = GameObject.Find ("Stage3BG").GetComponent<BG3> ();
		pspeed = bg.GetComponent<BG3> ().speed;
	}
	
	// Update is called once per frame
	void Update ()
	{


		var deltaspeed = pspeed * Time.deltaTime;
		//Debug.Log (deltaspeed);
		gameObject.transform.position += new Vector3 (0,-deltaspeed-0.03f);
	}
}