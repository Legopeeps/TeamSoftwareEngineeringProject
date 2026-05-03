using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class playgame : MonoBehaviour
{
    public Image play_button;
    public Sprite normal_play;
    public Sprite pressed_play;

    void Start()
    {
        play_button = GetComponent<Image>();
        play_button.sprite = normal_play;
    }
    
    public void onclick()
    {
        play_button.sprite = pressed_play;
        Invoke("loadscene", 0.2f);
    }

    public void loadscene()
    {
        SceneManager.LoadScene("sc_Game");
    }

}
