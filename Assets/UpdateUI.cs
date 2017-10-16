using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{

    public Sprite[] Numbers;

    public Image Matey1;
    public Image Matey2;
    public Image TowerMax;
    public Image TowerCurrent;
    public Image Enemy1;
    public Image Enemy2;
    public Image Enemy3;
    
    public GameObject mateys;
    public int NumEnemies = 0;
    public GameObject player;

    public Scoreboard Scoreboard;
    
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

        SetNumbers(mateysHP, Matey1, Matey2);
        TowerMax.sprite = Numbers[Mathf.Clamp(maxTowers, 0, 9)];
        TowerCurrent.sprite = Numbers[Mathf.Clamp(numTowers, 0, 9)];
        SetNumbers(Scoreboard.score, Enemy1, Enemy2, Enemy3);
    }

    void SetNumbers(int number, Image firstDigitSprite, Image secondDigitSprite, Image thirdDigitSprite = null)
    {
        if (thirdDigitSprite == null)
        {
            number = Mathf.Clamp(number, 0, 99);

            int firstDigit = Mathf.FloorToInt(number / 10f);
            int secondDigit = number % 10;

            firstDigitSprite.sprite = Numbers[firstDigit];
            secondDigitSprite.sprite = Numbers[secondDigit];
        }
        else
        {
            number = Mathf.Clamp(number, 0, 999);

            int firstDigit = Mathf.FloorToInt(number / 100f);
            int secondDigit = Mathf.FloorToInt(number / 10f) % 10;
            int thirdDigit = number % 10;

            firstDigitSprite.sprite = Numbers[firstDigit];
            secondDigitSprite.sprite = Numbers[secondDigit];
            thirdDigitSprite.sprite = Numbers[thirdDigit];
        }
    }
}
