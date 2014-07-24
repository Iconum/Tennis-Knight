using UnityEngine;
using System.Collections;

public class smallFireBall : BallBehaviour {

	// Use this for initialization
	void Start () 
	{
		base.Start ();
		particleColour = new Color (1f, 0.7f, 0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{ 
		base.FixedUpdate ();
		gameObject.renderer.transform.rotation = Quaternion.FromToRotation(Vector3.down, new Vector3(rigidbody2D.velocity.x, rigidbody2D.velocity.y));
	}
}
