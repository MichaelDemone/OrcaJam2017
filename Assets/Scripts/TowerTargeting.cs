using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargeting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private List<GameObject> targets = new List<GameObject>();

	public GameObject GetCurrentTarget()
	{
		if (targets.Count > 0)
		{
			targets[0].GetComponent<TestSideScrollingEnemy>().Highlight();
			return targets[0];
		}
		return null;
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<TestSideScrollingEnemy>() != null)
		{
			targets.Add(other.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<TestSideScrollingEnemy>() != null)
		{
			targets.Remove(other.gameObject);
			other.GetComponent<TestSideScrollingEnemy>().UnHighlight();
		}
	}
}
