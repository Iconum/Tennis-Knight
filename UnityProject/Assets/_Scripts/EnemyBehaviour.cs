using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectilePrefab = null, player = null;
	public float shootTimerLimit = 1.0f;

	private float _shootTimer = 0.0f;

	// Use this for initialization
	void Start () {
		GameObject.Instantiate (projectilePrefab, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(-player.transform.position.x, transform.position.y);

		_shootTimer += Time.deltaTime;
		if (_shootTimer > shootTimerLimit)
		{
			_shootTimer = 0.0f;// shootTimerLimit;
			GameObject.Instantiate (projectilePrefab, transform.position, transform.rotation);
		}
	}

}
