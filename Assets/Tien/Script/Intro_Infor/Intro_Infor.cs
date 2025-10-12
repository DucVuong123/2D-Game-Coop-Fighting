using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Intro_Infor : MonoBehaviour
{
    
    [SerializeField] private float delay = 2f;    
    [SerializeField] private bool useRealtime = false; 

    public void OnIntroAnimationEnd()
    {
        StartCoroutine(LoadAfterDelay());
    }

    private IEnumerator LoadAfterDelay()
    {
        if (useRealtime) yield return new WaitForSecondsRealtime(delay);
        else             yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(2);
    }
}
