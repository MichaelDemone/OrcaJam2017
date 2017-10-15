using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFireScript : MonoBehaviour {
    
    public GameObject Player;

    float WeaponCooldown = 0;
    public float FireRate;
    public float BulletSpeed;
    public GameObject Bullet;

    public bool flip = false;   // modified by PlayerController.cs

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
                Vector3 BulletPosition = transform.position;
                if(flip)
                {
                    BulletPosition -= transform.right * 1.4f;
                } else
                {
                    BulletPosition += transform.right * 1.4f;
                }

                GameObject bullet = Instantiate(Bullet, BulletPosition, Quaternion.identity);
                
                Vector3 MousePos = Input.mousePosition;
                MousePos.z = 0;
                MousePos = Camera.main.ScreenToWorldPoint(MousePos);

                Vector3 direction2 = MousePos - gameObject.transform.position;
                direction2.z = 0;

                Vector3 PlayerVelocity = Player.GetComponent<Rigidbody2D>().velocity;

                bullet.GetComponent<Rigidbody2D>().velocity = direction2.normalized * BulletSpeed + PlayerVelocity;

                bullet.GetComponent<AudioSource>().Play();

                WeaponCooldown = FireRate;
            }
	    }
    }
}
