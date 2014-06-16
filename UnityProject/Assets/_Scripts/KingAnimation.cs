using UnityEngine;
using System.Collections;

public class KingAnimation : MonoBehaviour {

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
			//anim.SetTrigger("LeftShot");
			
		}
		if (Input.GetKeyDown (KeyCode.W))
		{
			anim.SetTrigger("Deflect");
		}
		if (Input.GetKeyDown (KeyCode.E))
		{
			anim.SetTrigger("Damage");
		}
		
	}
}
