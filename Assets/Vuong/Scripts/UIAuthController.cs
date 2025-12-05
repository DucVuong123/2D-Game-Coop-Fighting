using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [Header("Player_Name")]
    [SerializeField] private InputField Player_N;
    [SerializeField] private GameObject Panel_Confirm_Username;


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
                GameController.Instance.Account_Player_AffterLogin = new GameDataModels.TaiKhoan() {
                    MaTaiKhoan = acc.accountId,
                    TenDangNhap = acc.username,
                    Email = acc.email,
                    SoDienThoai = acc.phone,
                    MatKhau = acc.passwordHash
                };

                GameController.Instance.check_Account_Players_AfterLogin(acc, (exists) =>
                {
                    if (exists)
                    {
                        PlayerPrefs.SetString("accountId", acc.accountId);
                        SceneManager.LoadScene("MenuGame");
                    }
                    else
                    {
                        Panel_Confirm_Username.SetActive(true);
                        Debug.Log("Log");
                    }
                });

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

    public void OnConfirmNamePlayer()
    {
        DatabaseCtr.Instance.GenerateUniquePlayerId(uniqueId =>
        {
            GameDataModels.NguoiChoi newPlayer = new GameDataModels.NguoiChoi
            {
                MaNguoiChoi = uniqueId,
                TenNguoiChoi = Player_N.text.ToString(),
                CapDo = 0,
                TrangThai = "Online",
                MaTranDau = "",
                MaTaiKhoan = GameController.Instance.Account_Player_AffterLogin.MaTaiKhoan 
            };

            DatabaseCtr.Instance.AddData("NguoiChoi_Account", newPlayer, (addSuccess, addMessage) =>
            {
                if (addSuccess)
                    Debug.Log("Người chơi mới đã được tạo!");
                else
                    Debug.LogError("Tạo người chơi mới thất bại: " + addMessage);
            });
        });
        PlayerPrefs.SetString("accountId", GameController.Instance.Account_Player_AffterLogin.MaTaiKhoan);
        SceneManager.LoadScene("MenuGame");
    }
    
}
