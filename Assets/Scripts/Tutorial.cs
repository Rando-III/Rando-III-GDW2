using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject FirstRoom;
    [SerializeField] GameObject SecondRoom;
    bool triggered; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !triggered)
        {
            triggered = true;
            if (SecondRoom != null)
            {
                FirstRoom.SetActive(false);
                SecondRoom.SetActive(true);
            }
            else
            {
                FirstRoom.SetActive(false);
            }
        }
    }
}
