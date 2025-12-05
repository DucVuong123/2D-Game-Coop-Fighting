using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TongKet_EndMap : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text_Score;
    [SerializeField] private TextMeshProUGUI Text_MapUnlock;

    public void setData(string score, string mapUnlock)
    {
        Text_Score.text = score;
        Text_MapUnlock.text = mapUnlock;
    }

/*    public void backSecne() => SceneManager.LoadScene("");*/
}
