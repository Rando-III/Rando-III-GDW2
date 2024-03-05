using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    bool CamFollow = true;
    Vector3 playerPosOld;
    [SerializeField] Camera MainCamera;

    public float CamSize = 8f;

    float CamCount = 4;
    float CamSpeed = 0.1f;
    float CamChange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CamZone")
        {
            CamFollow = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CamZone")
        {
            CamFollow = true;
            CamCount = 0;
        }
    }

    public void CamCheck()
    {
        CamCount = 4;
        CamSizeChange(CamSize);
    }

    public void CamSizeChange(float size)
    {
        if (size != MainCamera.orthographicSize)
        {
            CamChange = (MainCamera.orthographicSize - size);
        }
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

        if (CamChange != 0)
        {
            if (CamChange >= CamSpeed)
            {
                MainCamera.orthographicSize -= CamSpeed;
                CamChange -= CamSpeed;
            }
            else if (CamChange <= -CamSpeed)
            {
                MainCamera.orthographicSize += CamSpeed;
                CamChange += CamSpeed;
            }
            else
            {
                MainCamera.orthographicSize -= CamChange;
                CamChange = 0;
            }
        }
        else if (CamFollow)
        {
            MainCamera.orthographicSize = CamSize;
        }

        playerPosOld = transform.position;
    }
}
