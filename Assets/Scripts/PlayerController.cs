﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	public float HorizontalMaxSpeed = 20;
	public float HorizontalSpeed = 5;
	public float JumpSpeed = 3;
	public float MaxVertSpeed = 10;
	public float TimeJumpingStaysAffecting = 1;
	
	
	private Rigidbody2D rigidbod2d;
	
	// Use this for initialization
	void Start ()
	{
		rigidbod2d = GetComponent<Rigidbody2D>();
	}

	private bool jumping = false;
	private float timeJumping = 0;
	private bool onGround = false;

	private Vector2 velocity;
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		velocity = rigidbod2d.velocity; 
		velocity.x = Input.GetAxis("Horizontal") * HorizontalSpeed;

		
		SetOnGround();
		if (onGround && Input.GetAxis("Jump") > 0 && !jumping)
		{
			velocity.y += Input.GetAxis("Jump") * JumpSpeed;
			jumping = true;
			timeJumping = 0;
		}
		else if (jumping)
		{
			jumping = Input.GetAxis("Jump") > 0;
			if (Input.GetAxis("Jump") > 0)
			{
				timeJumping += Time.deltaTime;
				if (timeJumping > TimeJumpingStaysAffecting)
				{
					jumping = false;
				}
				velocity.y += JumpSpeed * (TimeJumpingStaysAffecting - timeJumping) / TimeJumpingStaysAffecting;
			}
			else
			{
				jumping = false;
			}
		}
		else if (velocity.y > 0 && Input.GetAxis("Jump") < 0.1f)
		{
			velocity.y = 0;
		}


		if (Math.Abs(velocity.x) > HorizontalMaxSpeed)
		{
			velocity.x = Math.Sign(velocity.x) * HorizontalMaxSpeed;
		}
		
		if (Math.Abs(velocity.y) > MaxVertSpeed)
		{
			velocity.y = Math.Sign(velocity.y) * MaxVertSpeed;
		}

		rigidbod2d.velocity = velocity;
	}

	private Vector2 pos;
	void SetOnGround()
	{
		pos = transform.position;
		pos.y -= 1.25f;
		onGround = Physics2D.Raycast(pos, Vector2.zero);
	}
}