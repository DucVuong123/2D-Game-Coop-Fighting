using UnityEngine;

public class Intro_Menu : MonoBehaviour
{
    [Header("Object sẽ hiện sau X giây")]
    public GameObject UIMenu_intro;

    [Tooltip("Số GIÂY chờ trước khi hiện")]
    public float delaySeconds = 3f;   // ← 5 giây

    void Start()
    {
        if (UIMenu_intro == null) { Debug.LogWarning("Chưa gán UIMenu_intro!"); return; }
        UIMenu_intro.SetActive(false);
        StartCoroutine(ShowAfterDelayRealtime());
    }

    System.Collections.IEnumerator ShowAfterDelayRealtime()
    {
        yield return new WaitForSecondsRealtime(delaySeconds); // đếm theo thời gian thực
        UIMenu_intro.SetActive(true);
    }
}
