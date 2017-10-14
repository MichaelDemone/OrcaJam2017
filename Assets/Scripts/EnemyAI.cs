﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public float moveVelocity = 4f;
    public int jumpVelocity = 10;
    private Rigidbody2D body;
    private bool touchingGround = false;
    private bool aboutToFall = false;
    public int jumpSpeedMod = 2;

    public Transform jumpPos, fallPos;
    RaycastHit2D[] jumpRay, fallRay;

    public int enemyMaxHP = 10;
    public int enemyCurrentHP;

    // Use this for initialization
    #region Starts

    private void Awake() {

        body = GetComponent<Rigidbody2D>();
    }

    void Start() {

        enemyCurrentHP = enemyMaxHP;
    }

    #endregion

    // Update is called once per frame
    void Update() {

        GetRays();


        if (touchingGround) {
            body.velocity = new Vector2(-moveVelocity, 0);
            if (CheckJump()) {
                if (WillJump(randomNumber()) && !aboutToFall) {
                    Jump();
                } else {
                    aboutToFall = true;
                }
            }
            if (CheckFall()) {
                Fall();
            }
        } 
    }

    private void GetRays() {
        jumpRay = Physics2D.RaycastAll(jumpPos.position, Vector2.zero);
        fallRay = Physics2D.RaycastAll(fallPos.position, Vector2.zero);

    }

    private bool CheckJump() {

        return !jumpRay.Any(ray => ray.collider != null && ray.collider.CompareTag("Ground"));
        
    }

    private bool CheckFall() {
        return !fallRay.Any(ray => ray.collider != null && ray.collider.CompareTag("Ground"));
    }

    private void Jump() {
        print("I'm going to jump!");
        touchingGround = false;
        body.velocity = new Vector2(-moveVelocity*jumpSpeedMod, jumpVelocity);
    }

    private void Fall() {
        print("I'M FREE... FREE FAAAAALING!");
        touchingGround = false;
        body.velocity = new Vector2(-moveVelocity/jumpSpeedMod, 0);
    }

   /* private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("projectile")) {
            Destroy(collision.gameObject);
            print("OUCH, THAT HURT");
            TalkShitGetHit(collision.gameObject.GetComponent<Bullet>().damage);
            print("My hp is now " + enemyCurrentHP);
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            touchingGround = true;
            aboutToFall = false;
        }
    }

    public void TalkShitGetHit(int damage) {
        print("OUCH, THAT HURT");
        if (enemyCurrentHP > damage) {
            enemyCurrentHP = enemyCurrentHP - damage;
            print("My hp is now " + enemyCurrentHP);
        } else
            Die();
    }

    void Die() {
        print("ded");
        Destroy(this.gameObject);
    }

    private bool WillJump(float rand) {
        if (rand >= 0.5f) {
            return true;
        } else
            return false;
    }

    private float randomNumber() {
        float number = Random.Range(0f, 1f);
        return number;
    }

}