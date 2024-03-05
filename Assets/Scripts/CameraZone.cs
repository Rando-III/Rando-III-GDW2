using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [SerializeField] GameObject CameraPos;
    [SerializeField] Camera MainCamera;
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
            if (MainCamera.transform.position != CameraPos.transform.position)
            {
                var distance = MainCamera.transform.position - CameraPos.transform.position;
                if (distance.x > speed)
                {
                    MainCamera.transform.Translate(-speed, 0, 0);
                }
                else if (distance.x < -speed)
                {
                    MainCamera.transform.Translate(speed, 0, 0);
                }
                else
                {
                    MainCamera.transform.Translate(distance.x * -1, 0, 0);
                    distance.x = 0;
                }

                if (distance.y > speed)
                {
                    MainCamera.transform.Translate(0, -speed, 0);
                }
                else if (distance.y < -speed)
                {
                    MainCamera.transform.Translate(0, speed, 0);
                }
                else
                {
                    MainCamera.transform.Translate(distance.y * -1, 0, 0);
                    distance.y = 0;
                }

                if (distance.x == 0 && distance.y == 0)
                {
                    GoToCamPos = false;
                }
            }
            else
            {
                GoToCamPos = false;
            }
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
