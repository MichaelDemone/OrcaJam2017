using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerWaveScript : MonoBehaviour {

    int NextWave;
    public float WaveFrequency;
    public float WaveDuration;
    float WaveTimer;

    public float SpawnRateIncreaseFrequency;
    float RateIncreaseTimer;

    public int BossSpawnPoints;
    int BossCounter = 0;

    public GameObject[] Spawners;

    public GameObject WarningText;
    bool WaveWarningSent = false;
    bool BossWarningSent = false;
    float BossWarningDuration = 10f;
    float BossWarningTimer = 0f;

    // Use this for initialization
    void Start () {
        NextWave = Random.Range(0, 3);
        WaveTimer = WaveFrequency;
        RateIncreaseTimer = SpawnRateIncreaseFrequency;

    }
	
	// Update is called once per frame
	void Update () {
        BossWarningTimer -= Time.deltaTime;
        if(BossWarningTimer <= 0)
        {
            WarningText.GetComponent<Text>().text = "";
            BossWarningSent = false;
        }

        WaveTimer -= Time.deltaTime;
        if(WaveTimer <= 10f && !WaveWarningSent)
        {
            if (NextWave == 0)
            {
                WarningText.GetComponent<Text>().text = "Warning! A wave of enemies is approaching the top spawner!";
            } else if (NextWave == 1)
            {
                WarningText.GetComponent<Text>().text = "Warning! A wave of enemies is approaching the middle spawner!";
            } else
            {
                WarningText.GetComponent<Text>().text = "Warning! A wave of enemies is approaching the bottom spawner!";
            }
            WaveWarningSent = true;
            BossWarningSent = false;
        }
        if(WaveTimer <= 0)
        {
            Spawners[NextWave].GetComponent<EnemySpawner>().StartSpawnWave(WaveDuration);
            NextWave = (NextWave + Random.Range(1, 3)) % 3;
            WaveTimer = WaveFrequency;
            if (WaveWarningSent)
            {
                WaveWarningSent = false;
                WarningText.GetComponent<Text>().text = "";
            }
        }

        RateIncreaseTimer -= Time.deltaTime;

        if (RateIncreaseTimer < 0)
        {
            RateIncreaseTimer = SpawnRateIncreaseFrequency;
            for(int i = 0; i < Spawners.Length; i++)
            {
                Spawners[i].GetComponent<EnemySpawner>().IncreaseSpawnRate();
            }
        }

    }

    public void IncreaseBossCounter(int val)
    {
        BossCounter += val;
        if(BossCounter >= BossSpawnPoints)
        {
            WarningText.GetComponent<Text>().text = "Warning! An enemy captain is approaching!";
            BossWarningSent = true;
            WaveWarningSent = false;
            BossWarningTimer = BossWarningDuration;

            BossCounter -= BossSpawnPoints;
            Spawners[2].GetComponent<EnemySpawner>().BossSpawnReady = true;
        }
    }
}
