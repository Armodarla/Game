using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCodes : MonoBehaviour
{
    public float player_speed;
    public float speed_modifier;

    Rigidbody2D PlayerRb;
    Animator animator;
    float horizontalVal;
    
    bool isRunning = false;
    bool facingRight = true;

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
    }

    void FixedUpdate()
    {
        Move(horizontalVal);
    }

    void Move(float dir)
    {
        float xDir = dir * player_speed * 100 * Time.deltaTime;
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
    }
}
