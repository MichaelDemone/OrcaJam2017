using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float TimeBeforeDestroy = 3f;
	public float ExplosionRadius = 5;
    public int damage = 3;

	void Start()
	{
		StartCoroutine(AutoDestroy());
	}
	
	IEnumerator AutoDestroy()
	{
		yield return new WaitForSeconds(TimeBeforeDestroy);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<Enemy>() != null)
		{
			print("Hit!");
			Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
			foreach (var col in cols)
			{
				Enemy test;
				if ((test = col.GetComponent<Enemy>()) != null)
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
