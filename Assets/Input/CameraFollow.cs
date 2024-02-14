using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public GameObject playermove;
    public bool camerafollow = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.position.x > 1.3)
        {
            camerafollow = true;


        }
        else
        {
            camerafollow = false;
        }
        if (camerafollow == true) 
        {
            gameObject.transform.position = player.position + new Vector3(0, 2, -10);
        }
        
    }
}
