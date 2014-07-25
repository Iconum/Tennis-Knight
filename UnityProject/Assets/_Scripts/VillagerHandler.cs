using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillagerHandler : MonoBehaviour {


	public GameObject villagerPrefab;
	public LevelBehaviour level;
	public int respawnCount = 5;
	public float villagerSpawnRate = 0.5f;
	public List<GameObject> villagers = new List<GameObject>();
	public List<Vector3> spawnPositions = new List<Vector3> ();
	public Vector3 spawnPos;
	public Texture villagerSprite;

	protected int health = Statics.villagers, living = 0;
	protected bool _looting = false, _gameOver = false;
	// Use this for initialization
	void Start () {
		//Spawn villagers on screen when game starts
		if (!level)
		{
			level = GameObject.Find ("Level").GetComponent<LevelBehaviour> ();
		}
		for (int i = 0; i < respawnCount; i++)
		{
			//Spawn villagers at fitted positions (this can be tweaked later if there
			//Will be more than 5 villagers on screen
			Spawn (villagerPrefab, transform.position + new Vector3 (-2.0f + i, -4.6f), transform.rotation);
			//villagers.Add((GameObject)Instantiate (villagerPrefab, transform.position + new Vector3 (-2.0f + i, -4.6f), transform.rotation));
		}
		villagerSpawnRate = 1.0f / villagerPrefab.GetComponent<VillagerBehaviour> ().movementSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (health == 0 && villagers.Count == 0 && !_gameOver && !level.switchingScene)
		{
			level.ClearTheLevel (false);
			_gameOver = true;
		}

		//Debug
		if (Input.GetKeyDown (KeyCode.Home))
		{
			health = 0;
			for (int i = 0; i < villagers.Count; ++i)
			{
				villagers [i].GetComponent<VillagerBehaviour> ().Die ();
			}
		}
	}
	//Deletes villagers when fallen and respawns at that position where
	//Last villager died.
	public void Delete(GameObject me)
	{
		villagers.Remove (me);
		StartCoroutine (DeathSpawn ());
	}
	IEnumerator DeathSpawn()
	{
		yield return new WaitForSeconds(0.1f);
		GameObject tempo = Spawn(villagerPrefab, spawnPositions [0], transform.rotation);
		if (_looting)
		{
			tempo.GetComponent<VillagerBehaviour> ().StartLooting (true);
		}
		spawnPositions.RemoveAt (0);
	}
	public void OffScreen(GameObject me)
	{
		++living;
		villagers.Remove (me);
		if (villagers.Count == 0)
		{

		}
	}
	//Checks if there is health, so it can respawn a villager.
	//Also extracts one health per spawn
	public GameObject Spawn(GameObject obj, Vector3 vec3, Quaternion quat)
	{
		if (health > 0)
		{
			GameObject tempo = (GameObject)Instantiate (obj, vec3, quat); //tempo = temporary object
			villagers.Add (tempo);
			health--;
			return tempo;
		}
		return null;
	}

	public void LootCastle()
	{
		_looting = true;
		for (int i = 0; i < villagers.Count; ++i)
		{
			if (villagers [i])
			if (!villagers [i].GetComponent<VillagerBehaviour> ().StartLooting (true))
				--i;
		}
		StartCoroutine (SpawnLine (villagerSpawnRate));
	}
	IEnumerator SpawnLine(float t)
	{
		yield return new WaitForSeconds (t);
		GameObject tempo = Spawn (villagerPrefab, new Vector3 (0.0f, -6.0f), Quaternion.Euler (Vector3.zero));
		if (tempo)
		{
			tempo.GetComponent<VillagerBehaviour> ().StartLooting ();
			StartCoroutine (SpawnLine (villagerSpawnRate));
		}
	}

	public void attackMelee(Vector3 attackPosition)
	{

	}

	void OnGUI()
	{
		Statics.StyleInitialization ();
		GUI.Box (new Rect (0.0f, 0.0f, 128.0f, 128.0f), villagerSprite, Statics.creditsTextStyle);
		GUI.Label (new Rect (128.0f, 0.0f, 128.0f, 128.0f), "x " + (health + villagers.Count).ToString (), Statics.creditsTextStyle);
	}

	public void GetVillagers()
	{
		Statics.villagers = living + health + villagers.Count;
	}

	public void SetPause(bool paused)
	{
		for (int i = 0; i < villagers.Count; ++i)
		{
			villagers [i].GetComponent<VillagerBehaviour> ().SetPause (paused);
		}
	}

	public void HideVillagers()
	{
		health = 0;
		for (int i = 0; i < villagers.Count; ++i)
		{
			villagers[i].renderer.enabled = false;
		}
	}
}
