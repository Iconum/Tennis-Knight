using UnityEngine;
using System.Collections;

public class VillagerBehaviour : MonoBehaviour {

	public VillagerHandler handler;

	public float flyingHeight = 2.0f;
	public float deathTime = 2.0f;
	public float rotationSpeed = 5.0f;
	public float flyingSpeed = 5.0f;
	protected float rotation = 0.0f;
	protected float height = 0.0f;
	protected bool isDead = false;
	protected bool isGoingup = true;
	// Use this for initialization
	void Start () {
		handler = GameObject.Find ("VillagerManager").GetComponent<VillagerHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isDead) {
			death ();
		}
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.CompareTag ("Deflectable") && isDead == false) {
			handler.spawnPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
			handler.spawnPositions.Add(handler.spawnPos);
			isDead = true;
		}
		
		Destroy(collision.gameObject);
	}

	protected virtual void death()
	{
		if (deathTime > 0)
		{
			if(gameObject.transform.position.y < flyingHeight && isGoingup == true){
				height = flyingSpeed/100;
			}
			else {
				isGoingup = false;
			}
			if (isGoingup == false){
				height = -flyingSpeed/50;
			}


			//gameObject.transform.rotation = new Quaternion(0,0,1,rotation);
			gameObject.transform.Rotate( new Vector3(0.0f,0.0f,rotation));
			gameObject.transform.position += new Vector3 (flyingSpeed/500, height);
			deathTime -= Time.deltaTime;
			rotation += Time.deltaTime*rotationSpeed;
		} 
		else {
			handler.Delete(gameObject);
			Destroy(gameObject);
		}

	}
}
