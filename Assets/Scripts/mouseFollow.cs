using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mouseFollow : MonoBehaviour
{

    [SerializeField] Camera mainCam;

    [SerializeField] Transform _transform;
    [SerializeField] Renderer _renderer;

    [SerializeField] PlayerInput playerInput;

    int green;
    int blue;
    int red;

    Vector3 mouse;


    private void FixedUpdate()
    {
        _transform.position = new Vector3(mouse.x, mouse.y);
        _renderer.material.color = new Color(red * 130, green * 130, blue * 130);
    }



    private void OnMouse(InputValue value)
    {
        var mouse_pos = value.Get<Vector2>();
        mouse = mainCam.ScreenToWorldPoint(mouse_pos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.tag == "Blue")
        {
            blue++;
        }
        else if (collision.tag == "Red")
        {
            red++;
        }
        else if (collision.tag == "Green")
        {
            green++;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.tag == "Blue")
        {
            blue--;
        }
        else if (collision.tag == "Red")
        {
            red--;
        }
        else if (collision.tag == "Green")
        {
            green--;
        }
        
    }
}
