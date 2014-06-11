using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour
{
	public GameObject leftPaddle = null, rightPaddle = null, ballPrefab;
	public bool paddleActive = false, stunned = false;
	public float strafeSpeed = 0.1f;

	private List<GameObject> _collisionList = new List<GameObject> ();
	private float currentSpeed;
	private float sixty = 60.0f, eighty = 80.0f, onefourty = 140.0f;

	// Use this for initialization
	void Start ()
	{
		sixty = (60.0f / 640.0f) * Screen.height;
		eighty = (80.0f / 400.0f) * Screen.width;
		onefourty = (140.0f / 640.0f) * Screen.height;
	}
	
	// Update is called once per frame
	void Update ()
	{
		currentSpeed = strafeSpeed;
		if ((Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) && !stunned)
		{
			currentSpeed *= 3;
		} else if (stunned)
		{
			currentSpeed *= 0.2f;
		}

		if (Input.GetKey (KeyCode.RightArrow))
		{
			transform.position += new Vector3 (currentSpeed, 0.0f);
		}
		if (Input.GetKey (KeyCode.LeftArrow))
		{
			transform.position -= new Vector3 (currentSpeed, 0.0f);
		}
		if (Input.GetKeyDown (KeyCode.Z))
		{
			PaddleActivate (leftPaddle);
		}
		if (Input.GetKeyDown (KeyCode.X))
		{
			PaddleActivate (rightPaddle);
		}
//#if UNITY_ANDROID
		float tempf = 0.0f;
		tempf = Mathf.Clamp(Input.acceleration.x / 5.0f, -currentSpeed, currentSpeed);
		transform.position += new Vector3(tempf, 0.0f);
//#endif
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -3.0f, 3.0f), -2.5f);


		//Debug näppäimet
		if (Input.GetKeyDown (KeyCode.Space))
		{
			GameObject.Instantiate(ballPrefab, transform.position + new Vector3(0.0f, 10.0f), transform.rotation);
		}
	}

	void PaddleActivate (GameObject paddle)
	{
		if (!paddleActive)
		{
			paddleActive = true;
			paddle.SetActive (true);
			paddle.GetComponent<PaddleBehaviour>().PaddleHit();
		}
	}

	IEnumerator StunRemove()
	{
		yield return new WaitForSeconds (3.0f);
		stunned = false;
	}

	void OnGUI()
	{
		if (GUI.RepeatButton (new Rect (0, Screen.height-sixty, eighty, sixty), "<----"))
		{
			transform.position -= new Vector3 (currentSpeed, 0.0f);
		}
		if (GUI.RepeatButton (new Rect (Screen.width-eighty, Screen.height-sixty, eighty, sixty), "---->"))
		{
			transform.position += new Vector3 (currentSpeed, 0.0f);
		}

		if (GUI.Button (new Rect (0, Screen.height-onefourty, eighty, sixty), "Vasen"))
		{
			PaddleActivate(leftPaddle);
		}
		if (GUI.Button (new Rect (Screen.width-eighty, Screen.height-onefourty, eighty, sixty), "Oikea"))
		{
			PaddleActivate(rightPaddle);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Deflectable"))
		{
			stunned = true;
			StartCoroutine(StunRemove ());
			Destroy(collision.gameObject);
		}
	}
}
