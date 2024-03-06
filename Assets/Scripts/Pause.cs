using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject PauseScreen;
    bool isPaused = false;



    public void OnEscape()
    {
        if (isPaused)
        {
            isPaused = false;
            PauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            isPaused = true;
            PauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
