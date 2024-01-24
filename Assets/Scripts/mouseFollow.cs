using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mouseFollow : MonoBehaviour
{

    [SerializeField] Camera mainCam;

    [SerializeField] Transform _transform;
    [SerializeField] Renderer _renderer;

    int green;
    int blue;
    int red;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        _renderer.material.color = new Color (red * 130, green * 130, blue * 130);
    }

    private void OnMouse(InputValue value)
    {
        var mouse_pos = value.Get<Vector2>();
        var mouse = mainCam.ScreenToWorldPoint(mouse_pos);
        
        _transform.position = new Vector3(mouse.x, mouse.y);
        

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
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
}
