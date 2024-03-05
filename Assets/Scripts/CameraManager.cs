using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    bool CamFollow = true;
    Vector3 playerPosOld;
    [SerializeField] Camera MainCamera;

    float CamCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CamZone")
        {
            Debug.Log("Enter CamZone");
            CamFollow = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CamZone")
        {
            Debug.Log("Exit CamZone");
            CamFollow = true;
            CamCount = 0;
        }
    }

    public void CamCheck()
    {
        CamCount = 4;
    }


    private void Start()
    {
        playerPosOld = transform.position;
    }

    private void FixedUpdate()
    {
        if (CamFollow)
        {
            if (playerPosOld != transform.position)
            {
                var distance = playerPosOld - transform.position;
                MainCamera.transform.Translate(-distance);
            }

            if (CamCount <= 4)
            {
                CamCount += Time.deltaTime;
            }
            else
            {
                CamCount = 3;
                if (MainCamera.transform.position.x != transform.position.x || MainCamera.transform.position.y != (transform.position.y + 3))
                {
                    var error = MainCamera.transform.position - (transform.position + Vector3.up * 3);
                    MainCamera.transform.Translate(new Vector3(-error.x, -error.y, 0));
                }
            }
        }

        playerPosOld = transform.position;
    }
}
