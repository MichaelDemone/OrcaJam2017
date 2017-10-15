using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{

    public GameObject mateys;
    public int NumEnemies = 0;
    public GameObject player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int mateysHP = mateys.GetComponent<TheMateys>().mateysCurrentHP;
        int numTowers = player.GetComponent<PlayerTowerInteractions>().NumberOfTowersPlaced;
        int maxTowers = player.GetComponent<PlayerTowerInteractions>().MaxNumberOfTowers;
        gameObject.GetComponent<Text>().text = mateysHP + "\n" +
            numTowers + "/" + maxTowers + "\n"
            + NumEnemies;
    }
}
