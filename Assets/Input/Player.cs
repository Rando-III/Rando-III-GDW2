using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UIElements;

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
    //public Animator animator;
    public bool canWalljump;
    public float rollforce = 0;
    public bool walljumping;
    public bool canwalljumpleft;
    public bool platLock = false;

    bool onPlat = false;
    bool onFloor = false;

    bool dead = false;



    //Respawn points
    public Transform respawn;
    public float deathtimer;


    void Start()
    {
        deathtimer = 1;
        sr = GetComponent<SpriteRenderer>();
        InputManager.Init(this);
        InputManager.SetGameControls();
        rb = GetComponent<Rigidbody2D>();
        //animator.SetBool("Rolls", false);
    }


    void FixedUpdate()
    {
        if (!dead)
        {
            rolltimer -= Time.deltaTime;
            if (rolltimer < 0)
            {
                rolltimer = 0;
            }
            if (isGrounded)
            {
                gameObject.GetComponent<Animator>().SetBool("Jump", false);
            }

            if (rb.velocity.x != 0 && isGrounded)
            {
                gameObject.GetComponent<Animator>().SetBool("Run", true);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("Run", false);
            }
            if (canWalljump || canwalljumpleft)
            {
                gameObject.GetComponent<Animator>().SetBool("walljump", true);

            }
            else
            {

                gameObject.GetComponent<Animator>().SetBool("walljump", false);
            }

            //if (rb.velocity.x >= maxV)
            //{
            //  rb.velocity = new Vector2( 1,0);
            //}
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
                rb.AddForce((new Vector2(50, 0) * _moveDirection * 0.8f));
            }
            if (rb.velocity.y >= 0)
            {
                rb.gravityScale = 4;
            }
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = 6;
            }
            if (rolltimer > 0 && rolltimer < 0.6f)
            {
                jumpforce = 30;
                maxV = 50;
            }
            else
            {
                jumpforce = 30;
                maxV = 50;
            }

        }    

          
        if (dead)
        {
            gameObject.GetComponent<Animator>().SetBool("Dead", true);
            
            deathtimer -= Time.deltaTime;
            if (deathtimer < 0) 
            {
                gameObject.GetComponent<Animator>().SetBool("Dead", false);
                gameObject.transform.position = respawn.position + new Vector3(0, 5, 0);
                dead = false;
                deathtimer = 1;
                
            }
            // Add Death Stuff Here
            //Destroy(gameObject); // temp
            
            
                
            
            
            
        }

    }


    public void platformLock(float move, bool moveOnX)
    {
        if (platLock)
        {
            if (moveOnX)
            {
                transform.position = new Vector3(move + transform.position.x, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, move + transform.position.y, transform.position.z);
            }
        }
    }

    public void SetMovementDirection(Vector2 currentDirection)
    {
        
        _moveDirection = currentDirection;
        
        
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            respawn = collision.gameObject.transform;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kill")
        {
            dead = true;
        }
        

        if (collision.gameObject.layer == 3)
        {
            onFloor = true;

            isGrounded = true;
            canMove = true;
            isjumping = false;
            walljumping = false;
        }

        if (collision.gameObject.layer == 11)
        {
            platLock = true;

            onPlat = true;

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
            
            canWalljump = true;
        }
        if (collision.gameObject.layer == 9)
        {
            
            canwalljumpleft = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            if (!onPlat)
            {
                isGrounded = false;
                canMove = false;
            }
            onFloor = false;
            
        }
        if (collision.gameObject.layer == 8)
        {
            canWalljump = false;
            
            
        }
        if (collision.gameObject.layer == 9)
        {
            canwalljumpleft = false;
        }
        if (collision.gameObject.layer == 11)
        {
            platLock = false;
            if (!onFloor)
            {
                isGrounded = false;
                canMove = false;
            }
            onPlat = false;
        }
    }

    internal void Jump()
    {
        if (isGrounded) 
        {
            gameObject.GetComponent<Animator>().SetBool("Jump", true);
            isjumping = true;
            rb.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
            isGrounded = false;
        }
        if (canWalljump)
        {

            sr.flipX = false;
            rb.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * 35, ForceMode2D.Impulse);
           

          
            
            canWalljump = false;
            
        }
        if (canwalljumpleft)
        {

            rb.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * 35, ForceMode2D.Impulse);
            sr.flipX = true;
            canwalljumpleft = false;
            
        }
        
    }

    internal void Roll()
    {
        
        if (rolltimer <= 0 && sr.flipX == false && isGrounded)
        {
            maxV = 20;
            rolltimer = 0.6f;
            rb.AddForce(new Vector2(20, 0f), ForceMode2D.Impulse);
            maxV = 10;
           
           
            
        }
        if (rolltimer <= 0 && sr.flipX == true && isGrounded)
        {
           
            rolltimer = 0.6f;
            rb.AddForce(new Vector2(-20, 0f), ForceMode2D.Impulse);
            maxV = 10;
        }
        
        
    }
    
}
