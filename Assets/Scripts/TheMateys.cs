using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMateys : MonoBehaviour {

    public int mateysTotalHP = 10;
    public int mateysCurrentHP = 0;
    // Use this for initialization
    void Start() {
        //ARRRR MATEYS
        mateysCurrentHP = mateysTotalHP;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Enemy")){
            //print("They are attacking your mateys!");
            collision.gameObject.GetComponent<EnemyAI>().Die();
        }
    }

    private void ArrMateysBeenHurtRealBad(int damage) {
        print("THEY BE HURTIN YOUR MATEYS");
        if (mateysCurrentHP > damage) {
            mateysCurrentHP = mateysCurrentHP - damage;
            print("YOU ONLY HAVE " + mateysCurrentHP + " MATEYS LEFT");
        } else
            ArrTheyKilledTheMateys();
    }

    private void ArrTheyKilledTheMateys() {
        print("THEY KILLED ALL YOUR MATEYS");
    }
}


