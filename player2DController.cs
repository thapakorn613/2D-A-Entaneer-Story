using System.Collections;
using UnityEngine;


public class player2DController : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float jumpPower ;
    

    public LayerMask whatIsGround;

    private Animator playerAnimator;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D playerRigidbody2D;
    private bool isGround;
    private const float centerY = 1.5f;

    private State playerStatus = State.Normal;


	void Awake()
	{
		playerAnimator = GetComponent<Animator>();
		boxCollider2D = GetComponent<BoxCollider2D>();
		playerRigidbody2D = GetComponent<Rigidbody2D>();
	}


	void Start()
    {
		
		// UnityChan2DController
		maxSpeed = 10f;
		jumpPower = 1000;
		
		//whatIsGround = 1 << LayerMask.NameToLayer("Ground");
		
		// Transform
		transform.localScale = new Vector3(1, 1, 1);
		
		// Rigidbody2D
		playerRigidbody2D.gravityScale = 3.5f;
	
		
		// BoxCollider2D
		//boxCollider2D.size = new Vector2(1, 2.5f);
		//boxCollider2D.offset = new Vector2(0, -0.25f);
		
		// Animator
		playerAnimator.applyRootMotion = false;

	}

   

    void Update()
    {
        if (playerStatus != State.Damaged)
        {
            float x = Input.GetAxis("Horizontal");
            bool jump = Input.GetButtonDown("Jump");
            Move(x, jump);
        }
    }

    void Move(float move, bool jump)
    {
        if (Mathf.Abs(move) > 0)
        {
            Quaternion rot = transform.rotation;
            transform.rotation = Quaternion.Euler(rot.x, Mathf.Sign(move) == 1 ? 0 : 180, rot.z);
        }

        playerRigidbody2D.velocity = new Vector2(move * maxSpeed, playerRigidbody2D.velocity.y);

        playerAnimator.SetFloat("Horizontal", move);
        playerAnimator.SetFloat("Vertical", playerRigidbody2D.velocity.y);
        playerAnimator.SetBool("isGround", isGround);

        if (jump && isGround)
        {
            playerAnimator.SetTrigger("Jump");
            playerRigidbody2D.AddForce(Vector2.up * jumpPower);
        }
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        Vector2 groundCheck = new Vector2(pos.x, pos.y - (centerY * transform.localScale.y));
        Vector2 groundArea = new Vector2(boxCollider2D.size.x * 0.49f, 0.05f);

        isGround = Physics2D.OverlapArea(groundCheck + groundArea, groundCheck - groundArea, whatIsGround);
        playerAnimator.SetBool("isGround", isGround);

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "DamageObject" && playerStatus == State.Normal)
        {
            //playerStatus = State.Damaged;
            // do something
     
        }
    }


    enum State
    {
        Normal,
        Damaged,
    }
}