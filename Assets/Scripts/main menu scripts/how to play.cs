using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class howtoplay : MonoBehaviour
{
    public Image help_button;
    public Sprite normal_help;
    public Sprite pressed_help;

    void Start()
    {
        help_button = GetComponent<Image>();
        help_button.sprite = normal_help;
    }

    
    public void onclick()
    {
        help_button.sprite = pressed_help;
        Invoke("load", 0.2f);

    }

    public void load()
    {
        SceneManager.LoadScene("sc_HowToPlay");
    }


}
