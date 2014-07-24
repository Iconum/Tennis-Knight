using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndlessLevel : LevelBehaviour {
	public float SpawnDelay = 3.0f;
	public List<AudioClip> music = new List<AudioClip> ();

	void Start()
	{
		StartCoroutine (DelayedCreation (1.5f));

		if (audio)
		{
			audio.volume = Statics.musicVolume;
			
			audio.clip = music [0];
			audio.Play ();
		}

		Destroy (GameObject.Find ("MenuMusic"));
	}

	public override void EnemyDied ()
	{
		StartCoroutine (DelayedCreation (SpawnDelay)); 
	}
	IEnumerator DelayedCreation(float t)
	{
		yield return new WaitForSeconds(t);
		GameObject[] baddudes = enemySpawnPackages [Random.Range(0, enemySpawnPackages.Count)].Spawner (true);
		for (int i = 0; i < baddudes.Length; ++i)
		{
			baddudes [i].GetComponent<EnemyBehaviour> ().levelManager = gameObject;
		}
	}
}
