using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject PauseScreen;
    [SerializeField] TMP_Text PauseTime;
    [SerializeField] LevelManager LevelManager;

    bool isPaused = false;

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

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
            var temp = LevelManager.GetTime();
            temp = Mathf.Round(temp * 10.0f) * 0.1f;
            PauseTime.text = temp.ToString();


            int mins = Mathf.FloorToInt(temp / 60);
            int secs = Mathf.FloorToInt(temp % 60);

            if (secs < 10)
            {
                PauseTime.text = mins.ToString() + ":0" + secs.ToString();
            }
            else
            {
                PauseTime.text = mins.ToString() + ":" + secs.ToString();
            }


            PauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OnReset()
    {
        SceneManager.LoadScene(0);
    }
}
