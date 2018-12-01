using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (PlayerController_StompBug))]
public class Player_StompBug : SingletonBehaviour<Player_StompBug> {

	[Header ("Moving and Jumping")]
	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	public float moveSpeed = 6;

    private Vector2 deltaMovement;
    private float accelerationTimeAirborne = .4f;
	private float accelerationTimeGrounded = .2f;
	private float gravity;
	private float jumpVelocity;
	private Vector3 velocity;
	private float velocityXSmoothing;

    private PlayerController_StompBug controller;
	private Animator anim;

    private bool isDead = false;
	private bool facingRight = true;
	private bool doubleJump = false;
    private bool isRunning = false;
    private bool isOnGround = false;
    private bool foot = false;
    const int maxJumpNum = 1;
	int jumpNum;

    [Header("Skills")]
    public RangedWeapon rangeWeapon;

    [Header("Visual Effects")]
	public GameObject damageEffect;
	public GameObject footEffect;
	public Animator hurtPanel;
	//private PlayerHealth health;
    public GameObject trailEffect;
    public float startTrailEffectTime;
    private float trailEffectTime;

    [Header("Audio Effects")]
	public AudioClip landing;
    private AudioSource source;

	void Start() {
		source = GetComponent<AudioSource>();
		//health = GameObject.FindGameObjectWithTag("GM").GetComponent<PlayerHealth>();
		anim = GetComponent<Animator>();
        controller = GetComponent<PlayerController_StompBug> ();
		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        //health.noDam = false;

        jumpNum = maxJumpNum;
	}

	void Update() 
    {
        HandleInput();
        UpdateMovement();

		// detects when on the ground
		if (controller.collisions.below) {
			if(foot == true){
                if (landing != null)
                {
                    source.clip = landing;
                    source.Play();
                }
				anim.SetBool("isJumping", false);
				foot = false;
				Vector2 pos = new Vector2(transform.position.x, transform.position.y - 0.6f);
				Instantiate(footEffect, pos, Quaternion.identity);
			}
			anim.SetBool("isJumping", false);
			velocity.y = 0;
			doubleJump = false;
            jumpNum = maxJumpNum;
		} else if(controller.collisions.below == false){
			anim.SetBool("isJumping", true);
			foot = true;
		}


	}

    /// <summary>
    /// Handles the input. 
    /// calculate deltaMovement used for UpdateMovement
    /// </summary>
    private void HandleInput()
    {
        deltaMovement = Vector2.zero;

        // detects if the player is holding the arrow keys to move
        if (!isDead)
        {
            deltaMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // jump and double jump and triple jump
            if (Input.GetKey(KeyCode.Space) && controller.collisions.below)
            {
                Jump();
            }
            /*
            if (Input.GetKeyDown(KeyCode.Space) && doubleJump == false && !controller.collisions.below){
                velocity.y = jumpVelocity;
                jumpNum--;
                if(jumpNum <= 0){
                    doubleJump = true;
                }
            } 
            */
        }
    }


    /// <summary>
    /// Updates the movement using deltaMovement calculated every frame
    /// </summary>
    private void UpdateMovement()
    {
        Flip(deltaMovement);

        // handles moving and physics for jumping
        float targetVelocityX = deltaMovement.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;

        if (isDead)
        {
            controller.MoveIgnoreCollision(velocity * Time.deltaTime);
            return;
        }
         
               

        controller.Move(velocity * Time.deltaTime);
        isRunning = (deltaMovement.x != 0);
        anim.SetBool("isRunning", isRunning);

        isOnGround = controller.collisions.below;

        if (isRunning || !isOnGround)
        {
            //UpdateTrailEffect(); 
        }
    }



    /*****************************************
     * 
     * Actions
     * 
     *****************************************/

    public void Dead()
    {
        if (isDead)
            return;

        PlayerUtils.PlayerDeadOccur();
        velocity = new Vector2(-jumpVelocity / 8, jumpVelocity / 2);
        //anim.SetBool("isDead", true);
        //anim.Play("Dead");
        controller.collisions.below = false;
        isDead = true;
    }

    private void Jump()
    {
        foot = true;
        velocity.y = jumpVelocity;
    }

    private void Attack()
    {
        
    }

    private void AttackMelee()
    {
        
    }

    private void AttackRange()
    {
        
    }

    void UpdateTrailEffect()
    {
        if (trailEffectTime <= 0)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y - 1f);
            Instantiate(trailEffect, pos, Quaternion.identity);
            trailEffectTime = startTrailEffectTime;
        }
        else
        {
            trailEffectTime -= Time.deltaTime;
        }
    }


	// flip the character so he is facing the direction he is moving in
	void Flip(Vector2 input){
		
		if(input.x > 0 && facingRight == false || input.x < 0 && facingRight == true){
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}

	public void Damage(){
        if (hurtPanel != null)
            hurtPanel.SetTrigger("Hurt");
        if (damageEffect != null)
            Instantiate(damageEffect, transform.position, Quaternion.identity);
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }

    public bool IsDead()
    {
        return isDead;
    }



}
