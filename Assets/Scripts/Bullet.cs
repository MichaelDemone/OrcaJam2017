using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float TimeBeforeDestroy = 3f;
	public float ExplosionRadius = 5;
    public int RegularDamage;
	public int SplashDamage;


	public AudioClip ExplodeClip;

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
		Enemy hitEnemy;
		if ((hitEnemy = other.GetComponent<Enemy>()) == null && other.gameObject.layer != 8) return;
		
		
		if (hitEnemy != null)
		{
			hitEnemy.TalkShitGetHit(RegularDamage);
		}
		
		Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

		var enemyCols = cols.Select(col => col.GetComponent<Enemy>());
		Vector2 thisPos = transform.position;

		foreach (var enemy in enemyCols)
		{
			if (enemy == hitEnemy || enemy == null) continue;
				
			Vector2 enemyPos = enemy.gameObject.transform.position;
			float dist = (enemyPos - thisPos).magnitude;
			if (dist < 1) dist = 1;
				
			enemy.TalkShitGetHit(Mathf.RoundToInt((1 / dist) * SplashDamage));
			print("hit enemy with splash");
		}
            
		AudioPlayer.PlayFile(ExplodeClip, 0.2f);
		Destroy(gameObject);
	}
}
