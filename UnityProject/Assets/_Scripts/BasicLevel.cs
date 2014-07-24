using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicLevel : LevelBehaviour {
	public bool levelTest = false, openEnd = false;

	public List<AudioClip> music = new List<AudioClip> ();

	public GameObject oldMusic;

	void Start()
	{

		if (!player)
		{
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		if (!villagerManager)
		{
			villagerManager = GameObject.Find ("VillagerManager").GetComponent<VillagerHandler>();
		}
		StartCoroutine (DelayedCreation (1.5f));

		if (audio)
		{
			audio.volume = Statics.musicVolume;

			audio.clip = music [0];
			audio.Play ();
		}

		oldMusic = GameObject.Find("MenuMusic");
		Destroy (oldMusic);
	}

	public override void EnemyDied()
	{
		if (enemySpawnPackages.Count >= 0)
		{
			if (enemySpawnPackages.Count == 0)
			{
				ClearDeflectables ();
				ClearTheLevel(true);
				if (!openEnd)
				{
					BGLoop.current.ToggleStop();
					ToggleWall ();
				}
				audio.clip = music [1];
				audio.Play ();
				StartTheEnd ();
			} else
			{
				StartCoroutine (DelayedCreation (5.0f));
			}
		}
	}

	public void EnemyCreation()
	{
		if (enemySpawnPackages.Count != 0)
		{
			GameObject[] baddudes = enemySpawnPackages [0].Spawner (true);
			enemySpawnPackages.RemoveAt (0);
			for (int i = 0; i < baddudes.Length; ++i)
			{
				baddudes [i].GetComponent<EnemyBehaviour> ().levelManager = gameObject;
			}
			if (enemySpawnPackages.Count == 0)
			{
				if (!openEnd)
				{
					BGLoop.current.ToggleStop();
					ToggleWall ();
				}
				audio.clip = music [1];
				audio.Play ();
			}
		} else if (levelTest)
		{
			ToggleWall ();
		}
	}
	IEnumerator DelayedCreation(float t)
	{
		yield return new WaitForSeconds (t);
		EnemyCreation ();
	}

	public void StartTheEnd()
	{
		if (villagerManager && player)
		{
			villagerManager.GetComponent<VillagerHandler> ().LootCastle ();
			player.GetComponent<PlayerBehaviour> ().StartTheEnd ();
		}
	}
}
