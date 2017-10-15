using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpauseButton : MonoBehaviour
{

    public GameObject player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonPressed()
    {
        player.GetComponent<PlayerController>().Unpause();
    }
}
