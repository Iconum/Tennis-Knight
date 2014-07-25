using UnityEngine;
using System.Collections;


	// Use this for initialization
public class MenuMusic : MonoBehaviour {
	
	private static MenuMusic instance = null;
	public static MenuMusic Instance {
		get { return instance; }
	}
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	void Update()
	{
		audio.volume = Statics.musicVolume;
	}
	
	// any other methods you need
}