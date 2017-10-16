using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyboiAI : Enemy {


    private Vector3 vLastPos;
    public float flyboiSpeed = 3f;
    
    public int amplitudeMod=1;
    public int wavelengthMod = 1;
    public int enemyDamage = 0;


    void Awake() {
        body = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        vLastPos = transform.position;
        body.velocity = new Vector2(-flyboiSpeed, 0);
        transform.position = vLastPos + new Vector3(0.0f, (Mathf.Sin(Time.time*wavelengthMod))/amplitudeMod, 0.0f);

    }
}
