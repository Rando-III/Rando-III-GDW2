using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    float startTime;
    float finalTime;

    float L1Time;
    float L2Time;
    float L3Time;
    public int Level;


    private void Start()
    {
        startTime = Time.time;
    }

    public void LevelEnd()
    {
        finalTime = Time.time - startTime;
    }


    public float GetTime()
    {
        return Time.time - startTime;
    }

    void OnDisable()
    {
        if (Level == 1)
        {
            PlayerPrefs.SetFloat("L1Time", finalTime);
        }
        else if (Level == 2)
        {
            PlayerPrefs.SetFloat("L2Time", finalTime);
        }
        else if (Level == 3)
        {
            PlayerPrefs.SetFloat("L3Time", finalTime);
        }

    }

}
