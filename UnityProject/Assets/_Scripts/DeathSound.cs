using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathSound : MonoBehaviour 
{

	public List<AudioClip> deathSounds = new List<AudioClip> ();
	// Use this for initialization
	void Start ()
	{
		audio.volume = Statics.soundVolume;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//audio.clip = deathSounds [0];
		//audio.pitch = Random.Range (0.9f, 1.2f);
		//audio.Play ();
	}

	public virtual void batDeath()
	{
		audio.clip = deathSounds [0];
		audio.pitch = Random.Range (0.9f, 1.2f);
		audio.Play ();
	}
}