using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PropBehaviour : MonoBehaviour {

	public BG3 bg;
	public float pspeed,cheight,cwidth;
	public Sprite[] textures;
	// Use this for initialization
	private SpriteRenderer rend;
	void Start ()
	{
		bg = GameObject.Find ("Stage3BG").GetComponent<BG3> ();
		pspeed = bg.GetComponent<BG3> ().speed;

		cheight  = 2 * Camera.main.orthographicSize;
		cwidth = cheight * Camera.main.aspect;

		rend = GetComponent<SpriteRenderer> ();
		var tex = textures [Random.Range (0, textures.Length)];
		if(tex != null)
			rend.sprite = tex;
	}
	
	// Update is called once per frame
	void Update ()
	{

		//renderer


		var deltaspeed = pspeed * Time.deltaTime*11;
		//Debug.Log (deltaspeed);
		gameObject.transform.position += new Vector3 (0,-deltaspeed);

		if (gameObject.transform.position.y <= -cheight/2)
		{
			Destroy(gameObject);
			bg.GetComponent<BG3> ().asd = true;
		}
	}
}