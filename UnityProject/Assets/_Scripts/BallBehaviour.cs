using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour {
	public Vector2 startVelocity = new Vector2(0.1f, -1.0f);
	public float constantSpeed = 2.0f;

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = constantSpeed * startVelocity.normalized;
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = constantSpeed * rigidbody2D.velocity.normalized;
	}
}
