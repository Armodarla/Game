using UnityEngine;

public class CatFollowPlayer : MonoBehaviour
{
    public Transform player;           // Reference to the player's transform
    public float followSpeed = 2f;     // Speed at which the cat follows the player
    public float followDistance = 1f;  // How close the cat stays to the player
    public CharacterCodes characterCode;
    

    bool facingRight = true;
    Rigidbody2D rb;            // Cat's Rigidbody2D
    [SerializeField] int catMode = 0; // 0 = Follows player, 1 = Stops, 2 = Player controls cat
    bool controllingCat = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // characterCode = playerObj.GetComponent<CharacterCodes>();
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
        if (catMode == 0)
        {
            controllingCat = false;
            characterCode.isControllable = true;
            FollowPlayer();
        }
        else if (catMode == 1)
            CatStay();
        else if (catMode == 2)
            ControlCat();
            
    }

    void ShiftMode()
    {
        catMode = (catMode + 1) % 3;
    }

    void FollowPlayer()
    {
        // Calculate the distance between the cat and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        Vector2 direction = (player.position - transform.position).normalized;
        // If the cat is farther than the followDistance, move toward the player
        if (distanceToPlayer > followDistance)
        {
            // Move the cat towards the player
            if (!characterCode.isRunning)
                rb.linearVelocity = new Vector2(direction.x * followSpeed * 120 * Time.fixedDeltaTime, (direction.y - 0.2f) * 100 * followSpeed * Time.fixedDeltaTime);
            else
                rb.linearVelocity = new Vector2(direction.x * followSpeed * 120 * Time.fixedDeltaTime * characterCode.speed_modifier, (direction.y - 0.2f) * 100 * followSpeed * Time.fixedDeltaTime);
        }
        else
        {
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
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    void ControlCat()
    {
        Debug.Log("Control Cat Mode!");
        controllingCat = true;
        characterCode.isControllable = false;
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
    }
}