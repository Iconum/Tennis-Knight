using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelFunctionality : MonoBehaviour {

	public int levelsCleared = 0;
	public int lastClearedLevel = 0;
	public GameObject levelSelectorPrefab;
	public GameObject wayPointHandler;

	protected int _curPoint = 0,_nextPoint = 0,_tempPoint = 0,_waypointCount = 0;
	protected Vector3 _curPointPos;
	protected Vector3 _nextPointPos;
	protected List<Vector3> _wayPoints = new List<Vector3>();
	protected GameObject levelSelector;

	// Use this for initialization
	void Start () 
	{
		_waypointCount = countChildren (wayPointHandler.transform);
		for (int i = 0; _waypointCount > i; ++i)
		{
			_wayPoints.Add(wayPointHandler.transform.GetChild(i).position);
		}
		_curPointPos = _wayPoints [levelsCleared];
		_curPoint  = levelsCleared;
		_nextPoint = levelsCleared;
		levelSelector = (GameObject)Instantiate ( levelSelectorPrefab,
		                                         _curPointPos,
		                                          levelSelectorPrefab.transform.rotation);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_curPoint == _nextPoint)
			_curPoint = _nextPoint;
		else if (_curPoint > _nextPoint)
		{

		} else if (_curPoint < _nextPoint)
		{

		}




		levelSelector.transform.position = new Vector3 (_curPointPos.x, 
		                                                _curPointPos.y + Mathf.Cos(Time.time*4)/4);
	}

	protected void gotoNextPoint(int ID)
	{
		//_tempPoint = _wayPoints [ID];
		//levelSelector.transform.position = Vector3.Lerp(_curPoint, _tempPoint, Time.time*2);

	}

	public void setPoint(int pointID)
	{
		_nextPoint = pointID;
	}

	protected void keyboardThings()
	{
		if (Input.GetKey (KeyCode.Alpha1))
		{
			setPoint(0);
		}
		if (Input.GetKey (KeyCode.Alpha2))
		{
			setPoint(4);
		}
	}

	protected int countChildren(Transform a)
	{
		int childCount = 0;
		foreach (Transform b in a)
		{
			childCount++;
			childCount += countChildren(b);
		}
		return childCount;

	}

}
