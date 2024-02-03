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
    public float maxVelocityX = 10;
    public float minVelocityX = -10;
    public bool isjumping = false;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Init(this);
        InputManager.SetGameControls();
        rb = GetComponent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    void Update()
    {

        rb.AddForce((new Vector2(10,0) * _moveDirection));
        if (rb.velocity.x >= 10)
        {
            rb.velocity -= new Vector2( 1,0);
        }
        if (rb.velocity.x <= -10)
        {
            rb.velocity += new Vector2(1, 0);
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
        }
        
    }
}
