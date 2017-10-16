using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class EnemyAI : Enemy
{
    public Transform GroundCheckLeft;
    public Transform GroundCheckRight;
    
    public float moveVelocity = 4f;
    public int jumpVelocity = 10;
    private bool touchingGround = false;
    private bool aboutToFall = false;
    public int jumpSpeedMod = 2;
    public int fallSpeedMod = 2;

    public Transform jumpPos, fallPos1, fallPos2, hillPos, slopePos;
    RaycastHit2D[] jumpRay, fallRay1, fallRay2, hillRay, slopeRay;



    public int enemyDamage = 1;

    // Update is called once per frame
    void Update() {

        GetRays();


        if (touchingGround) 
        {
            body.velocity = new Vector2(-moveVelocity, 0);
            
            if (CheckJump() && !CheckSlope()) 
            {
                if (WillJump(randomNumber()) && !aboutToFall) 
                {
                    Jump();
                } 
                else 
                {
                    aboutToFall = true;
                }
            }
            
            if (CheckFall()) 
            {
                Fall();
            }

            if (CheckHill()) 
            {
                body.velocity = new Vector2(-moveVelocity, jumpVelocity/2);
            }
        } 


    }

    private void GetRays() {
        jumpRay = Physics2D.RaycastAll(jumpPos.position, Vector2.zero);
        fallRay1 = Physics2D.RaycastAll(fallPos1.position, Vector2.zero);
        fallRay2 = Physics2D.RaycastAll(fallPos2.position, Vector2.zero);
        hillRay = Physics2D.RaycastAll(hillPos.position, Vector2.zero);
        slopeRay = Physics2D.RaycastAll(slopePos.position, Vector2.zero);

    }

    private bool CheckJump() {

        return !jumpRay.Any(ray => ray.collider != null && ray.collider.CompareTag("Ground"));
        
    }

    private bool CheckFall() {
        //return !fallRay.Any(ray => ray.collider != null && ray.collider.CompareTag("Ground"));
        if(fallRay1.Any(ray => ray.collider != null && ray.collider.CompareTag("Ground"))
            || fallRay2.Any(ray => ray.collider != null && ray.collider.CompareTag("Ground")))
        {
            return false;
        }
        return true;
    }

    private bool CheckHill() {
        return hillRay.Any(ray => ray.collider != null && ray.collider.CompareTag("Ground"));
    }

    private bool CheckSlope() {
        return slopeRay.Any(ray => ray.collider != null && ray.collider.CompareTag("Ground"));

    }

    private void Jump() {
        touchingGround = false;
        body.velocity = new Vector2(-moveVelocity*jumpSpeedMod, jumpVelocity);
    }

    private void Fall() {
        touchingGround = false;
        body.velocity = new Vector2(-moveVelocity/fallSpeedMod, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            touchingGround = true;
            aboutToFall = false;
        }
    }

    private bool WillJump(float rand)
    {
        return rand >= 0.5f;
    }

    private float randomNumber() {
        float number = Random.Range(0f, 1f);
        return number;
    }

}
