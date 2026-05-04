using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class SceneManagerScr : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
