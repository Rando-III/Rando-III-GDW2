using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] GameObject L1highscore;
    [SerializeField] GameObject L2highscore;
    [SerializeField] GameObject L3highscore;
    [SerializeField] GameObject highscore;

    [SerializeField] TMP_Text L1high;
    [SerializeField] TMP_Text L2high;
    [SerializeField] TMP_Text L3high;
    [SerializeField] TMP_Text high;

    [SerializeField] TMP_Text L1hight;
    [SerializeField] TMP_Text L2hight;
    [SerializeField] TMP_Text L3hight;
    [SerializeField] TMP_Text hight;

    float L1Time;
    float L2Time;
    float L3Time;

    float TotalTime;

    float L1High;
    float L2High;
    float L3High;

    float TotalHigh;

    bool NewHighScore;


    private void Start()
    {
        L1Time = PlayerPrefs.GetFloat("L1Time");
        L2Time = PlayerPrefs.GetFloat("L2Time");
        L3Time = PlayerPrefs.GetFloat("L3Time");

        TotalTime = L1Time + L2Time + L3Time;

        L1High = PlayerPrefs.GetFloat("L1High");
        L2High = PlayerPrefs.GetFloat("L2High");
        L3High = PlayerPrefs.GetFloat("L3High");

        TotalHigh = PlayerPrefs.GetFloat("TotalHigh");

        if (L1Time < L1High)
        {
            L1highscore.SetActive(true);
            PlayerPrefs.SetFloat("L1High", L1Time);
            NewHighScore = true;
        }
        if (L2Time < L2High)
        {
            L2highscore.SetActive(true);
            PlayerPrefs.SetFloat("L2High", L2Time);
            NewHighScore = true;
        }
        if (L3Time < L3High)
        {
            L3highscore.SetActive(true);
            PlayerPrefs.SetFloat("L3High", L3Time);
            NewHighScore = true;
        }
        if (TotalTime < TotalHigh)
        {
            highscore.SetActive(true);
            PlayerPrefs.SetFloat("TotalHigh", TotalTime);
            NewHighScore = true;
        }


  
        L1Time = Mathf.Round(L1Time * 10.0f) * 0.1f;
        L1hight.text = L1Time.ToString();

        L2Time = Mathf.Round(L2Time * 10.0f) * 0.1f;
        L1hight.text = L2Time.ToString();

        L3Time = Mathf.Round(L3Time * 10.0f) * 0.1f;
        L1hight.text = L3Time.ToString();

        TotalTime = Mathf.Round(TotalTime * 10.0f) * 0.1f;
        L1hight.text = TotalTime.ToString();


        L1High = Mathf.Round(L1High * 10.0f) * 0.1f;
        L1hight.text = L1High.ToString();

        L2High = Mathf.Round(L2High * 10.0f) * 0.1f;
        L1hight.text = L2High.ToString();

        L3High = Mathf.Round(L3High * 10.0f) * 0.1f;
        L1hight.text = L3High.ToString();

        TotalHigh = Mathf.Round(TotalHigh * 10.0f) * 0.1f;
        L1hight.text = TotalHigh.ToString();



    }

    public void mainmenu()
    {
        SceneManager.LoadScene(0);
    }
}
