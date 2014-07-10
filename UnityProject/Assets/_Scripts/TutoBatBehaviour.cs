using UnityEngine;
using System.Collections;

public class TutoBatBehaviour : RangedBehaviour {
	public TutorialBehaviour tutoKnight = null;

	protected bool _shotOne = false;
	protected float _lerper = 0.0f;
	protected Vector3 _lerpStart;

	protected override void Awake ()
	{
		tutoKnight = GameObject.Find ("TutoKnight").GetComponent<TutorialBehaviour> ();
		base.Awake ();
	}

	protected override void Update ()
	{
		if (!tutoKnight.tutorialOn && (tutoKnight.currentState == TutorialState.DragSwing || tutoKnight.currentState == TutorialState.FreeSwing || tutoKnight.currentState == TutorialState.KeySwing ||
			tutoKnight.currentState == TutorialState.OppositeSwing || tutoKnight.currentState == TutorialState.TiltSwing))
		{
			if (spawning)
			{
				base.Update ();
				_lerpStart = transform.position;
			} else if (transform.position.x == tutoKnight.transform.position.x)
			{
				Vector3.Lerp (_lerpStart, targetLocation, _lerper);
			} else if (!_shotOne)
			{
				ShootProjectile (new Vector3 (0.0f, 1.0f));
				_shotOne = true;
			}
		}
		if (!tutoKnight.tutorialOn && tutoKnight.currentState == TutorialState.End)
			base.Update ();
	}
}
