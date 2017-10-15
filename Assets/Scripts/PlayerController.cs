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

    public GameObject PlayerCannon;

    bool paused = false;
	
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
        if(Input.GetKeyDown("p"))
        {
            if (paused)
            {
                paused = false;
                Time.timeScale = 1;
            } else
            {
                paused = true;
                Time.timeScale = 0;
            }
        }
        if (!paused)
        {
            SetMovement();
            AimCannon();
            TowerPlacing();
        }
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

        /*if (Math.Abs(velocity.x) > 0)
		{
			Vector2 scale = transform.localScale;
			scale.x = (velocity.x > 0 ? 1 : -1) * Math.Abs(scale.x);
			transform.localScale = scale;
		}*/

        // flip the player to face the mouse
        Vector3 MousePos = Input.mousePosition;
        MousePos.z = 0;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        if(MousePos.x < transform.position.x)
        {
            Vector2 scale = transform.localScale;
            scale.x = -1 * Math.Abs(scale.x);
            transform.localScale = scale;
        } else
        {
            Vector2 scale = transform.localScale;
            scale.x = 1 * Math.Abs(scale.x);
            transform.localScale = scale;
        }

        if (gameObject.transform.position.y < -13)
        {
            gameObject.transform.position = new Vector3(5, -7, 0);
        }

	}

	private bool towerClicked = false;
	private void TowerPlacing()
	{

		if (Input.GetAxis("PlaceTower") != 0 && !towerClicked)
		{
			towerClicked = true;
			GetComponent<PlayerTowerInteractions>().PlaceTower(transform.localScale.x < 0);
		}
		else if (Input.GetAxis("PlaceTower") == 0)
		{
			towerClicked = false;
		}

		if (Input.GetAxis("RemoveTower") != 0)
		{
			towerClicked = true;
			GetComponent<PlayerTowerInteractions>().RetreiveTower(transform.localScale.x < 0);
		}
	}

    private void AimCannon()
    {
            Vector3 MousePos = Input.mousePosition;
            MousePos.z = 0;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);

            Vector3 direction = MousePos - PlayerCannon.transform.position;

            if (MousePos.x < transform.position.x)
            {
                // since the player is flipped to face the mouse, have to account for this
                direction = direction * -1;
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            PlayerCannon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
}
