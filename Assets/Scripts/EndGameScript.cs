using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    public bool countdown;
    public float time;
    public GameObject player;
    public string level;

    // Start is called before the first frame update
    void Start()
    {
        time = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown)
        {
            time -= Time.deltaTime;
        }
        if (time <= 0)
        {
            End();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartEnd();
        }
    }
    void StartEnd()
    {
        player.SetActive(false);
        countdown = true;
    }
    void End()
    {
        SceneManager.LoadScene(level);
    }
}
