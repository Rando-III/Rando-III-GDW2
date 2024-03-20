using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class mouseFollow : MonoBehaviour
{

    [SerializeField] Camera mainCam;
    [SerializeField] Player player;

    Renderer _renderer;
    PlayerInput playerInput;

    public float platformSpeed;

    int[] effect = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] effectType = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public string[] TYPE_HELP = { "0 = No Effect", "1 = Door", "2 = Left/Right Platform", "3 = Up/Down Platform", "4 = On Off Blocks (OnClick)", "5 = L/R Constant moving platform", "6 = U/D Constant moving platform", "7 = L/R Stopable Constant moving platform", "8 = U/D Stopable Constant moving platform" };
    bool[] effectActive = { false, false, false, false, false, false, false, false, false, false };
    public bool[] effectEnabled = { false, false, false, false, false, false, false, false, false, false };
    public string[] OBJECT_HELP = { "DOOR = all objects will be disabled on enable", "MOVING PLATFORMS (ALL TYPES (2,3,5 - 8))", "1. Platform", "2. Start Point", "3. End Point", "ON OFF = objects will flip active state" };
    bool[] directionFlip = { false, false, false, false, false, false, false, false, false, false };
    float[] moveStop = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public float stopTime = 3;
    public GameObject[] Effect1Objects;
    public GameObject[] Effect2Objects;
    public GameObject[] Effect3Objects;
    public GameObject[] Effect4Objects;
    public GameObject[] Effect5Objects;
    public GameObject[] Effect6Objects;
    public GameObject[] Effect7Objects;
    public GameObject[] Effect8Objects;
    public GameObject[] Effect9Objects;
    public GameObject[] Effect10Objects;

    bool Click;

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
        if (effectEnabled[5])
        {
            effectCheck(Effect6Objects, 5);
        }
        if (effectEnabled[6])
        {
            effectCheck(Effect7Objects, 6);
        }
        if (effectEnabled[7])
        {
            effectCheck(Effect8Objects, 7);
        }
        if (effectEnabled[8])
        {
            effectCheck(Effect9Objects, 8);
        }
        if (effectEnabled[9])
        {
            effectCheck(Effect10Objects, 9);
        }
        Click = false;
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
        else if (effectType[num] == 4)
        {
            OnOffBlocks(Objects, num);
        }
        else if (effectType[num] == 5)
        {
            ConstantPlatformH(Objects, num);
        }
        else if (effectType[num] == 6)
        {
            ConstantPlatformV(Objects, num);
        }
        else if (effectType[num] == 7)
        {
            StopPlatformH(Objects, num);
        }
        else if (effectType[num] == 8)
        {
            StopPlatformV(Objects, num);
        }
        else
        {
            effectEnabled[num] = false;
            Debug.Log($"ERROR Effect {num} was set active but no valid effect type was set");
        }
    }

    private void OnMouse(InputValue value)
    {
        mousePos = value.Get<Vector2>();
    }

    void OnClick()
    {
        Click = true;
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
        else if (collision.tag == "Effect5")
        {
            effect[5]++;
        }
        else if (collision.tag == "Effect6")
        {
            effect[6]++;
        }
        else if (collision.tag == "Effect7")
        {
            effect[7]++;
        }
        else if (collision.tag == "Effect8")
        {
            effect[8]++;
        }
        else if (collision.tag == "Effect9")
        {
            effect[9]++;
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
        else if (collision.tag == "Effect5")
        {
            effect[5]--;
        }
        else if (collision.tag == "Effect6")
        {
            effect[6]--;
        }
        else if (collision.tag == "Effect7")
        {
            effect[7]--;
        }
        else if (collision.tag == "Effect8")
        {
            effect[8]--;
        }
        else if (collision.tag == "Effect9")
        {
            effect[9]--;
        }
    }

    void OnOffBlocks(GameObject[] Objects, int num)
    {
        if (effect[num] > 0 && Click)
        {
            Click = false;
            foreach (GameObject obj in Objects)
            {
                if (obj.activeInHierarchy == false)
                {
                    obj.SetActive(true);
                }
                else
                {
                    obj.SetActive(false);
                }
            }
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
                    player.platformLock(platformSpeed, true, "Effect" + num.ToString());
                }
            }
            else if (effect[num] <= 0) // end action for effect (called at end of colission)
            {
                if (Objects[0].transform.position.x > Objects[1].transform.position.x)
                {
                    Objects[0].transform.Translate(new Vector3(-platformSpeed, 0, 0));
                    player.platformLock(-platformSpeed, true, "Effect" + num.ToString());
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
                    player.platformLock(-platformSpeed, true, "Effect" + num.ToString());
                }
            }
            else if (effect[num] <= 0) // end action for effect (called at end of colission)
            {
                if (Objects[0].transform.position.x < Objects[1].transform.position.x)
                {
                    Objects[0].transform.Translate(new Vector3(platformSpeed, 0, 0));
                    player.platformLock(platformSpeed, true, "Effect" + num.ToString());
                }
            }
        }
    }

    void EffectPlatformV(GameObject[] Objects, int num)
    {
        if (Objects[1].transform.position.y - Objects[2].transform.position.y < 0)
        {
            if (effect[num] > 0) // start action for effect (called at start of colission)
            {
                if (Objects[0].transform.position.y < Objects[2].transform.position.y)
                {
                    Objects[0].transform.Translate(new Vector3(0, platformSpeed, 0));
                    player.platformLock(platformSpeed, false, "Effect" + num.ToString());
                }
            }
            else if (effect[num] <= 0) // end action for effect (called at end of colission)
            {
                if (Objects[0].transform.position.y > Objects[1].transform.position.y)
                {
                    Objects[0].transform.Translate(new Vector3(0, -platformSpeed, 0));
                    player.platformLock(-platformSpeed, false, "Effect" + num.ToString());
                }
            }
        }
        else
        {
            if (effect[num] > 0)
            {
                if (Objects[0].transform.position.y > Objects[2].transform.position.y)
                {
                    Objects[0].transform.Translate(new Vector3(0, -platformSpeed, 0));
                    player.platformLock(-platformSpeed, false, "Effect" + num.ToString());
                }
            }
            else if (effect[num] <= 0) // end action for effect (called at end of colission)
            {
                if (Objects[0].transform.position.y < Objects[1].transform.position.y)
                {
                    Objects[0].transform.Translate(new Vector3(0, platformSpeed, 0));
                    player.platformLock(platformSpeed, false, "Effect" + num.ToString());
                }
            }
        }
    }


    void ConstantPlatformH(GameObject[] Objects, int num)
    {
        if (Objects[1].transform.position.x - Objects[2].transform.position.x < 0)
        {
            //left right

            if (Objects[0].transform.position.x < Objects[1].transform.position.x)
            {
                directionFlip[num] = true;
            }
            else if (Objects[0].transform.position.x > Objects[2].transform.position.x)
            {
                directionFlip[num] = false;
            }

            if (directionFlip[num])
            {
                Objects[0].transform.Translate(new Vector3(platformSpeed, 0, 0));
                player.platformLock(platformSpeed, true, "Effect" + num.ToString());
            }
            else
            {
                Objects[0].transform.Translate(new Vector3(-platformSpeed, 0, 0));
                player.platformLock(-platformSpeed, true, "Effect" + num.ToString());
            }
        }
        else
        {
            if (Objects[0].transform.position.x > Objects[1].transform.position.x)
            {
                directionFlip[num] = true;
            }
            else if (Objects[0].transform.position.x < Objects[2].transform.position.x)
            {
                directionFlip[num] = false;
            }

            if (!directionFlip[num])
            {
                Objects[0].transform.Translate(new Vector3(platformSpeed, 0, 0));
                player.platformLock(platformSpeed, true, "Effect" + num.ToString());
            }
            else
            {
                Objects[0].transform.Translate(new Vector3(-platformSpeed, 0, 0));
                player.platformLock(-platformSpeed, true, "Effect" + num.ToString());
            }
        }
    }


    void ConstantPlatformV(GameObject[] Objects, int num)
    {
        if (Objects[1].transform.position.y - Objects[2].transform.position.y < 0)
        {
            //Up Down

            if (Objects[0].transform.position.y > Objects[1].transform.position.y)
            {
                directionFlip[num] = true;
            }
            else if (Objects[0].transform.position.y < Objects[2].transform.position.y)
            {
                directionFlip[num] = false;
            }

            if (directionFlip[num])
            {
                Objects[0].transform.Translate(new Vector3(0, -platformSpeed, 0));
                player.platformLock(-platformSpeed, false, "Effect" + num.ToString());
            }
            else
            {
                Objects[0].transform.Translate(new Vector3(0, platformSpeed, 0));
                player.platformLock(platformSpeed, false, "Effect" + num.ToString());
            }
        }
        else
        {
            if (Objects[0].transform.position.y < Objects[1].transform.position.y)
            {
                directionFlip[num] = true;
            }
            else if (Objects[0].transform.position.y > Objects[2].transform.position.y)
            {
                directionFlip[num] = false;
            }

            if (!directionFlip[num])
            {
                Objects[0].transform.Translate(new Vector3(0, -platformSpeed, 0));
                player.platformLock(-platformSpeed, false, "Effect" + num.ToString());
            }
            else
            {
                Objects[0].transform.Translate(new Vector3(0, platformSpeed, 0));
                player.platformLock(platformSpeed, false, "Effect" + num.ToString());
            }
        }
    }

    void StopPlatformH(GameObject[] Objects, int num)
    {
        if (effect[num] > 0 && Click)
        {
            moveStop[num] = stopTime;
        }

        if (moveStop[num] <= 0)
        { 
            if (Objects[1].transform.position.x - Objects[2].transform.position.x < 0)
            {
                //left right

                if (Objects[0].transform.position.x < Objects[1].transform.position.x)
                {
                    directionFlip[num] = true;
                }
                else if (Objects[0].transform.position.x > Objects[2].transform.position.x)
                {
                    directionFlip[num] = false;
                }

                if (directionFlip[num])
                {
                    Objects[0].transform.Translate(new Vector3(platformSpeed, 0, 0));
                    player.platformLock(platformSpeed, true, "Effect" + num.ToString());
                }
                else
                {
                    Objects[0].transform.Translate(new Vector3(-platformSpeed, 0, 0));
                    player.platformLock(-platformSpeed, true, "Effect" + num.ToString());
                }

            }
            else
            {
                if (Objects[0].transform.position.x > Objects[1].transform.position.x)
                {
                    directionFlip[num] = true;
                }
                else if (Objects[0].transform.position.x < Objects[2].transform.position.x)
                {
                    directionFlip[num] = false;
                }

                if (!directionFlip[num])
                {
                    Objects[0].transform.Translate(new Vector3(platformSpeed, 0, 0));
                    player.platformLock(platformSpeed, true, "Effect" + num.ToString());
                }
                else
                {
                    Objects[0].transform.Translate(new Vector3(-platformSpeed, 0, 0));
                    player.platformLock(-platformSpeed, true, "Effect" + num.ToString());
                }
            }
        }
        else
        {
            moveStop[num] -= Time.deltaTime;
        }
    }

    void StopPlatformV(GameObject[] Objects, int num)
    {
        if (effect[num] > 0 && Click)
        {
            moveStop[num] = stopTime;
        }

        if (moveStop[num] <= 0)
        {
            if (Objects[1].transform.position.y - Objects[2].transform.position.y < 0)
            {
                //Up Down

                if (Objects[0].transform.position.y > Objects[1].transform.position.y)
                {
                    directionFlip[num] = true;
                }
                else if (Objects[0].transform.position.y < Objects[2].transform.position.y)
                {
                    directionFlip[num] = false;
                }

                if (directionFlip[num])
                {
                    Objects[0].transform.Translate(new Vector3(0, -platformSpeed, 0));
                    player.platformLock(-platformSpeed, false, "Effect" + num.ToString());
                }
                else
                {
                    Objects[0].transform.Translate(new Vector3(0, platformSpeed, 0));
                    player.platformLock(platformSpeed, false, "Effect" + num.ToString());
                }
            }
            else
            {
                if (Objects[0].transform.position.y < Objects[1].transform.position.y)
                {
                    directionFlip[num] = true;
                }
                else if (Objects[0].transform.position.y > Objects[2].transform.position.y)
                {
                    directionFlip[num] = false;
                }

                if (!directionFlip[num])
                {
                    Objects[0].transform.Translate(new Vector3(0, -platformSpeed, 0));
                    player.platformLock(-platformSpeed, false, "Effect" + num.ToString());
                }
                else
                {
                    Objects[0].transform.Translate(new Vector3(0, platformSpeed, 0));
                    player.platformLock(platformSpeed, false, "Effect" + num.ToString());
                }
            }
        }
        else
        {
            moveStop[num] -= Time.deltaTime;
        }
    }
}
