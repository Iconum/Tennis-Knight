using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBehaviour : MonoBehaviour {
	protected List<GameObject> deflectableList = new List<GameObject>();

	public class DeflectableSorter : Comparer<GameObject>
	{
		public Vector3 _targetPosition;

		public override int Compare(GameObject x, GameObject y)
		{
			if (x.rigidbody2D.velocity.y < 0 || x.rigidbody2D.velocity.y < 0)
			{
				return (int)Mathf.Round(x.rigidbody2D.velocity.y - x.rigidbody2D.velocity.y);
			} else
			{
				return (int)Mathf.Round(Vector3.Distance(x.transform.position, _targetPosition) - Vector3.Distance(y.transform.position, _targetPosition));
			}
		}
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
			deflectableList.Sort (sorter);
			return deflectableList[0];
		} else
			return null;
	}
}

