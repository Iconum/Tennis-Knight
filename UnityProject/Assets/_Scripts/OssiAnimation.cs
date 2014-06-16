using UnityEngine;
using System.Collections;

public class OssiAnimation : MonoBehaviour {

	private Animator anim;
	
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.S))
		{
			anim.SetTrigger("LeftShot");
			
		}
		if (Input.GetKeyDown (KeyCode.D))
		{
			anim.SetTrigger("RightShot");
		}
		if (Input.GetKeyDown (KeyCode.A))
		{
			anim.SetTrigger("Damage");
		}
	
	}
}
