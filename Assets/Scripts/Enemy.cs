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
	}

	public virtual void TalkShitGetHit(int damage)
	{
		if (enemyCurrentHP > damage) {
			enemyCurrentHP = enemyCurrentHP - damage;
		} else
			Die();
	}

	public virtual void Die()
    {
        UIText.GetComponent<UpdateUI>().NumEnemies -= 1;
		Destroy(this.gameObject);
	}
}
