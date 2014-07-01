using UnityEngine;
using System.Collections;

public class BGLoop : MonoBehaviour {

	public float speed = 5;
	public static BGLoop current;

	float pos = 0;
	// Use this for initialization
	void Start () {
		current = this;
	
	}
	
	// Update is called once per frame
	void Update () {
		var deltaspeed = speed * Time.deltaTime;
		pos += deltaspeed;
		if (pos > 1.0f)
			pos -= 1.0f;
	
		renderer.material.mainTextureOffset = new Vector2 (0, pos);
	}
}
