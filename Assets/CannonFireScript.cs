using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFireScript : MonoBehaviour {
    
    public GameObject Player;

    float WeaponCooldown = 0;
    public float FireRate;
    public float BulletSpeed;
    public GameObject Bullet;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!Player.GetComponent<PlayerController>().paused)
        {
            if (WeaponCooldown > 0)
            {
                WeaponCooldown -= Time.deltaTime;
            }
        }
    }

    public void Fire()
    {
        

	    if (Input.GetAxis("Fire1") != 0)
	    {
            if (WeaponCooldown <= 0)
            {
                GameObject bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
                
                Vector3 MousePos = Input.mousePosition;
                MousePos.z = 0;
                MousePos = Camera.main.ScreenToWorldPoint(MousePos);

                Vector3 direction2 = MousePos - gameObject.transform.position;

                Vector3 PlayerVelocity = Player.GetComponent<Rigidbody2D>().velocity;

                bullet.GetComponent<Rigidbody2D>().velocity = direction2.normalized * BulletSpeed + PlayerVelocity;

                bullet.GetComponent<AudioSource>().Play();

                WeaponCooldown = FireRate;
            }
	    }
    }
}
