using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mouseFollow : MonoBehaviour
{

    [SerializeField] Camera mainCam;

    [SerializeField] GameObject door;
    [SerializeField] GameObject platform;

    Renderer _renderer;
    PlayerInput playerInput;

    public float platformSpeed;

    public int[] effect = { 0, 0, 0, 0, 0};

    public bool[] effectActive = { false, false, false, false, false};

    float effect2PlatformStart;

    Vector3 mouse;
    Vector2 mousePos;

    Transform e1_tf;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        _renderer = GetComponent<Renderer>();

        e1_tf = platform.GetComponent<Transform>();
        effect2PlatformStart = e1_tf.position.x;
    }

    private void FixedUpdate()
    {
        mouse = mainCam.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(mouse.x, mouse.y);
        _renderer.material.color = new Color(effect[2] * 130, effect[0] * 130, effect[1] * 130);


        if (effect[0] > 0 && !effectActive[0]) // start action for effect (called at start of colission)
        {
            effectActive[0] = true;
            door.SetActive(false);
        }
        else if (effectActive[0] && effect[0] <= 0) // end action for effect (called at end of colission)
        {
            effectActive[0] = false;
            door.SetActive(true);
        }

        if (effect[1] > 0) // start action for effect (called at start of colission)
        {
            if (e1_tf.position.x > effect2PlatformStart - 19)
            {
                e1_tf.position = new Vector3(e1_tf.position.x - platformSpeed, e1_tf.position.y, e1_tf.position.z);
            }
        }
        else if (effect[1] <= 0) // end action for effect (called at end of colission)
        {
            if (e1_tf.position.x < effect2PlatformStart)
            {
                e1_tf.position = new Vector3(e1_tf.position.x + platformSpeed, e1_tf.position.y, e1_tf.position.z);
            }
        }

        if (effect[2] > 0 && !effectActive[2]) // start action for effect (called at start of colission)
        {

        }
        else if (effectActive[2] && effect[2] <= 0) // end action for effect (called at end of colission)
        {

        }

        if (effect[3] > 0 && !effectActive[3]) // start action for effect (called at start of colission)
        {

        }
        else if (effectActive[3] && effect[3] <= 0) // end action for effect (called at end of colission)
        {

        }

        if (effect[4] > 0 && !effectActive[4]) // start action for effect (called at start of colission)
        {

        }
        else if (effectActive[4] && effect[4] <= 0) // end action for effect (called at end of colission)
        {

        }
    }


    private void OnMouse(InputValue value)
    {
        mousePos = value.Get<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.tag == "Effect1")
        {
            effect[1]++;
        }
        else if (collision.tag == "Effect2")
        {
            effect[2]++;
        }
        else if (collision.tag == "Effect0")
        {
            effect[0]++;
        }
        else if (collision.tag == "Effect3")
        {
            effect[3]++;
        }
        else if (collision.tag == "Effect4")
        {
            effect[4]++;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.tag == "Effect1")
        {
            effect[1]--;
        }
        else if (collision.tag == "Effect2")
        {
            effect[2]--;
        }
        else if (collision.tag == "Effect0")
        {
            effect[0]--;
        }
        else if (collision.tag == "Effect3")
        {
            effect[3]--;
        }
        else if (collision.tag == "Effect4")
        {
            effect[4]--;
        }

    }
}
