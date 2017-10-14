using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public float ExplosionRadius = 5;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<TestSideScrollingEnemy>() != null)
		{
			print("Hit!");
			Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
			foreach (var col in cols)
			{
				TestSideScrollingEnemy test;
				if ((test = col.GetComponent<TestSideScrollingEnemy>()) != null)
				{
					test.Hit();
				}
			}
			transform.position = new Vector3(100, 100);
		}
	}
}
