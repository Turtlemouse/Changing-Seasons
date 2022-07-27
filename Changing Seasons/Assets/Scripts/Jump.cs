using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jump : MonoBehaviour
{ 

    
    public float jumpForce = 300F;

    public Animator animator;
    private Rigidbody2D rb;

    public Animator squasher;
    bool holdingJump;
    

    public LayerMask groundLayers;
    const float groundedRadius = 0.2F;
    public Transform groundCheck;

    public bool grounded;
    private bool wasGrounded;
    public float groundRememberTime = 0.2F;
    public float groundRememberTimer = -50000F;

    public bool jumping = false;
    public float jumpRememberTime = 0.5F;
    public float jumpRememberTimer = -50000F;

    public float airTime = 3.0F;
    public float airTimer = -50000F;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //check if grounded
        wasGrounded = grounded;
        grounded = CheckGrounded();

        if (grounded && !wasGrounded)
        {
            //UnityEngine.Debug.Log("GROUNDED!!!");
            jumping = false;
        }

        //set how long you have before remember ground runs out
        //this contributes to "coyote time" where you could run off of a platform and have an extra few miliseconds to jump
        if (grounded)
        {
            
            groundRememberTimer = groundRememberTime + Time.time;
        }

        if (jumping && (airTimer < Time.time))
        {
            //UnityEngine.Debug.Log("Airtime over");
            jumping = false;
        }

        //jump if necessary

        if ((!jumping) && (groundRememberTimer > Time.time) && (jumpRememberTimer > Time.time))
        {
            ActuallyJump();
        }

        //update animations
        animator.SetBool("Jump", jumping);
        animator.SetBool("Grounded", grounded);

        squasher.SetBool("Squash", grounded && holdingJump);
    }

    bool CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, groundLayers);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
                
            }
        }
        return false;
    }

    void ActuallyJump()
    {
        grounded = false;
        jumping = true;
        airTimer = airTime + Time.time;
        rb.AddForce(new Vector2(0f, jumpForce));
        jumpRememberTimer = 0;
        groundRememberTimer = 0;
    }

    public void holdJump()
    {
        holdingJump = true;
    }

    public void pressJump(int charge)
    {
        jumpRememberTimer = jumpRememberTime + Time.time;
        holdingJump = false;
    }


}
