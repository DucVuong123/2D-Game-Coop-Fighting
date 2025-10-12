using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Room_Click : MonoBehaviour
{
    [SerializeField] private Button toMenu;
   
    void Awake()
    {
        if (toMenu) toMenu.onClick.AddListener(toMenuClick);
    }

   
    public void toMenuClick()
    {
        SceneManager.LoadScene(2);
    }
}
