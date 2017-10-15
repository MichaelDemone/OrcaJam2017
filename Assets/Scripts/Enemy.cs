using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Enemy : MonoBehaviour {

	public int enemyMaxHP;
	public int enemyCurrentHP;
	protected Rigidbody2D body;

    public GameObject UIText;
	
	// Use this for initialization
	void Start () {
		enemyCurrentHP = enemyMaxHP;
		body = GetComponent<Rigidbody2D>();
        UIText = GameObject.Find("UIText");
        UIText.GetComponent<UpdateUI>().NumEnemies += 1;
		playerFiring = GameObject.Find("Player").GetComponentInChildren<CannonFireScript>();
	}

	public virtual void TalkShitGetHit(int damage)
	{
		if (enemyCurrentHP > damage) {
			enemyCurrentHP = enemyCurrentHP - damage;
		} else
			Die();
	}

	private CannonFireScript playerFiring;
	
	public virtual void Die()
    {
        Scoreboard.score = Scoreboard.score + 1;
        UIText.GetComponent<UpdateUI>().NumEnemies -= 1;
		Destroy(this.gameObject);
	    
	    if(this.GetType() == typeof(FlyboiAI))
	    {
		    playerFiring.FireRate *= 0.5f;
	    }
	}
}
