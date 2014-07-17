using UnityEngine;
using System.Collections;

public class PaddleBehaviour : MonoBehaviour {
	public GameObject player;
	public bool isLeft;

	private Vector3 _startPosition;
	private Quaternion _startRotation;
	private int _deflectedLayer;
	private float _hitTime;
	private bool _hitting = false;

	// Use this for initialization
	void Start () {
		_startPosition = transform.localPosition;
		_startRotation = transform.localRotation;
		_deflectedLayer = LayerMask.NameToLayer ("OwnProjectiles");
		Hitting (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (_hitting)
		{
			_hitTime += Time.fixedDeltaTime;
			if (isLeft)
			{
				rigidbody2D.velocity = new Vector2 (7.0f, 9.0f);
				rigidbody2D.angularVelocity = -640.0f;
			} else
			{
				rigidbody2D.velocity = new Vector2 (-7.0f, 9.0f);
				rigidbody2D.angularVelocity = 640.0f;
			}
			if (_hitTime > 0.3f)
				PaddleDisable ();
		}
	}

	public void PaddleHit()
	{
		Hitting (true);
	}

	void PaddleDisable ()
	{
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0.0f;
		transform.localPosition = _startPosition;
		transform.localRotation = _startRotation;
		_hitTime = 0.0f;
		player.GetComponent<PlayerBehaviour> ().visualRacket.SetActive (true);
		player.GetComponent<PlayerBehaviour> ().paddleActive = false;
		Hitting (false);
	}

	void Hitting(bool hit)
	{
		_hitting = hit;
		renderer.enabled = hit;
		collider2D.enabled = hit;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Deflectable"))
		{
			collision.gameObject.tag = "Deflected";
			collision.gameObject.layer = (LayerMask)_deflectedLayer;
		} else if (collision.gameObject.CompareTag ("AllDamaging"))
		{
			collision.gameObject.layer = (LayerMask)_deflectedLayer;
		}
	}
}
