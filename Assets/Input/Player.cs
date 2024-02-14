using JetBrains.Annotations;
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
    public float maxV = 50;
    public float minVelocityX = -50;
    public bool isjumping = false;
    public bool roll = false;
    public float rolltimer = 1;
    public bool isrolling = false;
    public SpriteRenderer sr;
    public float jumpforce= 40;
    public Animator animator;
    public bool canWalljump;
    public float rollforce = 0;
    public bool walljumping;
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
    void FixedUpdate()
    {
        rolltimer -= Time.deltaTime;
        if (rolltimer < 0) 
        {
            rolltimer = 0;
        }
        
        
        
        
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
            rb.AddForce((new Vector2(50, 0) * _moveDirection));
        }
        if (!isGrounded && !walljumping)
        {
            rb.AddForce((new Vector2(50, 0) * _moveDirection * 0.6f));
        }
        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = 4;
        }
        if (rb.velocity.y < 0 )
        {
            rb.gravityScale = 6;
        }
        if (rolltimer > 0  && rolltimer < 0.6f)
        {
            jumpforce = 20;
            maxV = 50;
        }
        else
        {
            jumpforce = 20;
            maxV = 50;
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
            walljumping = false;
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            rb.velocity = new Vector2 (rb.velocity.x, -1);
            canWalljump = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = false;
            canMove = false;
            
        }
        if (collision.gameObject.layer == 8)
        {
            canWalljump = false;
            
        }
    }

    internal void Jump()
    {
        if (isGrounded) 
        {
             isjumping = true;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
            isGrounded = false;
        }
        if (canWalljump)
        {
            rb.velocity = new Vector2 (50, 50);
            
            canWalljump = false;
            walljumping = true;
        }
        
    }

    internal void Roll()
    {
        
        if (rolltimer <= 0 && sr.flipX == false && isGrounded)
        {
            maxV = 20;
            rolltimer = 0.6f;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(20, 0f), ForceMode2D.Impulse);
            maxV = 10;
           
           
            
        }
        if (rolltimer <= 0 && sr.flipX == true && isGrounded)
        {
           
            rolltimer = 0.6f;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-20, 0f), ForceMode2D.Impulse);
            maxV = 10;
        }
        
        
    }
    
}
