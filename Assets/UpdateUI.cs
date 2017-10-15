using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{

    public GameObject mateys;
    public int NumEnemies = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "Mateys: " + mateys.GetComponent<TheMateys>().mateysCurrentHP + "\n" +
            "Towers: " + 0 + "\n" + "Enemies: " + NumEnemies;
    }
}
