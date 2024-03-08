using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mouseFollow : MonoBehaviour
{

    [SerializeField] Camera mainCam;
    [SerializeField] Player player;

    Renderer _renderer;
    PlayerInput playerInput;

    public float platformSpeed;

    int[] effect = { 0, 0, 0, 0, 0};

    bool[] effectActive = { false, false, false, false, false};
    public bool[] effectEnabled = { false, false, false, false, false };
    public GameObject[] Effect1Door;
    public GameObject[] Effect2Platform;
    public GameObject[] Effect3Null;
    public GameObject[] Effect4Null;
    public GameObject[] Effect5Null;

    bool Effect2Flip;

    Vector3 mouse;
    Vector2 mousePos;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        _renderer = GetComponent<Renderer>();

        if (effectEnabled[1])
        {
            Effect2Platform[0].transform.position = Effect2Platform[1].transform.position;

            if (Effect2Platform[1].transform.position.x - Effect2Platform[2].transform.position.x < 0)
            {
                Effect2Flip = true;
            }
            else 
            { 
                Effect2Flip = false; 
            }
        }
    }

    private void FixedUpdate()
    {
        mouse = mainCam.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(mouse.x, mouse.y);
        _renderer.material.color = new Color(effect[2] * 130, effect[0] * 130, effect[1] * 130);

        if (effectEnabled[0])
        {
            if (effect[0] > 0 && !effectActive[0]) // start action for effect (called at start of colission)
            {
                effectActive[0] = true;

                foreach (GameObject i in Effect1Door)
                {
                    i.SetActive(false);
                }

            }
            else if (effectActive[0] && effect[0] <= 0) // end action for effect (called at end of colission)
            {
                effectActive[0] = false;
                foreach (GameObject i in Effect1Door)
                {
                    i.SetActive(true);
                }
            }
        }

        if (effectEnabled[1])
        {
            if (!Effect2Flip)
            {
                if (effect[1] > 0) // start action for effect (called at start of colission)
                {

                    
                    if (Effect2Platform[0].transform.position.x > Effect2Platform[2].transform.position.x)
                    {

                        Effect2Platform[0].transform.Translate(new Vector3(-platformSpeed, 0, 0));
                        player.platformLock(-platformSpeed);

                    }
                    
                }
                else if (effect[1] <= 0) // end action for effect (called at end of colission)
                {

                    if (Effect2Platform[0].transform.position.x < Effect2Platform[1].transform.position.x)
                    {
                        Effect2Platform[0].transform.Translate(new Vector3(platformSpeed, 0, 0));
                        player.platformLock(platformSpeed);

                    }
                    
                }
            }
            else
            {
                if (effect[1] > 0) // start action for effect (called at start of colission)
                {


                    if (Effect2Platform[0].transform.position.x < Effect2Platform[2].transform.position.x)
                    {
                        Effect2Platform[0].transform.Translate(new Vector3(platformSpeed, 0, 0));
                        player.platformLock(platformSpeed);

                    }
                    
                }

                else if (effect[1] <= 0) // end action for effect (called at end of colission)
                {

                    if (Effect2Platform[0].transform.position.x > Effect2Platform[1].transform.position.x)
                    {
                        Effect2Platform[0].transform.Translate(new Vector3(-platformSpeed, 0, 0));
                        player.platformLock(-platformSpeed);

                    }
                    
                }
            }
        }

        if (effectEnabled[2])
        {
            if (effect[2] > 0 && !effectActive[2]) // start action for effect (called at start of colission)
            {

            }
            else if (effectActive[2] && effect[2] <= 0) // end action for effect (called at end of colission)
            {

            }
        }

        if (effectEnabled[3])
        {
            if (effect[3] > 0 && !effectActive[3]) // start action for effect (called at start of colission)
            {

            }
            else if (effectActive[3] && effect[3] <= 0) // end action for effect (called at end of colission)
            {

            }
        }

        if (effectEnabled[4])
        {
            if (effect[4] > 0 && !effectActive[4]) // start action for effect (called at start of colission)
            {

            }
            else if (effectActive[4] && effect[4] <= 0) // end action for effect (called at end of colission)
            {

            }
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
