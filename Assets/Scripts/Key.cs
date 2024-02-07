using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject uiIcon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            uiIcon.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
