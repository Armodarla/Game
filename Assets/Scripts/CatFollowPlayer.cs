using UnityEngine;
using Unity.Cinemachine;

public class CatFollowPlayer : MonoBehaviour
{
    public Transform player;           // Reference to the player's transform
    public float followSpeed = 2f;     // Speed at which the cat follows the player
    public float followDistance = 1f;  // How close the cat stays to the player
    public CharacterCodes characterCode;
    public CinemachineCamera cam;
    public static int catMode = 1; // 0 = Follows player, 1 = Stops, 2 = Player controls cat


    [SerializeField]bool doGroundCheck = true; // Cat don't do GroundCheck when following player (until its in the follow distance)
    [SerializeField]bool isGrounded = false;
    bool facingRight = true;
    
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;


    Rigidbody2D rb;            // Cat's Rigidbody2D
    SpriteRenderer sprender;
     
    bool controllingCat = false;
    Animator animator;

    void Start()
    {
        sprender = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShiftMode();
            Debug.Log("Key F Pressed, Now Cat Mode is : " + catMode.ToString());
        }
    }

    void FixedUpdate()
    {
        if (doGroundCheck)
            GroundCheck();
        if (catMode == 0)
        {
            CatTeleport();
            cam.Target.TrackingTarget = player;
            controllingCat = false;
            characterCode.isControllable = true;
            FollowPlayer();
        }
        else if (catMode == 1)
            CatStay();
        else if (catMode == 2)
            CatControl();
    }

    void ShiftMode()
    {
        catMode = (catMode + 1) % 3;
    }

    void FollowPlayer()
    {
        if (!isGrounded)
            gameObject.GetComponent<Collider2D>().enabled = false;
        // Calculate the distance between the cat and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        Vector2 direction = (player.position - transform.position).normalized;
        
        
        // If the cat is farther than the followDistance, move toward the player
        if (distanceToPlayer > followDistance)
        {
            isGrounded = false;
            doGroundCheck = false;
            // Move the cat towards the player
            if (!characterCode.isRunning)
                rb.linearVelocity = new Vector2(direction.x * followSpeed * 120 * Time.fixedDeltaTime, (direction.y) * 100 * followSpeed * Time.fixedDeltaTime);
            else
                rb.linearVelocity = new Vector2(direction.x * followSpeed * 120 * Time.fixedDeltaTime * characterCode.speed_modifier, (direction.y) * 100 * followSpeed * Time.fixedDeltaTime);
        }
        else
        {
            doGroundCheck = true;
            gameObject.GetComponent<Collider2D>().enabled = true;
            // Stop the cat when it reaches the follow distance
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        Vector3 currentScale = transform.localScale;

        if (facingRight && direction.x < 0)
        {
            currentScale.x *= -1;
            facingRight = false;
        }
        if (!facingRight && direction.x > 0)
        {
            currentScale.x *= -1;
            facingRight = true;
        }

        transform.localScale = currentScale;
    }

    void CatStay()
    {
        doGroundCheck = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    void CatControl()
    {
        Debug.Log("Control Cat Mode!");
        doGroundCheck = true;
        controllingCat = true;
        characterCode.isControllable = false;

        cam.Target.TrackingTarget = transform;

        float dir = characterCode.horizontalVal;
        #region Walk & Run
        float xDir = dir * followSpeed * 100 * Time.fixedDeltaTime;
        Vector2 targetVelocity = new Vector2(xDir, rb.linearVelocity.y);
        rb.linearVelocity = targetVelocity;

        Vector3 currentScale = transform.localScale;

        if (facingRight && dir < 0)
        {
            currentScale.x *= -1;
            facingRight = false;
        }
        if (!facingRight && dir > 0)
        {
            currentScale.x *= -1;
            facingRight = true;
        }

        transform.localScale = currentScale;
        #endregion

        #region Jump!
        bool jumpFlag = characterCode.jumpFlag;
        if (isGrounded && jumpFlag)
        {
            jumpFlag = false;
            isGrounded = false;
            rb.AddForce(new Vector2(0f, characterCode.jumpPower));
        }
        #endregion
    }

    void GroundCheck()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, 0.1f, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
        animator.SetBool("Jumping", !isGrounded);
    }

    void CatTeleport()
    {
        if (controllingCat)
        {
            sprender.enabled = false;
            transform.localPosition = player.localPosition;
            sprender.enabled = true;
        }
    }
}
