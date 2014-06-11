using UnityEngine;
using System.Collections;

public class PaddleBehaviour : MonoBehaviour {
	public GameObject player;
	public bool isLeft;

	private Vector3 startPosition;
	private Quaternion startRotation;
	private float hitTime = 0.0f;

	// Use this for initialization
	void Start () {
		startPosition = transform.localPosition;
		startRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		hitTime += Time.deltaTime * 5;
		transform.localPosition = Vector3.Lerp (startPosition, startPosition + new Vector3 (0.0f, 0.25f), hitTime);
		if (isLeft)
		{
			transform.localRotation = Quaternion.Lerp(startRotation, Quaternion.Euler(0.0f, 0.0f, -35.0f), hitTime);
		} else
		{
			transform.localRotation = Quaternion.Lerp(startRotation, Quaternion.Euler(0.0f, 0.0f, 35.0f), hitTime);
		}
	}

	public void PaddleHit()
	{
		StartCoroutine (PaddleDisable ());
	}

	IEnumerator PaddleDisable ()
	{
		yield return new WaitForSeconds (0.2f);
		transform.localPosition = startPosition;
		transform.localRotation = startRotation;
		hitTime = 0.0f;
		player.GetComponent<PlayerBehaviour>().paddleActive = false;
		gameObject.SetActive (false);
	}
}
