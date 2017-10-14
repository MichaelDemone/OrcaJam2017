using System;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float HorizontalMaxSpeed = 20;
	public float HorizontalSpeed = 5;
	public float JumpSpeed = 3;
	public float MaxVertSpeed = 10;
	public float TimeJumpingStaysAffecting = 1;
	public float MaxClimbSpeed = 3f;

	public Transform GroundCheck;
	
	private Rigidbody2D rigidbod2d;
	
	// Use this for initialization
	void Start ()
	{
		rigidbod2d = GetComponent<Rigidbody2D>();
	}

	private bool jumping = false;
	private float timeJumping = 0;

	private Vector2 velocity;
	
	// Update is called once per frame
	void Update ()
	{
		SetMovement();
		TowerPlacing();
	}

	private void SetMovement()
	{
		velocity = rigidbod2d.velocity;
		velocity.x = Input.GetAxis("Horizontal") * HorizontalSpeed;

		bool touchingGround = Physics2D.OverlapPointAll(GroundCheck.position).Any(col => col.CompareTag("Ground"));

		if (touchingGround && Input.GetAxis("Jump") > 0 && !jumping)
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

		Collider2D[] overlapCols = Physics2D.OverlapPointAll(transform.position);
		bool overlayClimable = overlapCols.Any(overlapCol => overlapCol.CompareTag("Climbable"));

		if (overlayClimable)
		{
			GetComponent<Rigidbody2D>().gravityScale = 0;
			velocity.y = Input.GetAxis("Vertical") * MaxClimbSpeed;
		}
		else
		{
			GetComponent<Rigidbody2D>().gravityScale = 1;
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
		if (Math.Abs(velocity.x) > 0)
		{
			Vector2 scale = transform.localScale;
			scale.x = (velocity.x > 0 ? 1 : -1) * Math.Abs(scale.x);
			transform.localScale = scale;
		}
	}

	private bool towerClicked = false;
	private void TowerPlacing()
	{
		Debug.Log(Input.GetAxis("PlaceTower"));

		if (Input.GetAxis("PlaceTower") != 0 && !towerClicked)
		{
			print("Placed tower!");
			towerClicked = true;
			GetComponent<PlayerTowerInteractions>().PlaceTower(transform.localScale.x < 0);
		}
		else if (Input.GetAxis("PlaceTower") == 0)
		{
			towerClicked = false;
		}
	}
}
