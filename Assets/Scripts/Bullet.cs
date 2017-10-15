using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float TimeBeforeDestroy = 3f;
	public float ExplosionRadius = 5;
    public int damage;

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
			Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

            other.gameObject.GetComponent<Enemy>().TalkShitGetHit(damage);
			Destroy(gameObject);
		}
		else if (other.gameObject.layer == 8)
		{
			Destroy(gameObject);
		}
	}
}
