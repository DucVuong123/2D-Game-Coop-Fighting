using UnityEngine;
using UnityEngine.UI;

public class Ui_Click : MonoBehaviour
{
    [Header("UI")]
    public Button selectModeButton;

    [Header("Animation")]
    public Animator animator;                 
    public string idleStateName = "Idle";        // state Idle (default)
    public string playStateName = "SelectMode_Amin"; // state chứa clip

    bool isPlaying = false;

    void Awake()
    {
        if (!selectModeButton) selectModeButton = GetComponent<Button>();
        if (selectModeButton) selectModeButton.onClick.AddListener(OnClickPlay);

        if (!animator) animator = GetComponent<Animator>();
        if (!animator)
        {
            Debug.LogError("[Ui_Click] Chưa gán Animator!");
            return;
        }

        // Bảo đảm bắt đầu ở Idle (đang dùng Play thẳng state nên không có auto-play)
        animator.speed = 1f;
        animator.Play(idleStateName, 0, 0f);
        animator.Update(0f);
        animator.enabled = false;
    }

    public void OnClickPlay()
    {
        animator.enabled = true;
        if (!animator) return;
        if (isPlaying) return;                       // chặn spam
        isPlaying = true;

        if (selectModeButton) selectModeButton.interactable = false;

        animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        animator.updateMode  = AnimatorUpdateMode.Normal; 
        animator.speed = 1f;
        animator.Play(playStateName, 0, 0f);
        animator.Update(0f); 
    }

    public void OnAnimationEnd()
    {
        if (!animator) return;

        // Đưa về Idle & mở lại nút
        animator.speed = 1f;
        animator.Play(idleStateName, 0, 0f);
        animator.Update(0f);

        isPlaying = false;
        if (selectModeButton) selectModeButton.interactable = true;
    }
}
