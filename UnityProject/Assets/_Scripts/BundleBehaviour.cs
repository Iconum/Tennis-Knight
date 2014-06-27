using UnityEngine;
using System.Collections;

public class BundleBehaviour : MonoBehaviour {
	public GameObject targetCastle = null;
	public Vector3 targetLocation, startLocation;
	public float lerpTimeLimit = 1.0f;

	protected float _lerpTimer = 0.0f;
	protected CastleRaidHandler castleHandler = null;

	void Update()
	{
		_lerpTimer += Time.deltaTime;
		Vector3.Lerp (startLocation, targetLocation, Mathf.SmoothStep (0.0f, 1.0f, _lerpTimer));
		if (_lerpTimer >= lerpTimeLimit)
		{
			castleHandler.RemoveFromList (gameObject);
			Destroy(gameObject);
		}
	}

	public void GetInitialValues(CastleRaidHandler handler, GameObject castle, Vector3 location, Vector3 position)
	{
		castleHandler = handler;
		targetCastle = castle;
		targetLocation = location;
		startLocation = position;
	}
}
