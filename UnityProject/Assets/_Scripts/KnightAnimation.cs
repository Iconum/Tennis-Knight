using UnityEngine;
using System.Collections;

public class KnightAnimation : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.C))
		{
			anim.SetTrigger("RightSwing");

		}
		if (Input.GetKeyDown (KeyCode.X))
		{
			anim.SetTrigger("LeftSwing");
		}

	}
}
