using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	public GameObject Enemy;
    public GameObject BossEnemy;
    private IEnumerator spawnEnemyloop;

    public float TimeBetweenSpawns = 5f;

    public float SpawnRateMultiplier = 1.0f;

    public bool BossSpawnReady = false;

    bool ReadyPeriod = true;
    public float ReadyPeriodTime = 1.0f;
    // note: in the game, ready periods are set so that spawns are staggered

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
            if (ReadyPeriod)
            {
                yield return new WaitForSeconds(ReadyPeriodTime);
                ReadyPeriod = false;
            } else {
                SpawnEnemy();
                if (RapidSpawnWave)
                {
                    yield return new WaitForSeconds(TimeBetweenSpawns * 0.15f / SpawnRateMultiplier);
                }
                else {
                    yield return new WaitForSeconds(TimeBetweenSpawns / SpawnRateMultiplier);
                }
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
        if (BossSpawnReady)
        {
            Instantiate(BossEnemy, transform.position, Quaternion.identity);
            BossSpawnReady = false;
        } else
        {
            Instantiate(Enemy, transform.position, Quaternion.identity);
        }
	}

    public void IncreaseSpawnRate()
    {
        SpawnRateMultiplier += 0.25f;
        if(SpawnRateMultiplier > 5.0f)
        {
            SpawnRateMultiplier = 5.0f;
        }
    }
}
