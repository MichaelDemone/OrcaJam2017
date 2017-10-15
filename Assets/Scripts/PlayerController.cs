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

	private Animator animator;
	
    //public float firerate;
    //float firedelay = 0;

	//public float BulletSpeed;
	
	public Transform GroundCheck;
	
	private Rigidbody2D rigidbod2d;

    public GameObject PlayerCannon;

	//public GameObject Bullet;
	
    public bool paused = false;
	
	// Use this for initialization
	void Start ()
	{
		rigidbod2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
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
                Unpause();
            } else
            {
                Pause();
            }
        }
        if (!paused)
        {
            SetMovement();
            AimCannon();
        }
	}

	private bool climbing = false;
	private bool falling = false;
	
	private void SetMovement()
	{
		velocity = rigidbod2d.velocity;
		velocity.x = Input.GetAxis("Horizontal") * HorizontalSpeed;

		bool touchingGround = Physics2D.OverlapPointAll(GroundCheck.position).Any(col => col.CompareTag("Ground"));

		falling = !touchingGround;
		
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

			if (!jumping)
			{
				velocity.y = 0;
				falling = true;
			}
		}

		Collider2D[] overlapCols = Physics2D.OverlapPointAll(transform.position);
		bool overlayClimable = overlapCols.Any(overlapCol => overlapCol.CompareTag("Climbable"));

		GetComponent<Collider2D>().enabled = true;
		
		
		if (overlayClimable && Input.GetAxis("Vertical") != 0)
		{
			GetComponent<Rigidbody2D>().gravityScale = 0;
			climbing = true;
			GetComponent<SpriteRenderer>().sortingLayerName = "PlayerOverRopes";
			PlayerCannon.GetComponent<SpriteRenderer>().sortingLayerName = "PlayerOverRopes";
			velocity.y = Input.GetAxis("Vertical") * MaxClimbSpeed;
			GetComponent<Collider2D>().enabled = false;
		}
		else if (!overlayClimable)
		{
			climbing = false;
			GetComponent<Rigidbody2D>().gravityScale = 4;
			GetComponent<SpriteRenderer>().sortingLayerName = "Default";
			PlayerCannon.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
		}


		if (Math.Abs(velocity.x) > HorizontalMaxSpeed)
		{
			velocity.x = Math.Sign(velocity.x) * HorizontalMaxSpeed;
		}

		if (Math.Abs(velocity.y) > MaxVertSpeed)
		{
			velocity.y = Math.Sign(velocity.y) * MaxVertSpeed;
		}

		SetAnimator();
		
		rigidbod2d.velocity = velocity;

        // flip the player to face the mouse
        Vector3 MousePos = Input.mousePosition;
        MousePos.z = 0;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
       // print(MousePos.x + ", " + transform.position.x);
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


	private enum animationState
	{
		falling,
		jumping,
		running,
		idle,
	}

	private animationState AnimationState;
	
	private void SetAnimator()
	{
		if (climbing || falling)
		{
			if (AnimationState != animationState.falling)
			{
				AnimationState = animationState.falling;
				animator.SetTrigger("Falling");
			}
		}
		else if (jumping)
		{
			if (AnimationState != animationState.jumping)
			{
				AnimationState = animationState.jumping;
				animator.SetTrigger("Jumping");
			}
		}
		else if (Math.Abs(velocity.x) > 0)
		{
			if(AnimationState != animationState.running)
			{
				AnimationState = animationState.running;
				animator.SetTrigger("Running");
			}
		}
		else 
		{
			if (AnimationState != animationState.idle)
			{
				AnimationState = animationState.idle;
				animator.SetTrigger("Idle");
			}
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
            PlayerCannon.GetComponent<CannonFireScript>().flip = true;
        } else
        {
            PlayerCannon.GetComponent<CannonFireScript>().flip = false;
        }

		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		PlayerCannon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

	    if (Input.GetAxis("Fire1") != 0)
	    {
            PlayerCannon.GetComponent<CannonFireScript>().Fire();
	    }
	    
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        paused = false;
        Time.timeScale = 1;
    }
}
