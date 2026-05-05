using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class ButtonManagerScr : MonoBehaviour
{
    public GameObject howToPlayPanel;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HelpButton()
    {
        if(howToPlayPanel.activeSelf)
        {
            howToPlayPanel.SetActive(false);
        }
        else
        {
            howToPlayPanel.SetActive(true);
        }
    }

}
