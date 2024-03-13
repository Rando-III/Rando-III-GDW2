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
    public int[] effectType = { 0, 0, 0, 0, 0 };
    public string[] TYPE_INFO = { "0 = No Effect", "1 = Door", "2 = Left/Right Platform", "3 = Up/Down Platform" };
    bool[] effectActive = { false, false, false, false, false};
    public bool[] effectEnabled = { false, false, false, false, false };
    public string[] OBJECT_INFO = { "DOOR = all objects will be disabled on enable", "MOVING PLATFORM = First Object MUST be the Platform followed by the startpoint and end point" };
    public GameObject[] Effect1Objects;
    public GameObject[] Effect2Objects;
    public GameObject[] Effect3Objects;
    public GameObject[] Effect4Objects;
    public GameObject[] Effect5Objects;



    Vector3 mouse;
    Vector2 mousePos;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        _renderer = GetComponent<Renderer>();

    }

    private void FixedUpdate()
    {
        mouse = mainCam.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(mouse.x, mouse.y);
        _renderer.material.color = new Color(effect[2] * 130, effect[0] * 130, effect[1] * 130);

        if (effectEnabled[0])
        {
            effectCheck(Effect1Objects, 0);
        }
        if (effectEnabled[1])
        {
            effectCheck(Effect2Objects, 1);
        }
        if (effectEnabled[2])
        {
            effectCheck(Effect3Objects, 2);
        }
        if (effectEnabled[3])
        {
            effectCheck(Effect4Objects, 3);
        }
        if (effectEnabled[4])
        {
            effectCheck(Effect5Objects, 4);
        }
    }

    void effectCheck(GameObject[] Objects, int num)
    {
        if (effectType[num] == 1)
        {
            EffectDoor(Objects, num);
        }
        else if (effectType[num] == 2) 
        { 
            EffectPlatformH(Objects, num); 
        }
        else if (effectType[num] == 3)
        {
            EffectPlatformV(Objects, num);
        }
        else
        {
            effectEnabled[num] = false;
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



        void EffectDoor(GameObject[] Objects, int num)
        {
            if (effect[num] > 0 && !effectActive[num]) // start action for effect (called at start of colission)
            {
                effectActive[num] = true;

                foreach (GameObject i in Objects)
                {
                    i.SetActive(false);
                }

            }
            else if (effectActive[num] && effect[num] <= 0) // end action for effect (called at end of colission)
            {
                effectActive[num] = false;
                foreach (GameObject i in Objects)
                {
                    i.SetActive(true);
                }
            }
        }

        void EffectPlatformH(GameObject[] Objects, int num)
        {
            if (Objects[1].transform.position.x - Objects[2].transform.position.x < 0)
            {
                if (effect[num] > 0) // start action for effect (called at start of colission)
                {


                    if (Objects[0].transform.position.x < Objects[2].transform.position.x)
                    {
                        Objects[0].transform.Translate(new Vector3(platformSpeed, 0, 0));
                        player.platformLock(platformSpeed, true);

                    }

                }

                else if (effect[num] <= 0) // end action for effect (called at end of colission)
                {

                    if (Objects[0].transform.position.x > Objects[1].transform.position.x)
                    {
                        Objects[0].transform.Translate(new Vector3(-platformSpeed, 0, 0));
                        player.platformLock(-platformSpeed, true);

                    }

                }
            }
            else
            {
                if (effect[num] > 0)
                {


                    if (Objects[0].transform.position.x > Objects[2].transform.position.x)
                    {

                        Objects[0].transform.Translate(new Vector3(-platformSpeed, 0, 0));
                        player.platformLock(-platformSpeed, true);

                    }

                }
                else if (effect[num] <= 0) // end action for effect (called at end of colission)
                {

                    if (Objects[0].transform.position.x < Objects[1].transform.position.x)
                    {
                        Objects[0].transform.Translate(new Vector3(platformSpeed, 0, 0));
                        player.platformLock(platformSpeed, true);

                    }

                }
            }
        }

        void EffectPlatformV(GameObject[] Objects, int num)
        {
            if (Objects[1].transform.position.y - Objects[2].transform.position.y < 0)
            {
                // UP
                if (effect[num] > 0) // start action for effect (called at start of colission)
                {
                    if (Objects[0].transform.position.y < Objects[2].transform.position.y)
                    {
                        Objects[0].transform.Translate(new Vector3(0, platformSpeed, 0));
                        player.platformLock(platformSpeed, false); 

                    }

                }

                else if (effect[num] <= 0) // end action for effect (called at end of colission)
                {

                    if (Objects[0].transform.position.y > Objects[1].transform.position.y)
                    {
                        Objects[0].transform.Translate(new Vector3( 0, -platformSpeed, 0));
                        player.platformLock(-platformSpeed, false); 

                    }

                }
            }
            else
            {
                // DOWN
                if (effect[num] > 0)
                {


                    if (Objects[0].transform.position.y > Objects[2].transform.position.y)
                    {

                        Objects[0].transform.Translate(new Vector3( 0, -platformSpeed, 0));
                        player.platformLock(-platformSpeed, false);

                    }

                }
                else if (effect[num] <= 0) // end action for effect (called at end of colission)
                {

                    if (Objects[0].transform.position.y < Objects[1].transform.position.y)
                    {
                        Objects[0].transform.Translate(new Vector3( 0, platformSpeed, 0));
                        player.platformLock(platformSpeed, false);

                    }

                }
            }
        }


    
}
