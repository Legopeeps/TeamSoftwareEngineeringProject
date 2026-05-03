using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    public Image menu_button;
    public Sprite normal_menu;
    public Sprite pressed_menu;

    void Start()
    {
        menu_button = GetComponent<Image>();
        menu_button.sprite = normal_menu;
    }

    // Update is called once per frame
    public void onclick()
    {
        menu_button.sprite = pressed_menu;
        Invoke("loadmenu", 0.2f);
    }

    public void loadmenu()
    {
        SceneManager.LoadScene("sc_MainMenu");
    }
}
