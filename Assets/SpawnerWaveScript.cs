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

    public GameObject[] Spawners;

    public GameObject WarningText;
    bool WarningSent = false;

    // Use this for initialization
    void Start () {
        NextWave = Random.Range(0, 3);
        WaveTimer = WaveFrequency;
        RateIncreaseTimer = SpawnRateIncreaseFrequency;

    }
	
	// Update is called once per frame
	void Update () {
        WaveTimer -= Time.deltaTime;
        if(WaveTimer <= 10f && WarningText != WarningSent)
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
            WarningSent = true;
        }
        if(WaveTimer <= 0)
        {
            Spawners[NextWave].GetComponent<EnemySpawner>().StartSpawnWave(WaveDuration);
            NextWave = (NextWave + Random.Range(1, 3)) % 3;
            WaveTimer = WaveFrequency;
            WarningSent = false;
            WarningText.GetComponent<Text>().text = "";
        }

        RateIncreaseTimer -= Time.deltaTime;

        if(RateIncreaseTimer < 0)
        {
            RateIncreaseTimer = SpawnRateIncreaseFrequency;
            for(int i = 0; i < Spawners.Length; i++)
            {
                Spawners[i].GetComponent<EnemySpawner>().IncreaseSpawnRate();
            }
        }

    }
}
