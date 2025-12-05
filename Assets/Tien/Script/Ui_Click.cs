using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ui_Click : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button selectModeButton;
    [SerializeField] private Button SelectInforModeCoop;
    [SerializeField] private Button SelectInforModeDK;
    [SerializeField] private Button SelectModeCoop;
    [SerializeField] private Button SelectModeDk;
    [SerializeField] private Button inforMenu;
    [SerializeField] private Button PlayGame;
    [SerializeField] private Button Players_Admin;


    [SerializeField] private Button SelectSetting;
    [SerializeField] private Button SaveSetting;

    [SerializeField] private TextMeshProUGUI tmpText;


    [Header("Đối tượng")]
    [SerializeField] private GameObject TextInforModeCoop;
    [SerializeField] private GameObject TextInforModeDK;
    [SerializeField] private GameObject TextSelectModeCoop;
    [SerializeField] private GameObject TextSelectModeDK;

    [Header("Menu")]
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject UIProfile;
    [SerializeField] private GameObject ModeMenu;
    [SerializeField] private GameObject SettingMenu;
    [SerializeField] private GameObject GameNameAndMode;
    
    [Header("Animation")]
    [SerializeField] private Animator animator;
    public string idleStateName = "Idle";
    public string playStateName = "SelectMode_Amin";

    private bool isPlaying = false;
    private int Mode = 0; // 0 = Coop, 1 = Đối kháng

    // Cache sprite (đặt file trong Assets/Resources/IMG/)
    private Sprite coopSprite;
    private Sprite dkSprite;


    private void Start()
    {
        if (GameController.Instance.isAdmin())
        {
            Players_Admin.gameObject.SetActive(true);
        }
        else
        {
            Players_Admin.gameObject.SetActive(false);
        } 
            
            
    }
    void Awake()
    {

        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
        if (!animator)
        {
            Debug.LogError("[Ui_Click] Chưa gán Animator!");
            enabled = false;
            return;
        }


        // Gán listener (chỉ khi đã gán từ Inspector)
        if (selectModeButton)  selectModeButton.onClick.AddListener(OnClickPlay);
        if (SelectInforModeDK)  SelectInforModeDK.onClick.AddListener(OnclickInforDK);
        if (SelectInforModeCoop) SelectInforModeCoop.onClick.AddListener(OnclickInforCoop);
        if (SelectModeCoop)     SelectModeCoop.onClick.AddListener(OnclickSelectCoop);
        if (SelectModeDk) SelectModeDk.onClick.AddListener(OnclickSelectDK);
        if (SelectSetting) SelectSetting.onClick.AddListener(OnSetting);
        if (SaveSetting) SaveSetting.onClick.AddListener(SaveSettingMenu);
        if (inforMenu) inforMenu.onClick.AddListener(inforCaptant);
        if (PlayGame) PlayGame.onClick.AddListener(PlayonGame);

        // Trạng thái ban đầu theo Mode
        ApplyModeUI(Mode);

        // Bắt đầu ở Idle, tắt auto-play
        animator.speed = 1f;
        animator.Play(idleStateName, 0, 0f);
        animator.Update(0f);
        animator.enabled = false;
    }

    private void ApplyModeUI(int mode)
    {
        bool isCoop = (mode == 0);

        if (TextSelectModeCoop) TextSelectModeCoop.SetActive(isCoop);
        if (TextSelectModeDK)   TextSelectModeDK.SetActive(!isCoop);
    }

    public void OnClickPlay()
    {
        if (!animator || isPlaying) return;

        isPlaying = true;
        if (selectModeButton) selectModeButton.interactable = false;

        animator.enabled = true;
        animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        animator.updateMode  = AnimatorUpdateMode.Normal;
        animator.speed = 1f;
        animator.Play(playStateName, 0, 0f);
        animator.Update(0f);
    }

    // Gọi hàm này bằng Animation Event ở cuối clip SelectMode_Amin
    public void OnAnimationEnd()
    {
        TextInforModeCoop.SetActive(false);
        if (!animator) return;
        animator.speed = 1f;
        animator.Play(idleStateName, 0, 0f);
        animator.Update(0f);
        isPlaying = false;
            if (selectModeButton) selectModeButton.interactable = true;
    }

    public void OnclickInforCoop()
    {
        if (!TextInforModeCoop.activeSelf)
        {
            TextInforModeCoop.SetActive(true);
        }
    }

    public void OnclickInforDK()
    {
        if (!TextInforModeDK) return;

        TextInforModeDK.SetActive(!TextInforModeDK.activeSelf);
        if (TextInforModeCoop && TextInforModeDK.activeSelf)
            TextInforModeCoop.SetActive(false);
    }

    public void OnclickSelectCoop()
    {
        Mode = 0;
        ApplyModeUI(Mode);
        OnAnimationEnd();
        TextInforModeCoop.SetActive(false);
        tmpText.text = "Chế độ Co-op";
        GameController.Instance.mode = mode_Select.Co_op;
    }

    public void OnclickSelectDK()
    {
        Debug.Log("Ngáo");
        Mode = 1;
        ApplyModeUI(Mode);
        OnAnimationEnd();
        TextInforModeDK.SetActive(false);
        tmpText.text = "Chế độ Đối kháng";
        GameController.Instance.mode = mode_Select.PvP;
    }

    public void OnSetting()
    {
        ModeMenu.SetActive(false);
        UIProfile.SetActive(false);
        MainMenu.SetActive(false);
        GameNameAndMode.SetActive(false);
        SettingMenu.SetActive(true);
    }
    public void SaveSettingMenu()
    {
        ModeMenu.SetActive(true);
        UIProfile.SetActive(true);
        MainMenu.SetActive(true);
        GameNameAndMode.SetActive(true);
        SettingMenu.SetActive(false);
        OnAnimationEnd();
    }
    public void inforCaptant()
    {
        SceneManager.LoadScene(3);
    }
    public void PlayonGame()
    {
        SceneManager.LoadScene(4);
    }
    public void Information_Player()
    {
        SceneManager.LoadScene("Information_Scene");
    }
}
