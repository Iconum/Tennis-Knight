using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionAnimBehaviour : MonoBehaviour {

	protected Animator anim;
	protected float timer = 0f;
	
	//public List<AudioClip> sounds = new List<AudioClip> ();

	// Use this for initialization
	void Start () 
	{
		anim = gameObject.GetComponent<Animator> ();
		
		//audio.volume = Statics.soundVolume;
		/*if (audio)
		{

			audio.clip = sounds [2];
			audio.pitch = Random.Range (0.9f, 1.2f);
			audio.Play ();
		}*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if (timer >= 0.3f)
			Destroy (gameObject);
	}
}
