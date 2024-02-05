using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().rolltimer > 0.1) 
        {
        
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (player.GetComponent<Player>().rolltimer < 0.1)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        
    }
}
