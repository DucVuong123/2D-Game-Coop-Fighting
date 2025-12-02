using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGoToMainSceen : MonoBehaviour
{
   [SerializeField] private bool isCharacter1 = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickChangeSceen()
    {
        if (isCharacter1)
            PlayerPrefs.SetInt("Character", 1);
        else
            PlayerPrefs.SetInt("Character", 2);

        SceneManager.LoadScene("MainSceen");
    }    
}
