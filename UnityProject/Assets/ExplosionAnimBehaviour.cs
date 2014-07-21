using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionAnimBehaviour : MonoBehaviour {

	protected Animator anim;
	protected float timer = 0f;

	// Use this for initialization
	void Start () 
	{
		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if (timer >= 0.3f)
			Destroy (gameObject);
	}
}
