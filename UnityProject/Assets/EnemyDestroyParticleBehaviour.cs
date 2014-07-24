using UnityEngine;
using System.Collections;

public class EnemyDestroyParticleBehaviour : MonoBehaviour {
	
	public AudioClip[] audioList;

	// Use this for initialization
	void Start () 
	{
		audio.volume = Statics.soundVolume;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (gameObject.audio.isPlaying == false)
		{
			Destroy(gameObject);
		}
	}

	void playDeathSound(int ID, Color particleColor)
	{
		audio.clip = audioList [ID];
		audio.pitch = Random.Range (0.9f, 1.2f);
		audio.Play ();

		GetComponent<ParticleSystem> ().startColor = particleColor;
	}
}
