using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Vector2 _moveDirection;
    public float speed;
    public bool isGrounded = true;
    public bool canMove = true;
    public Rigidbody2D rb;
    public float maxV = 10;
    public float minVelocityX = -10;
    public bool isjumping = false;
    public bool roll = false;
    public float rolltimer = 1;
    public bool isrolling = false;
    public SpriteRenderer sr;

    public Animator animator;
    
    

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        InputManager.Init(this);
        InputManager.SetGameControls();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("Rolls", false);
    }
    

    // Update is called once per frame
    void Update()
    {
        rolltimer -= Time.deltaTime;
        
        
        
        
        
        if (rb.velocity.x >= maxV)
        {
            rb.velocity -= new Vector2( 1,0);
        }
        if (rb.velocity.x <= -maxV)
        {
            rb.velocity += new Vector2(1, 0);
        }
        if (_moveDirection.x == -1)
        {
            sr.flipX = true;
        }
        if (_moveDirection.x == 1)
        {
            sr.flipX = false;
        }
        if (isGrounded)
        {
            rb.AddForce((new Vector2(10, 0) * _moveDirection));
        }
        if (!isGrounded)
        {
            rb.AddForce((new Vector2(10, 0) * _moveDirection * 0.3f));
        }
        

    }
    public void SetMovementDirection(Vector2 currentDirection)
    {
        
        _moveDirection = currentDirection;
        
        
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = true;
            canMove = true;
            isjumping = false;
        }
    }
    
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = false;
            canMove = false;
            
        }
    }

    internal void Jump()
    {
        if (isGrounded) 
        {
             isjumping = true;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
            isGrounded = false;
        }
        
    }

    internal void Roll()
    {
        
        if (rolltimer <= 0 && sr.flipX == false && isGrounded)
        {
            maxV = 20;
            rolltimer = 1f;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(20f, 0f), ForceMode2D.Impulse);
            maxV = 10;
           
           
            
        }
        if (rolltimer <= 0 && sr.flipX == true && isGrounded)
        {
           
            rolltimer = 1f;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-20f, 0f), ForceMode2D.Impulse);
            maxV = 10;
        }
        
        
    }
}
