using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGLoop : MonoBehaviour {
	public bool stopped = false, isSubBackground = false;
	public float speed = 5, brakeTime = 3.0f;
	public static BGLoop current;
	public List<BGLoop> subBackground = new List<BGLoop>();
	
	protected float _actualSpeed = 5.0f;
	protected float pos = 0;

	// Use this for initialization
	protected virtual void Start () {
		if (!isSubBackground)
			current = this;
		_actualSpeed = speed;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
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
