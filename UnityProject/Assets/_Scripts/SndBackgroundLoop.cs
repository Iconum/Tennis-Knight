using UnityEngine;
using System.Collections;

public class SndBackgroundLoop : MonoBehaviour {

	//public GameObject bg1 = null;
	public GameObject bg1= null, bg2=null,ol1=null, ol2=null;
	public GameObject omacamera = null;
	public float bgspeed = -3f;
	private Vector3 startPosition;
	private Vector3 cpos, bg1pos, bg2pos,bg2bs;
	private float chmax,chmin;

	private float cheight, cwidth;

	// Use this for initialization
	void Start ()
	{
		cheight  = 2 * Camera.main.orthographicSize;
		cwidth = cheight * Camera.main.aspect;

		startPosition = transform.localPosition;
		cpos = omacamera.transform.localPosition;
		bg1pos = bg1.transform.localPosition;
		bg2pos = bg2.transform.localPosition;

		//ch = Camera.main.pixelHeight;
		//cw = Camera.main.pixelWidth;
		//chmin = Camera.main.pixelRect;
		chmax= Camera.main.pixelWidth;
		bg2bs = bg2.renderer.bounds.size;
		//Debug.Log (Camera.main.pixelRect);
		//Debug.Log (chmax);
		Debug.Log (cheight);
		Debug.Log (cwidth);
	}
	
	// Update is called once per frame
	void Update ()
	{
		var deltaspeed = bgspeed * Time.deltaTime;
		//transform.position = new Vector3(transform.position.x, transform.position.y+=bgspeed, transform.position.z);
		bg1.transform.position += new Vector3 (0,deltaspeed);
		bg2.transform.position += new Vector3 (0,deltaspeed);

		//ol1.transform.position += new Vector3 (0,deltaspeed);
		//ol2.transform.position += new Vector3 (0,deltaspeed);

		//Debug.Log (bg2.transform.position);



		if (bg1.transform.position.y <= -cheight)
		{
			bg1.transform.position = new Vector3(0,cheight);
			//bg1.transform.position += new Vector3 (0,deltaspeed);
		}

		if (bg2.transform.position.y <= -cheight)
		{
			bg2.transform.position = new Vector3(0,cheight);
			//bg2.transform.position += new Vector3 (0,deltaspeed);
		}

		/*
		if (ol2.transform.position.y <= -cheight)
		{
			ol2.transform.position = new Vector3(ol2.transform.position.x,cheight);
			//bg2.transform.position += new Vector3 (0,deltaspeed);
		}
		
		if (ol1.transform.position.y <= -cheight)
		{
			ol1.transform.position = new Vector3(ol1.transform.position.x,cheight);
			//bg1.transform.position += new Vector3 (0,deltaspeed);
		}*/
	}
}
