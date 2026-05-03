using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class exitbutton : MonoBehaviour
{
    public Image exit_button;
    public Sprite normal_exit;
    public Sprite pressed_exit;

    void Start()
    {
        exit_button = GetComponent<Image>();
        exit_button.sprite = normal_exit;
    }

    // Update is called once per frame
    public void onclick()
    {
        exit_button.sprite = pressed_exit;
        Invoke("exit", 0.2f);
    }

    public void exit()
    {
        Application.Quit();
    }
}
