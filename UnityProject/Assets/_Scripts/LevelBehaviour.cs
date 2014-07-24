using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemySpawnData
{
	public GameObject enemy = null;
	public Vector3 position = new Vector3(0.0f, 4.0f);
	public float delayTime = 0.0f;
}

[System.Serializable]
public class EnemyPackage
{
	public EnemySpawnData[] prefabPositionCombo;

	public GameObject[] Spawner(bool delayed = false)
	{
		List<GameObject> instantiated = new List<GameObject> ();
		for (int i = 0; i < prefabPositionCombo.Length; ++i)
		{
			if (!delayed)
			{
				instantiated.Add ((GameObject)GameObject.Instantiate (prefabPositionCombo [i].enemy, prefabPositionCombo [i].position, Quaternion.Euler (Vector3.zero)));
			} else
			{
				instantiated.Add ((GameObject)GameObject.Instantiate (prefabPositionCombo [i].enemy, prefabPositionCombo [i].position + new Vector3(0.0f, 5.0f), Quaternion.Euler (Vector3.zero)));
				instantiated[instantiated.Count-1].GetComponent<EnemyBehaviour>().GiveSpawnDelay(prefabPositionCombo[i].position, prefabPositionCombo[i].delayTime);
			}
		}
		for (int j = 0; j < instantiated.Count; ++j)
		{
			instantiated [j].GetComponent<EnemyBehaviour> ().WaveEnemies (instantiated);
		}
		return instantiated.ToArray ();
	}
}

public class LevelBehaviour : MonoBehaviour {
	public GameObject topBorder, player = null;
	public VillagerHandler villagerManager = null;
	public CastleRaidHandler castleHandler = null;
	public PauseBehaviour pauseMenu = null;
	public bool hasMiniView = true, isPaused = false;
	public int loot = 500, optimalVillagerAmount = 10, levelNumber = 1;
	public float startFadeTime = 1.0f, endFadeTime = 2.0f, miniWaitTime = 3.0f, gameOverTime = 4.0f;
	public Texture2D fadeTexture;
	public List<EnemyPackage> enemySpawnPackages = new List<EnemyPackage> ();

	protected List<GameObject> deflectableList = new List<GameObject>(), _currentEnemies = new List<GameObject>();
	protected bool _loadingLevel = false, _startingLevel = true;
	protected float _fadeTimer = 0.0f, _alpha = 1.0f;
	protected AsyncOperation _aOperation = null;

	public class DeflectableSorter : Comparer<GameObject>
	{
		public Vector3 _targetPosition;

		public override int Compare (GameObject x, GameObject y)
		{
			return (int)Mathf.Round (Vector3.Distance (x.transform.position, _targetPosition) - Vector3.Distance (y.transform.position, _targetPosition));
		}
	}

	protected virtual void Update()
	{
		if (_loadingLevel)
		{
			_fadeTimer += Time.deltaTime;
			if (_fadeTimer >= endFadeTime)
			{
				_fadeTimer = endFadeTime;
				_aOperation.allowSceneActivation = true;
			}
			_alpha = _fadeTimer / endFadeTime;
		} else if (_startingLevel)
		{
			_fadeTimer += Time.deltaTime;
			if (_fadeTimer >= startFadeTime)
			{
				_fadeTimer = 0.0f;
				_startingLevel = false;
			}
			_alpha = (startFadeTime - _fadeTimer) / startFadeTime;
		}

		if (isPaused)
		{
			audio.volume = Statics.musicVolume;
		}

		//Debug
		if (Input.GetKeyDown (KeyCode.PageDown))
		{
			if (enemySpawnPackages.Count != 0)
				enemySpawnPackages.RemoveAt (0);
		}
	}

	public virtual void EnemyDied()
	{

	}

	public void AddToDeflectable(GameObject deflectable)
	{
		deflectableList.Add (deflectable);
	}

	public void RemoveFromDeflectable(GameObject deflectable)
	{
		deflectableList.Remove (deflectable);
	}

	public int DeflectableCount()
	{
		return deflectableList.Count;
	}

	public GameObject FindClosestDeflectable(Vector3 pos)
	{
		if (deflectableList.Count != 0)
		{
			DeflectableSorter sorter = new DeflectableSorter ();
			sorter._targetPosition = pos;
			List<GameObject> templist = deflectableList.FindAll (x => x.transform.position.y > (pos.y - 0.3f));
			if (templist.Count != 0)
			{
				templist.Sort (sorter);
				return templist [0];
			} else
			{
				return null;
			}
		} else
			return null;
	}

	public void ClearDeflectables()
	{
		for (int i = 0; i < deflectableList.Count; ++i)
		{
			Destroy(deflectableList[i]);
		}
		deflectableList.Clear ();
	}

	public virtual void ClearTheLevel(bool finished)
	{
		villagerManager.GetVillagers ();
		//float ratio = Mathf.Clamp (Statics.villagers / optimalVillagerAmount, 0.0f, 1.0f);
		//Statics.valuables += Mathf.FloorToInt (loot / ratio); //Removed feature
		_alpha = 0.0f;
		_aOperation = Application.LoadLevelAsync ("LevelSelectMenu");
		_aOperation.allowSceneActivation = false;
		if (finished)
		{
			if (Statics.levelsComplete < levelNumber + 1)
				Statics.levelsComplete = levelNumber + 1;

			if (hasMiniView)
				StartCoroutine (StartMiniView ());
			else 
				BackToMenus ();
		} else
		{
			player.GetComponent<PlayerBehaviour> ().GameOver ();
			BGLoop.current.ToggleStop ();
			StartCoroutine (GameOverTime ());
		}
	}
	IEnumerator StartMiniView()
	{
		yield return new WaitForSeconds (miniWaitTime);
		villagerManager.HideVillagers ();
		player.renderer.enabled = false;
		castleHandler.gameObject.SetActive (true);
		castleHandler.Display ();
	}
	IEnumerator GameOverTime()
	{
		yield return new WaitForSeconds (gameOverTime);
		BackToMenus ();
	}

	public virtual void BackToMenus()
	{
		if (Statics.villagers < 20)
		{
			Statics.villagers = 20;
		}
		_loadingLevel = true;
	}

	public int GetValuableObjects()
	{
		return ((loot / 10) * Mathf.Clamp(Statics.villagers, 0, optimalVillagerAmount))/optimalVillagerAmount;
	}

	public void ToggleWall()
	{
		SetWall (topBorder.CompareTag ("Removal"));
	}
	public void SetWall(bool solidity)
	{
		if (solidity)
		{
			topBorder.tag = "Border";
			topBorder.collider2D.isTrigger = false;
		} else
		{
			topBorder.tag = "Removal";
			topBorder.collider2D.isTrigger = true;
		}
	}

	public void SetPause(bool paused)
	{
		isPaused = paused;
		villagerManager.SetPause (paused);
		player.GetComponent<PlayerBehaviour> ().SetPause (paused);
		for (int i = 0; i < _currentEnemies.Count; ++i)
		{
			if (_currentEnemies[i])
				_currentEnemies [i].GetComponent<EnemyBehaviour> ().SetPause (paused);
		}
		for (int j = 0; j < deflectableList.Count; ++j)
		{
			if (deflectableList[j])
				deflectableList[j].GetComponent<BallBehaviour> ().SetPause (paused);
		}
		if (paused)
			Time.timeScale = 0.0f;
		else
			Time.timeScale = 1.0f;
		pauseMenu.gameObject.SetActive (paused);
	}

	void OnGUI()
	{
		if (_startingLevel || _loadingLevel)
		{
			GUI.color = new Color(1.0f, 1.0f, 1.0f, _alpha);
			GUI.depth = -1000;
			GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), fadeTexture);
		}
	}
}

