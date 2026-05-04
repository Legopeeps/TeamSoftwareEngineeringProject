using UnityEngine;
using UnityEngine.UI;

public class howtoplaypanel : MonoBehaviour
{
    public Image quit_panel;
    public GameObject panel;

    void Start()
    {
        panel.SetActive(false);
    }

    public void exit()
    {
        panel.SetActive(false);
    }

    public void show()
    {
        panel.SetActive(true);
    }
}
