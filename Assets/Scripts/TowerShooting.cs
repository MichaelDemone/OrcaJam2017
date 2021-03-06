﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TowerShooting : MonoBehaviour 
{
	
	public float ProjectileSpeed = 4f;
	public GameObject Bullet;
    public GameObject cannon;

	public float ShootingDelay = 2;

	public float EnemyPositionInSecond = 0.2f;

	public AudioClip ShootClip;

	public ParticleSystem particles;
	
	// Use this for initialization
	void Start ()
	{
		StartCoroutine(Shoot());
	}

	IEnumerator Shoot()
	{
		while (true)
		{
			GameObject enemy = GetComponent<TowerTargeting>().GetCurrentTarget();
			if (enemy == null)
			{
				yield return 0;
				continue;
			}

			ShootBulletAt(enemy.transform.position, enemy.GetComponent<Rigidbody2D>().velocity);
			yield return new WaitForSeconds(ShootingDelay);

		}
	}
	
	private void ShootBulletAt(Vector2 enemy, Vector2 enemyVelocity)
	{
		Vector2 velocity = GetVelocityToKill(transform.position, enemy, enemyVelocity);

		GameObject bullet = Instantiate(Bullet);
        AudioPlayer.PlayFile(ShootClip, 0.5f);
       // bullet.GetComponent<AudioSource>().Play();
		bullet.transform.position = transform.position;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        cannon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		bullet.GetComponent<Rigidbody2D>().velocity = velocity;
		
		particles.Play();
	}

	private Vector2 GetVelocityToKill(Vector2 start, Vector2 enemy, Vector2 enemyVelocity)
	{
		Vector2 destinationPosition = enemy + enemyVelocity * EnemyPositionInSecond;
		
		return (destinationPosition - start).normalized * ProjectileSpeed;
	}
}
