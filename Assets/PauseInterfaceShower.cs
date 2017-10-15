using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseInterfaceShower : MonoBehaviour {

    public GameObject PauseInterface;
    public GameObject Player;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Player.GetComponent<PlayerController>().paused)
        {
            PauseInterface.SetActive(true);
        } else
        {
            PauseInterface.SetActive(false);
        }
	}
}
