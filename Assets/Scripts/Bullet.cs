using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public float ExplosionRadius = 5;
    public int damage = 3;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<EnemyAI>() != null)
		{
			print("Hit!");
			Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
			foreach (var col in cols)
			{
				EnemyAI test;
				if ((test = col.GetComponent<EnemyAI>()) != null)
				{
					test.TalkShitGetHit(damage);
				}
			}
			Destroy(gameObject);
		}
		else if (other.gameObject.layer == 8)
		{
			Destroy(gameObject);
		}
	}
}
