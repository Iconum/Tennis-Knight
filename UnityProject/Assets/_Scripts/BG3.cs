using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BG3 : MonoBehaviour {
	public bool stopped = false, isSubBackground = false;
	public float speed = 5, brakeTime = 3.0f;
	public static BG3 current;
	public List<BG3> subBackground = new List<BG3>();

	public GameObject propPrefab1, propPrefab2, propPrefab3;
	
	protected float _actualSpeed = 5.0f;
	private Vector3 ps;

	float pos = 0;
	// Use this for initialization
	void Start () {
		if (!isSubBackground)
			current = this;
		_actualSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (stopped && _actualSpeed > 0.0f)
		{
			_actualSpeed -= Time.deltaTime * speed / brakeTime;
			if (_actualSpeed < 0.0f)
				_actualSpeed = 0.0f;
		} else if (!stopped && _actualSpeed < speed)
		{
			_actualSpeed += Time.deltaTime * speed / brakeTime;
			if (_actualSpeed > speed)
				_actualSpeed = speed;
		}
		var deltaspeed = _actualSpeed * Time.deltaTime;
		pos += deltaspeed;
		if (pos > 1.0f)
			pos -= 1.0f;
	
		renderer.material.mainTextureOffset = new Vector2 (0, pos);

		/*GameObject tempo = (GameObject)Instantiate (propPrefab1,transform.position,transform.rotation);
		Vector3 temp = tempo.transform.position;
		temp.y += deltaspeed;
		tempo.transform.position = temp;
		//temp.transform.position.x = ps+=deltaspeed;
		*/
	}

	public void ToggleStop()
	{
		stopped = !stopped;
		for (int j = 0; j < subBackground.Count; ++j)
		{
			subBackground [j].ToggleStop ();
		}
	}
}
