using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mouseFollow : MonoBehaviour
{

    [SerializeField] Camera mainCam;
    [SerializeField] GameObject[] effectObjects;

    [SerializeField] GameObject door;

    Renderer _renderer;
    PlayerInput playerInput;

    public int green;
    public int blue;
    public int red;
    public int testColour;
    


    Vector3 mouse;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        _renderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(mouse.x, mouse.y);
        _renderer.material.color = new Color(red * 130, green * 130, blue * 130);

        if (green > 0 || red > 0 || blue > 0 || testColour > 0)
        {

        }
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
            door.transform.Translate(0, 10, 0);
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
            door.transform.Translate(0, -10, 0);
        }
        
    }
}
