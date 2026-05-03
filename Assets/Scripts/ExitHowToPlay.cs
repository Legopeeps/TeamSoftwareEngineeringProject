using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitHowToPlay : MonoBehaviour
{
    public void onclick()
    {
        SceneManager.LoadScene("sc_MainMenu");
    }
}
