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
		if (Input.GetKeyDown (KeyCode.Space))
		{
			anim.SetTrigger("RightSwing");
			//if(this.anim.GetCurrentAnimatorStateInfo(0).IsName("RightSwing"))
			//{

			//	anim.SetBool("RightSwing",false);
			//}
		}


	}
}
