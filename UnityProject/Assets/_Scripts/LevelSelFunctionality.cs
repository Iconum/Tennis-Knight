using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelFunctionality : MonoBehaviour {

	public int levelsCleared = 0;
	public int lastClearedLevel = 0;
	public GameObject levelSelectorPrefab;
	public GameObject wayPointPrefab;

	protected int _curPoint = 0,_nextPoint = 0,_tempPoint = 0,_waypointCount = 0;
	protected Vector3 _curPointPos;
	protected Vector3 _nextPointPos;
	protected Vector3 _tempPointPos;
	protected List<Vector3> _wayPoints = new List<Vector3>();
	protected GameObject levelSelector;
	protected bool isInPosition = true;

	protected float moveTime = 0f;

	// Use this for initialization
	void Start () 
	{
		levelsCleared = wayPointPrefab.gameObject.GetComponent<PointLevel>().levelID;
		levelSelector = (GameObject)Instantiate ( levelSelectorPrefab,
		                                         _curPointPos,
	                                          	  levelSelectorPrefab.transform.rotation);
	}
	// Update is called once per frame
	void Update () 
	{
		levelSelector.transform.position = new Vector3 (_curPointPos.x, 
		                                                _curPointPos.y + Mathf.Cos(Time.time*4)/4);

		if (isInPosition == false)
		{
			move ();
		}

		//Optional
		keyboardThings ();
	}

	protected void move ()
	{
		_curPointPos = Vector3.Lerp (_curPointPos, _nextPointPos, Time.deltaTime);
	}

	public void setPoint(int pointID)
	{
		_nextPoint = pointID;
	}

	protected void keyboardThings()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1))
		{
			setPoint(0);
			isInPosition = false;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2))
		{
			setPoint(1);
			isInPosition = false;
		}
	}
}
