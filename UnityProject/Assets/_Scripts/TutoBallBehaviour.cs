using UnityEngine;
using System.Collections;

public class TutoBallBehaviour : BallBehaviour {
	public TutorialBehaviour tutoBehaviour = null;
	public TutoBatBehaviour batBehaviour = null;

	public void SetInitials(TutorialBehaviour tutor, TutoBatBehaviour bat)
	{
		tutoBehaviour = tutor;
		batBehaviour = bat;
	}

	protected void OnCollisionEnter2D(Collision2D collision) // Slightly spaghetti code to change the tutorial state at appropriate times
	{
		if (collision.gameObject.CompareTag ("Villager"))
		{
			if (tutoBehaviour.currentState != TutorialState.End && tutoBehaviour.currentState != TutorialState.Dodge)
				batBehaviour.shotOne = false;
			else if (tutoBehaviour.currentState == TutorialState.Dodge)
			{
				tutoBehaviour.currentState = TutorialState.Villagers;
				tutoBehaviour.tutorialOn = true;
			}
		} else if (collision.gameObject.CompareTag ("Enemy") || collision.gameObject.CompareTag ("Removal"))
		{
			if (tutoBehaviour.currentState == TutorialState.DragSwing || tutoBehaviour.currentState == TutorialState.FreeSwing || tutoBehaviour.currentState == TutorialState.KeySwing ||
				tutoBehaviour.currentState == TutorialState.OppositeSwing || tutoBehaviour.currentState == TutorialState.TiltSwing) // If any of the swings is the state
			{
				tutoBehaviour.currentState = TutorialState.Tanking;
				tutoBehaviour.tutorialOn = true;
				batBehaviour.shotOne = false;
			}
		} else if (collision.gameObject.CompareTag ("Player"))
		{
			if (tutoBehaviour.currentState != TutorialState.Tanking) // To make sure there are always balls in the air
			{
				batBehaviour.shotOne = false;
			}
		}
	}

	protected override void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Removal"))
		{
			if (tutoBehaviour.currentState == TutorialState.DragSwing || tutoBehaviour.currentState == TutorialState.FreeSwing || tutoBehaviour.currentState == TutorialState.KeySwing ||
			    tutoBehaviour.currentState == TutorialState.OppositeSwing || tutoBehaviour.currentState == TutorialState.TiltSwing) // If any of the swings is the state
			{
				tutoBehaviour.currentState = TutorialState.Tanking;
				tutoBehaviour.tutorialOn = true;
				batBehaviour.shotOne = false;
			}
			Destroy (gameObject);
		}
	}
}
