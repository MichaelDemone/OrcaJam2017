using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TowerShooting : MonoBehaviour 
{
	
	public float ProjectileSpeed = 4f;
	public GameObject Bullet;
    private GameObject lastBullet;


    public AudioSource shoot, hit;

	public float ShootingDelay = 2;

	public float EnemyPositionInSecond = 0.2f;
	
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

    private void Update() {
        if (lastBullet != null) {
            if (lastBullet.GetComponent<Bullet>().destroyed) {
                hit.Play();
            }

                    }
    }

   
	
	private void ShootBulletAt(Vector2 enemy, Vector2 enemyVelocity)
	{
		Vector2 velocity = GetVelocityToKill(transform.position, enemy, enemyVelocity);

      
		GameObject bullet = Instantiate(Bullet);
        lastBullet = bullet;
        shoot.Play();
       // bullet.GetComponent<AudioSource>().Play();
		bullet.transform.position = transform.position;
		bullet.GetComponent<Rigidbody2D>().velocity = velocity;
	}

	private Vector2 GetVelocityToKill(Vector2 start, Vector2 enemy, Vector2 enemyVelocity)
	{
		Vector2 destinationPosition = enemy + enemyVelocity * EnemyPositionInSecond;
		
		return (destinationPosition - start).normalized * ProjectileSpeed;
	}
}
