using UnityEngine;
using System.Collections;

public class EnemyDestroyParticleBehaviour : MonoBehaviour {
	
	public AudioClip[] audioList;
	protected bool isPlaying;
	protected float timer = 0f;

	// Use this for initialization
	void Start () 
	{
		audio.volume = Statics.soundVolume;
		particleSystem.renderer.sortingLayerName = "Overlay";
		particleSystem.renderer.sortingOrder = 2;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > 1.3f)
		{
			Destroy(gameObject);
		}
	}

	public void playDeathSound(AudioClip deathSound, Color particleColor)
	{
		if (deathSound != null)
		{
			audio.clip = deathSound;
			Debug.Log(audio.clip.name);
			audio.pitch = Random.Range (0.9f, 1.2f);
			audio.Play ();
		}

		if(particleColor != null)
			GetComponent<ParticleSystem> ().startColor = particleColor;
	}
}
