using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Enemy : MonoBehaviour {

	public int enemyMaxHP = 10;
	public int enemyCurrentHP;
	protected Rigidbody2D body;
	
	// Use this for initialization
	void Start () {
		enemyCurrentHP = enemyMaxHP;
		body = GetComponent<Rigidbody2D>();
	}

	public virtual void TalkShitGetHit(int damage)
	{
		print("OUCH, THAT HURT");
		if (enemyCurrentHP > damage) {
			enemyCurrentHP = enemyCurrentHP - damage;
			print("My hp is now " + enemyCurrentHP);
		} else
			Die();
	}

	public virtual void Die()
	{
		print("ded");
		Destroy(this.gameObject);
	}
}
