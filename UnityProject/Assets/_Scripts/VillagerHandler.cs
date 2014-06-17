using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillagerHandler : MonoBehaviour {


	public GameObject villagerPrefab;
	public int respawnCount = 5;
	public List<GameObject> villagers = new List<GameObject>();
	public List<Vector3> spawnPositions = new List<Vector3> ();
	public Vector3 spawnPos;

	protected int health = Statics.villagers - 1;
	// Use this for initialization
	void Start () {
		//Spawn villagers on screen when game starts
		for (int i = 0; i < respawnCount; i++)
		{
			//Spawn villagers at fitted positions (this can be tweaked later if there
			//Will be more than 5 villagers on screen
			Spawn(villagerPrefab, transform.position + new Vector3 (-2.0f + i, -4.6f), transform.rotation);
			//villagers.Add((GameObject)Instantiate (villagerPrefab, transform.position + new Vector3 (-2.0f + i, -4.6f), transform.rotation));
		}
	}
	
	// Update is called once per frame
	void Update () {


	}
	//Deletes villagers when fallen and respawns at that position where
	//Last villager died.
	public void Delete(GameObject me)
	{
		villagers.Remove (me);
		Spawn(villagerPrefab, spawnPositions [0], transform.rotation);
		spawnPositions.RemoveAt (0);
	}
	//Checks if there is health, so it can respawn a villager.
	//Also extracts one health per spawn
	public void Spawn(GameObject obj, Vector3 vec3, Quaternion quat)
	{
		if (health > 0)
		{
			villagers.Add ((GameObject)Instantiate (obj, vec3, quat));
			health--;
		}
	}

}
