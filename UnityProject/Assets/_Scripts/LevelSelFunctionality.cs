using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelFunctionality : MonoBehaviour {

	public int levelsCleared = 0;
	public GameObject levelSelectorPrefab;
	public GameObject wayPointHandlerPrefab;

	protected int _curPoint = 0,_nextPoint = 0,_tempPoint = 0,_waypointCount = 0;
	protected Vector3 _curPointPos;
	protected Vector3 _nextPointPos;
	protected Vector3 _tempPointPos;
	public List<GameObject> _levels = new List<GameObject>();
	protected List<GameObject> _clouds = new List<GameObject>();
	protected GameObject _level;
	protected GameObject levelSelector;
	protected bool isInPosition = true;
	protected float moveSpeed;

	protected float moveTime = 0f;

	public GameObject lockedLevelCloud = null;

	// Use this for initialization
	void Start () 
	{
		levelsCleared = Statics.levelsComplete;
		for (int i = 0; wayPointHandlerPrefab.transform.childCount > i; ++i)
		{
			_level = wayPointHandlerPrefab.transform.GetChild(i).gameObject;
			_levels.Add(_level);
		}
		levelSelector = (GameObject)Instantiate ( levelSelectorPrefab,
		                                         _levels[levelsCleared].transform.position,
	                                          	  levelSelectorPrefab.transform.rotation);
		if(levelsCleared <= 0 || levelsCleared == 1)
			setPoint (0);
		else
			setPoint(levelsCleared - 1);

		addClouds ();

	}
	// Update is called once per frame
	void Update () 
	{
		moveSpeed = Time.deltaTime * 4f;

		levelSelector.transform.position = new Vector3 (_curPointPos.x, 
		                                                _curPointPos.y + Mathf.Cos(Time.time*2)/6);
		if (isInPosition == false)
		{
			move (_curPointPos,_levels[_nextPoint].transform.position);
		}

		//Optional
		keyboardThings ();
	}

	protected void move (Vector3 from, Vector3 to)
	{
		_curPointPos = Vector3.Lerp (from, to, moveSpeed);
		if (_curPointPos == to)
			isInPosition = true;
	}

	public void setPoint(int pointID)
	{
		_nextPoint = pointID;
		isInPosition = false;
	}
	public int getPoint()
	{
		return _nextPoint;
	}

	void addClouds()
	{
		for (int i = 0; i < _levels.Count; ++i)
		{
			if(_levels[i].GetComponent<PointLevel>().levelID > levelsCleared)
			{
				_levels[i].GetComponent<PointLevel>().isLocked = true;
				var cloud = (GameObject)Instantiate (lockedLevelCloud, _levels [i].transform.position, _levels [i].transform.rotation);
				_clouds.Add (cloud);
			}
		}

	}


	protected void keyboardThings()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1))
		{
			levelsCleared = 1;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2))
		{
			levelsCleared = 2;
		}
		if (Input.GetKeyDown (KeyCode.Alpha3))
		{
			levelsCleared = 3;
		}
	}
}
