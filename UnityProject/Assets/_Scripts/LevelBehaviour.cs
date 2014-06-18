using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBehaviour : MonoBehaviour {
	public GameObject topBorder;

	protected List<GameObject> deflectableList = new List<GameObject>();

	public class DeflectableSorter : Comparer<GameObject>
	{
		public Vector3 _targetPosition;

		public override int Compare (GameObject x, GameObject y)
		{
			return (int)Mathf.Round (Vector3.Distance (x.transform.position, _targetPosition) - Vector3.Distance (y.transform.position, _targetPosition));
		}
	}

	public virtual void EnemyDied()
	{

	}

	public void AddToDeflectable(GameObject deflectable)
	{
		deflectableList.Add (deflectable);
	}

	public void RemoveFromDeflectable(GameObject deflectable)
	{
		deflectableList.Remove (deflectable);
	}

	public GameObject FindClosestDeflectable(Vector3 pos)
	{
		if (deflectableList.Count != 0)
		{
			DeflectableSorter sorter = new DeflectableSorter ();
			sorter._targetPosition = pos;
			List<GameObject> templist = deflectableList.FindAll (x => x.transform.position.y > (pos.y - 0.3f));
			templist.Sort (sorter);
			return templist [0];
		} else
			return null;
	}

	public void ToggleWall()
	{
		if (topBorder.CompareTag ("Removal"))
		{
			topBorder.tag = "Border";
			topBorder.collider2D.isTrigger = false;
		} else
		{
			topBorder.tag = "Removal";
			topBorder.collider2D.isTrigger = true;
		}
	}
}

