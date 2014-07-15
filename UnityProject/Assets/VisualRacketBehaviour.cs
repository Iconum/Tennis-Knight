using UnityEngine;
using System.Collections;

public class VisualRacketBehaviour : MonoBehaviour {

	public GameObject leftPaddle = null, rightPaddle = null, player = null;
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		/*if (player.GetComponent<GraphicsPlayerBehaviour> ().paddleActive == true)
		{
			//gameObject.SetActive (false);
			HitIt();
		} 

		if (player.GetComponent<GraphicsPlayerBehaviour> ().paddleActive == false)
		{
			//Debug.Log("lol");
			gameObject.SetActive (true);
			//HitIt();
		}

	}

	public void HitIt()
	{
		StartCoroutine (notAgain ());
	}
	
	IEnumerator notAgain ()
	{
		yield return new WaitForSeconds (0.0f);
		gameObject.SetActive (false);
	}*/
	}
}
