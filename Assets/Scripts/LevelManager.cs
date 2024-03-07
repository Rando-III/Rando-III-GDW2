using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    float startTime;
    float finalTime;




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

}
