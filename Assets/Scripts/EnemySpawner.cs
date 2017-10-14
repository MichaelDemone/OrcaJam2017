using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	public GameObject Enemy;
	private IEnumerator spawnEnemyloop;

	public float TimeBetweenSpawns = 3f;
	
	public void StopSpawningEnemies()
	{
		StopCoroutine(spawnEnemyloop);
	}

	public void StartSpawningEnemies()
	{
		StartCoroutine(spawnEnemyloop);
	}
	
	private void Start()
	{
		spawnEnemyloop = SpawnEnemyLoop();
		StartSpawningEnemies();
	}

	IEnumerator SpawnEnemyLoop()
	{
		while (true)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(TimeBetweenSpawns);
		}
	}

	
	public void SpawnEnemy()
	{
		Instantiate(Enemy, transform.position, Quaternion.identity);
	}
}
