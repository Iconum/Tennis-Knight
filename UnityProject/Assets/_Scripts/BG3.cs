using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BG3 : BGLoop {
	public GameObject propPrefab1, propPrefab2, propPrefab3;
	public bool asd;

	private Vector3 ps;
	private float randvar=5;

	private float cheight,cwidth;
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		asd = true;

		cheight  = 2 * Camera.main.orthographicSize;
		cwidth = cheight * Camera.main.aspect;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		//GameObject tempo;

		if (!stopped)
		{
			if (asd == true)
			{
				asd = false;
				GameObject tempo = (GameObject)Instantiate (propPrefab1, transform.position + new Vector3 (Random.Range (-2.5f, 2.5f), cheight / 2), transform.rotation);
				if (transform.position.y <= -10)
				{
					Destroy (tempo);
					//asd=true;
					Debug.Log ("lol");

				}
			}
		}
		//Debug.Log (asd);

		//GameObject tempo = (GameObject)Instantiate (propPrefab1, new Vector3(0,randvar-=deltaspeed), transform.rotation);
		//tempo.transform.position+=new Vector3(0,deltaspeed);

	
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
