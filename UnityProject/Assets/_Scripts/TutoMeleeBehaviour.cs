using UnityEngine;
using System.Collections;

public class TutoMeleeBehaviour : MeleeBehaviour {
	public TutorialBehaviour tutoKnight = null;

	protected override void Awake ()
	{
		tutoKnight = GameObject.Find ("TutoKnight").GetComponent<TutorialBehaviour> ();
		base.Awake ();
	}

	 protected override void Update ()
	{
		if (!tutoKnight.tutorialOn && (tutoKnight.currentState == TutorialState.Dodge || tutoKnight.currentState == TutorialState.End))
			base.Update ();
	}

	protected override void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Deflected"))
		{
			if (!specialInvincibility && !spawning)
			{
				DamageHealth ();
			}
		}
		if (other.CompareTag ("Removal"))
		{
			if(tutoKnight.currentState == TutorialState.Dodge)
			{
				tutoKnight.currentState = TutorialState.Villagers;
				tutoKnight.tutorialOn = true;
			}
			Instantiate (pow, transform.position, transform.rotation);
			if (sounds.Count > 0 && audio)
			{
				audio.clip = sounds [1];
				audio.pitch = Random.Range (0.9f, 1.2f);
				audio.Play ();
			}
			InstantDeath ();
		}
	}
}