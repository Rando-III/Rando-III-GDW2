using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public FloatSO collectible;

    private void Start()
    {
        collectible.value = 0;
    }
    public void Level1()
    {
        SceneManager.LoadScene(1);
    }

}
