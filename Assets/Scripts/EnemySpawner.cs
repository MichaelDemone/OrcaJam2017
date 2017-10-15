using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	public GameObject Enemy;
	private IEnumerator spawnEnemyloop;

	public float TimeBetweenSpawns = 5f;

    public bool RapidSpawnWave = false;
    float SpawnWaveTimer;
	
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

    private void Update()
    {
        if(RapidSpawnWave)
        {
            SpawnWaveTimer -= Time.deltaTime;
            if(SpawnWaveTimer < 0)
            {
                RapidSpawnWave = false;
            }
        }
    }

	IEnumerator SpawnEnemyLoop()
	{
        while (true)
        {
            SpawnEnemy();
            if (RapidSpawnWave) {
                yield return new WaitForSeconds(TimeBetweenSpawns * 0.15f);
            } else {
                yield return new WaitForSeconds(TimeBetweenSpawns);
            }
		}
	}

    public void StartSpawnWave(float t)
    {
        StopSpawningEnemies();
        RapidSpawnWave = true;
        SpawnWaveTimer = t;
        StartSpawningEnemies();
    }

	
	public void SpawnEnemy()
	{
		Instantiate(Enemy, transform.position, Quaternion.identity);
	}
}
