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
    public string platTag;

    public bool onPlat = false;
    bool onFloor = false;

    bool dead = false;
    public Transform respawn;
    public float deadtimer;
    int i;

    public GameObject DeathEffect;
    public float deathEffectCount = 0;


    public float walljumptimer;
    public float walljumptimerleft;
    public float jumpintime;
    public float walljumpcount;
    void Start()
    {
        deadtimer = 1;
        sr = GetComponent<SpriteRenderer>();
        gameObject.GetComponent<Animator>();
        InputManager.Init(this);
        InputManager.SetGameControls();
        rb = GetComponent<Rigidbody2D>();
        //animator.SetBool("Rolls", false);
    }
    

    void FixedUpdate()
    {
        
        walljumptimer -= Time.deltaTime;
        walljumptimerleft -= Time.deltaTime;
        jumpintime -= Time.deltaTime;
        if (walljumptimer > 0 || walljumptimerleft >0 )
        {
            jumpintime = 0;
        
        }
        if ( jumpintime > 0)
        {
            walljumptimerleft = 0;
            walljumptimer = 0;
        }
        if (canwalljumpleft)
        {
            walljumptimerleft = 0.2f;
        }
        if (canWalljump)
        {
            walljumptimer = 0.2f;
        }
        if (walljumpcount > 0)
        {
            walljumptimer = 0;
            walljumptimerleft = 0;
            jumpintime = 0;
        }

        if (rb.velocity.y < 0 && !isGrounded)
        {
            
            
            try
            {
                gameObject.GetComponent<Animator>().SetBool("fall", true);
            }
            catch { }

        }
        
        else
        {
            
            try
            {
                gameObject.GetComponent<Animator>().SetBool("fall", false);
            }
            catch { }
        }
        rolltimer -= Time.deltaTime;
        if (rolltimer < 0) 
        {
            rolltimer = 0;
            
        }
        if (rolltimer < 0.3) 
        {
            
            try
            {
                gameObject.GetComponent<Animator>().SetBool("dash", false);
            }
            catch { }
        }
 
        if (rb.velocity.x != 0)
        {
            
            try
            {
                gameObject.GetComponent<Animator>().SetBool("run", true);
            }
            catch { }

        }
        else if (rb.velocity.x == 0)
        {
            
            try
            {
                gameObject.GetComponent<Animator>().SetBool("run", false);
            }
            catch { }
        }
        if (canWalljump || canwalljumpleft)
        {
            
            try
            {
                gameObject.GetComponent<Animator>().SetBool("wall", true);
            }
            catch { }
        }
        else
        {
            try
            {
                gameObject.GetComponent<Animator>().SetBool("wall", false);
            }
            catch { }
            
        }
        
        if (!dead)
        {

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
            if (deathEffectCount < 1)
            {

                Instantiate(DeathEffect, gameObject.transform.position, Quaternion.identity);
                deathEffectCount++;
            }
            try
            {
                gameObject.GetComponent<Animator>().SetBool("dead", true);
            }
            catch { }
            try
            {
                gameObject.GetComponent<Animator>().SetBool("wall", false);
            }
            catch { }
            
            
            deadtimer -= Time.deltaTime;
            {
                if (deadtimer <= 0 )
                {
                    Destroy(GameObject.Find("CFXR2 Broken Heart(Clone)"));
                    
                    try
                    {
                        gameObject.GetComponent<Animator>().SetBool("dead", false);
                    }
                    catch { }
                    gameObject.transform.position = respawn.transform.position + new Vector3(0, 5f, 0);
                    dead = false;
                    deadtimer = 1;
                    deathEffectCount = 0;
                }
            }
            // Add Death Stuff Here
            //Destroy(gameObject); // temp
            
        }

    }


    public void platformLock(float move, bool moveOnX, string effect)
    {
        if (platLock && platTag == effect)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kill")
        {
            dead = true;
        }

        if (collision.gameObject.layer == 3)
        {
            onFloor = true;
            try
            {
                gameObject.GetComponent<Animator>().SetBool("jump", false);
            }
            catch { }
            if (!canWalljump && !canwalljumpleft)
            {
                isGrounded = true;
            }

            canMove = true;
            isjumping = false;
            walljumping = false;
        }

        if (collision.gameObject.layer == 11)
        {
            platLock = true;
            platTag = collision.gameObject.tag;
            onPlat = true;
            try
            {
                gameObject.GetComponent<Animator>().SetBool("jump", false);
            }
            catch { }
            isGrounded = true;
            canMove = true;
            isjumping = false;
            walljumping = false;
        }
        if (collision.gameObject.layer == 8 && !isGrounded && jumpintime > 0)
        {
            rb.AddForce(Vector2.right * 20, ForceMode2D.Impulse);

            rb.AddForce(Vector2.up * 35, ForceMode2D.Impulse);
            walljumpcount = 1;
            sr.flipX = true;
            jumpintime = 0;
            walljumptimer = 0;

            canWalljump = false;
        }
        if (collision.gameObject.layer == 8)
        {
            walljumpcount = 0;
        }
        if (collision.gameObject.layer == 9 && !isGrounded && jumpintime > 0)
        {
            rb.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
            walljumpcount = 1;
            rb.AddForce(Vector2.up * 35, ForceMode2D.Impulse);
            jumpintime = 0;
            walljumptimerleft = 0;
            canwalljumpleft = false;
        }
        if (collision.gameObject.layer == 9)
        {
            walljumpcount = 0;
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3 && !canWalljump && !canwalljumpleft)
        {

            isGrounded = true;
        }
        if (collision.gameObject.layer == 8 && !isGrounded)
        {
            jumpintime = 0;
            canWalljump = true;
            
        }
        if (collision.gameObject.layer == 9 && !isGrounded)
        {
            jumpintime = 0;
            canwalljumpleft = true;
            
        }
        if (collision.gameObject.layer == 11 && !canWalljump && !canwalljumpleft)
        {
            isGrounded = true;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Respawn")
        {
            respawn = collision.transform;
        }

        for (int i = 0; i < 25; i++)
        {
            if (collision.tag == "Effect" + i.ToString())
            {
                platTag = collision.tag;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < 25; i++)
        {
            if (collision.tag == "Effect" + i.ToString())
            {
                platTag = null;
            }
        }
    }


    public string getTag()
    {

        return platTag;
    }

    internal void Jump()
    {
        i++;
        if (!dead && i % 2 != 0)
        {
            if (!isGrounded && !canWalljump && !canwalljumpleft && walljumptimerleft <=0 && walljumptimer <=0)
            {
                jumpintime = 0.2f;
            }
            if (rolltimer > 0)
            {
                try
                {
                    gameObject.GetComponent<Animator>().SetBool("jump", true);
                }
                catch { }
                rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                isGrounded = false;
                rolltimer = 0;
            }

            try
            {
                gameObject.GetComponent<Animator>().SetBool("jump", true);
            }
            catch { }


            if (isGrounded)
            {



                    rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);


                isjumping = true;
                
                isGrounded = false;
            }
            if ((canWalljump  || walljumptimer > 0)&& walljumpcount <1)
            {

                walljumptimer = 0;
                walljumpcount = 1;
                rb.AddForce(Vector2.right * 20, ForceMode2D.Impulse);

                rb.AddForce(Vector2.up * 35, ForceMode2D.Impulse);

                sr.flipX = true;

                

                canWalljump = false;

            }
            if ((canwalljumpleft || walljumptimerleft > 0) && walljumpcount <1)
            {
                walljumpcount = 1;
                walljumptimerleft = 0;
                rb.AddForce(Vector2.left * 20, ForceMode2D.Impulse);

                rb.AddForce(Vector2.up * 35, ForceMode2D.Impulse);

                
                canwalljumpleft = false;

            }
        }
    
        
    }

    internal void Roll()
    {
        if (!dead && sr != null)
        {

            sr = GetComponent<SpriteRenderer>();

            if (rolltimer <= 0 && sr.flipX == false && isGrounded)
            {

                try
                {
                    gameObject.GetComponent<Animator>().SetBool("dash", true);
                }
                catch { }
                maxV = 20;
                rolltimer = 0.6f;
                rb.AddForce(new Vector2(20, 0f), ForceMode2D.Impulse);
                maxV = 10;



            }
            if (rolltimer <= 0 && sr.flipX == true && isGrounded)
            {
                try
                {
                    gameObject.GetComponent<Animator>().SetBool("dash", true);
                }
                catch { }
                rolltimer = 0.6f;
                rb.AddForce(new Vector2(-20, 0f), ForceMode2D.Impulse);
                maxV = 10;
            }
        }
        
    }
    
}
