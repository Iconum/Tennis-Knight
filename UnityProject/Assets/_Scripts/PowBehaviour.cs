using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowBehaviour : MonoBehaviour {
	public float destroyTime = 0.5f;
	public List<Sprite> effectList = new List<Sprite> ();

	private Vector3 startPosition;

	void Awake()
	{
		GetComponent<SpriteRenderer> ().sprite = effectList [Random.Range (0, effectList.Count)];
		startPosition = transform.position;
		Destroy (gameObject, destroyTime);
	}

	void Update()
	{
		transform.position = startPosition + new Vector3 (Random.Range (-0.2f, 0.2f), Random.Range (-0.2f, 0.2f));
	}
}
