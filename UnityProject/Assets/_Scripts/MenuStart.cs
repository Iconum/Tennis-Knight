using UnityEngine;
using System.Collections;

public class MenuStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

//		{
//			Application.LoadLevel("testi2");
//		}
		if (Input.touchCount == 1)
		{
			Application.LoadLevel("testi2");
		}
	}

	void OnMouseDown()
	{
		Application.LoadLevel("testi2");
	}
}
