using UnityEngine.UI;
using UnityEngine;

public class UIAuthController : MonoBehaviour
{
    [Header("Register")]
    [SerializeField]private InputField user_Name_RS;
    [SerializeField]private InputField user_Email_RS;
    [SerializeField]private InputField user_Phone_RS;
    [SerializeField]private InputField user_Password_RS;
    [SerializeField] private Button button_Register;
    [SerializeField] private GameObject panel_Register;
    [SerializeField] private GameObject button_RegistertoLogin;
    [Header("LogIn")]
    [SerializeField]private InputField user_Name_LI;
    [SerializeField]private InputField user_Password_LI;
    [SerializeField] private Button button_login;
    [SerializeField] private Button button_logintoRegister;
    [SerializeField] private GameObject panel_Login;

    [Header("Amin")]
    [SerializeField] private Animator Amin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnRegisterButton()
    {
        AccountAuthManager.Instance.RegisterAccount(user_Name_RS.text, user_Email_RS.text, user_Phone_RS.text, user_Password_RS.text, (success, message) =>
        {
            if (success) Debug.Log("Đăng ký OK: " + message);
            else Debug.LogWarning("Đăng ký thất bại: " + message);
        });
    }
    public void OnLoginByUsernameButton()
    {
        AccountAuthManager.Instance.LoginWithUsername(user_Name_LI.text, user_Password_LI.text, (ok, msg, acc) =>
        {
            if (ok)
            {
                Debug.Log("Đăng nhập thành công! Xin chào " + acc.username);
                // Lưu accountId vào PlayerPrefs để giữ session
                PlayerPrefs.SetString("accountId", acc.accountId);
            }
            else
            {
                Debug.LogWarning("Đăng nhập thất bại: " + msg);
            }
        });
    }
    public void OnLoginToRegister()
    {
        Amin.SetBool("toRegis", true);
    }
     public void OnRegisterToLogin()
    {
        Amin.SetBool("toRegis",false);
    }
    
}
