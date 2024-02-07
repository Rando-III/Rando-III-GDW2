using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] GameObject icon;
    [SerializeField] GameObject door;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (icon.activeSelf)
            {
                Destroy(icon);
                Destroy(door);
            }
        }
    }
}
