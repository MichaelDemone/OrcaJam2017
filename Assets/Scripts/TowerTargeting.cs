using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargeting : MonoBehaviour {
    public GameObject cannon;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject target = GetCurrentTarget();
      
		if(target != null)
        {
            if (target.transform.position.x < gameObject.transform.position.x)
            {
                Vector3 scale = transform.localScale;
                scale.x = -1 * Math.Abs(scale.x);
                transform.localScale = scale;
                cannon.transform.localScale = scale;
            } else
            {
                Vector3 scale = transform.localScale;
                scale.x = 1 * Math.Abs(scale.x);
                transform.localScale = scale;
                cannon.transform.localScale = scale;
            }
        } else
        {
            Vector3 scale = transform.localScale;
            scale.x = 1 * Math.Abs(scale.x);
            transform.localScale = scale;
            cannon.transform.localScale = scale;
        }
	}
	
	private List<GameObject> targets = new List<GameObject>();

	public GameObject GetCurrentTarget()
	{
		if (targets.Count > 0)
		{
			return targets[0];
		}
		return null;
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Enemy>() != null)
		{
			targets.Add(other.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<Enemy>() != null)
		{
			targets.Remove(other.gameObject);
		}
	}
}
