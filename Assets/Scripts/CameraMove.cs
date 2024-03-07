using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMove : MonoBehaviour
{
    public CinemachineVirtualCamera room1;
    public CinemachineVirtualCamera room2;
    // Start is called before the first frame update
    void Start()
    {
        room1.Priority = 1;
        room2.Priority = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "room2")
        {
            room1.Priority = 0;
            room2.Priority = 1;
        }
        if (collision.tag == "room1")
        {
            room1.Priority = 1;
            room2.Priority = 0;
        }
    }
}
