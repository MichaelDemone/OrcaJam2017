using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyboiAI : MonoBehaviour {


    private Vector3 vLastPos;
    public float flyboiSpeed = 3f;
    public int enemyTotalHP = 10;
    public int enemyCurrentHP = 0;
    private Rigidbody2D body;
    public int amplitudeMod=15;
    public int wavelengthMod = 3;


    void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        vLastPos = transform.position;
        enemyCurrentHP = enemyTotalHP;
    }
	
	// Update is called once per frame
	void Update () {
        vLastPos = transform.position;
        body.velocity = new Vector2(-flyboiSpeed, 0);
        transform.position = vLastPos + new Vector3(0.0f, (Mathf.Sin(Time.time*wavelengthMod))/amplitudeMod, 0.0f);

    }

    public void TalkShitGetHit(int damage) {
        print("OUCH, THAT HURT");
        if (enemyCurrentHP > damage) {
            enemyCurrentHP = enemyCurrentHP - damage;
            print("My hp is now " + enemyCurrentHP);
        } else
            Die();
    }

    public void Die() {
        print("ded");
        Destroy(this.gameObject);
    }
}
