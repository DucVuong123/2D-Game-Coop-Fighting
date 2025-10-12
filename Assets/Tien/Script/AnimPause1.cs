using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimPauseResume : MonoBehaviour
{
    public Animator animator;
    public int layerIndex = 0;
    public Button backButton;
    public Ui_Click uiClick;

    float pausedT = 0f;
    int pausedStateHash = 0;
    bool isPaused = false;
    bool resumeArmed = false;  // <-- chỉ khi true mới cho resume
    int pausedFrame = -1;

    void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();
        if (backButton) backButton.onClick.AddListener(ResumeFromPause);
    }

    // GỌI TỪ ANIMATION EVENT
    public void PauseAtEvent()
    {
        if (!animator) return;

        var info = animator.GetCurrentAnimatorStateInfo(layerIndex);
        pausedStateHash = info.fullPathHash;
        pausedT = Mathf.Repeat(info.normalizedTime, 1f);

        animator.speed = 0f;     
        isPaused = true;
        resumeArmed = false;   
        pausedFrame = Time.frameCount;

    
        StartCoroutine(ArmResumeNextFrame());

        Debug.Log($"[Pause] layer={layerIndex}, hash={pausedStateHash}, t={pausedT:0.000}");
    }

    IEnumerator ArmResumeNextFrame()
    {
    
        yield return null; 
        resumeArmed = true;
    }

    // KHI NHẤN NÚT QUAY LẠI
    public void ResumeFromPause()
    {
        if (!animator || !isPaused || !resumeArmed || Time.frameCount == pausedFrame)
        {
            return;
        }
        uiClick?.OnAnimationEnd();

        if (Time.timeScale == 0f) Time.timeScale = 1f; // nếu game đang pause toàn cục

        animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        animator.updateMode  = AnimatorUpdateMode.Normal;

        float t = Mathf.Clamp01(pausedT + 0.0001f); // đẩy lệch 1 epsilon
        StartCoroutine(ResumeNextFrame(pausedStateHash, layerIndex, t));

        isPaused = false;
        resumeArmed = false;

        Debug.Log($"[Resume] layer={layerIndex}, hash={pausedStateHash}, t={t:0.000}");
    }

    IEnumerator ResumeNextFrame(int stateHash, int layer, float t)
    {
        animator.speed = 1f;
        yield return null;                       // đợi 1 frame
        animator.Play(stateHash, layer, t);      // trở lại đúng state & thời điểm
        animator.Update(0f);                     // sample ngay
    }
}
