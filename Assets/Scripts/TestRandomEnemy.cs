using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRandomEnemy : MonoBehaviour
{

	public Vector2 MinPosition = new Vector2(-5, -5);
	public Vector2 MaxPosition = new Vector2(5, 5);

	public Vector2 MaxVelocity = new Vector2(0.01f, 0.01f);
	
	// Use this for initialization
	void Start ()
	{
		SetRandomCurrentPosition();
		SetRandomCurrentVelocity();
	}

	private Vector3 currentVelocity;
	
	// Update is called once per frame
	void Update ()
	{
		transform.position += currentVelocity;
	}

	void Die()
	{
		SetRandomCurrentPosition();
		SetRandomCurrentVelocity();
		
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		Die();
	}

	private void SetRandomCurrentPosition()
	{
		transform.position = new Vector3(UnityEngine.Random.Range(MinPosition.x, MaxPosition.x), UnityEngine.Random.Range(MinPosition.y, MaxPosition.y));
	}
	
	private void SetRandomCurrentVelocity()
	{
		currentVelocity = new Vector2(UnityEngine.Random.Range(-1*MaxVelocity.x, MaxVelocity.x), UnityEngine.Random.Range(-1*MaxVelocity.y, MaxVelocity.y));
	}
}
