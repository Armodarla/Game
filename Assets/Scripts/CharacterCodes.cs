using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCodes : MonoBehaviour
{
    
    public float player_speed=2;
    public float speed_modifier=2;
    public float jumpPower=500;
    public bool isControllable = true;
    public bool isRunning = false;
    public float horizontalVal;
    public bool jumpFlag = false;

    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D PlayerRb;
    Animator animator;
    

    const float groundCheckRadius = 0.1f;
    bool facingRight = true;
    [SerializeField] bool isGrounded = false;


    void Awake()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalVal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        if (Input.GetButtonDown("Jump"))
            jumpFlag = true;
        else if (Input.GetButtonUp("Jump"))
            jumpFlag = false;
    }

    void FixedUpdate()
    {
        GroundCheck();
        if (isControllable)
            Move(horizontalVal, jumpFlag);
        else
            PlayerRb.linearVelocity = new Vector2(0, PlayerRb.linearVelocity.y);
    }

    void Move(float dir, bool jumpFlag)
    {
        #region Walk & Run
        float xDir = dir * player_speed * 100 * Time.fixedDeltaTime;
        if (isRunning)
        {
            xDir *= speed_modifier;
        }
        Vector2 targetVelocity = new Vector2(xDir, PlayerRb.linearVelocity.y);
        PlayerRb.linearVelocity = targetVelocity;

        Vector3 currentScale = transform.localScale;

        if(facingRight && dir < 0)
        {
            currentScale.x *= -1;
            facingRight = false;
        }
        if(!facingRight && dir > 0)
        {
            currentScale.x *= -1;
            facingRight = true;
        }

        transform.localScale = currentScale;

        // 0 iddle, 3.9999 walk, 7.999 run
        // Debug.Log(PlayerRb.linearVelocity.x);
        animator.SetFloat("xVelocity", Mathf.Abs(PlayerRb.linearVelocity.x));
        #endregion

        #region Jump!
        if (isGrounded && jumpFlag)
        {
            jumpFlag = false;
            isGrounded = false;
            animator.SetBool("Jumping", !isGrounded);
            PlayerRb.AddForce(new Vector2(0f, jumpPower));
        }
        #endregion
    }

    void GroundCheck()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
        animator.SetBool("Jumping", !isGrounded);
    }

    public void SetControllable(bool setControl)
    {
        isControllable = setControl;
    }
}
