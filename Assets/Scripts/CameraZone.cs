using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [SerializeField] GameObject CameraPos;
    [SerializeField] Camera MainCamera;
    [SerializeField] Camera Cam;
    [SerializeField] CameraManager CameraManager;
    public float speed = 1;
    bool GoToCamPos = false;
    bool GoToPlayer = false;
    Vector3 playerPos;
    Vector3 camposplayer;
    Vector3 returnDistance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GoToCamPos = true;
            CameraManager.CamSizeChange(Cam.orthographicSize);
            GoToPlayer = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GoToPlayer = true;
            GoToCamPos = false;
            playerPos = collision.transform.position;
            playerPos.y += 3;
            camposplayer = MainCamera.transform.position;
            returnDistance = MainCamera.transform.position - playerPos;

        }
    }

    private void FixedUpdate()
    {
        if (GoToCamPos)
        {
            MainCamera.transform.position = Vector3.MoveTowards(MainCamera.transform.position, CameraPos.transform.position, speed);

        }

        if (GoToPlayer)
        {

            if (playerPos != Vector3.zero)
            {
                if (returnDistance.x > speed)
                {
                    MainCamera.transform.Translate(-speed, 0, 0);
                    returnDistance.x -= speed;
                }
                else if (returnDistance.x < -speed)
                {
                    MainCamera.transform.Translate(speed, 0, 0);
                    returnDistance.x -= -speed;
                }
                else
                {
                    MainCamera.transform.Translate(-returnDistance.x, 0, 0);
                    returnDistance.x = 0;
                }

                if (returnDistance.y > speed)
                {
                    MainCamera.transform.Translate(0, -speed, 0);
                    returnDistance.y -= speed;
                }
                else if (returnDistance.y < -speed)
                {
                    MainCamera.transform.Translate(0, speed, 0);
                    returnDistance.y -= -speed;
                }
                else
                {
                    MainCamera.transform.Translate(0, -returnDistance.y, 0);
                    returnDistance.y = 0;
                }

                if (returnDistance.x == 0 && returnDistance.y == 0)
                {
                    GoToPlayer = false;
                    CameraManager.CamCheck();
                }
            }
            else
            {
                GoToPlayer = false;
                CameraManager.CamCheck();
            }
        }
    }

}
