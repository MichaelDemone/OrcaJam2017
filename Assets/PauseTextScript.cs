using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseTextScript : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(player.GetComponent<PlayerController>().paused)
        {
            GetComponent<Text>().enabled = true;
        } else
        {
            GetComponent<Text>().enabled = false;
        }
	}
}
