using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] GameObject highscore;

    float L1Time;
    float L2Time;
    float L3Time;

    float TotalTime;

    float L1High;
    float L2High;
    float L3High;

    float TotalHigh;

    bool NewHighScore;
    private void OnEnable()
    {
        L1Time = PlayerPrefs.GetFloat("L1Time");
        L2Time = PlayerPrefs.GetFloat("L2Time");
        L3Time = PlayerPrefs.GetFloat("L3Time");

        TotalTime = L1Time + L2Time + L3Time;

        L1High = PlayerPrefs.GetFloat("L1High");
        L2High = PlayerPrefs.GetFloat("L2High");
        L3High = PlayerPrefs.GetFloat("L3High");

        TotalHigh = PlayerPrefs.GetFloat("TotalHigh");
    }

    private void Start()
    {
        if (L1Time < L1High)
        {
            PlayerPrefs.SetFloat("L1High", L1Time);
            NewHighScore = true;
        }
        if (L2Time < L2High)
        {
            PlayerPrefs.SetFloat("L2High", L2Time);
            NewHighScore = true;
        }
        if (L3Time < L3High)
        {
            PlayerPrefs.SetFloat("L3High", L3Time);
            NewHighScore = true;
        }
        if (TotalTime < TotalHigh)
        {
            PlayerPrefs.SetFloat("TotalHigh", TotalTime);
            NewHighScore = true;
        }

        if (NewHighScore)
        {
            highscore.SetActive(true);
        }
        else 
        { 
            highscore.SetActive(false); 
        }




    }
}
