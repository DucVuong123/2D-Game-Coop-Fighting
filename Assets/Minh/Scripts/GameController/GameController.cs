using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using PurrNet;
public class GameController : NetworkBehaviour
{
    [Header("Map")]
    public GameObject Main_Map;
    public GameObject currentMap;

    [Header("Charactor")]
    [SerializeField] private List<GameObject> charsList;
    private Dictionary<string, GameObject> Chars;

    [Header("MainPlayers")]
    public GameObject Player_Choice_Char;
    public GameObject Main_Player;


    [Header("Player_Acc")]
    public GameDataModels.NguoiChoi Main_Player_Acc;

    [Header("Account_Pl")]
    public GameDataModels.TaiKhoan Account_Player_AffterLogin;

    public static GameController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }



        Chars = new Dictionary<string, GameObject>();
        foreach (GameObject entry in charsList)
        {
            Chars[entry.name] = entry;
        }
    }

    private void Start()
    {
       // Invoke(nameof(add_Players), 2f);

    }
    void add_Players()
    {
        GameDataModels.NguoiChoi newPlayer = new GameDataModels.NguoiChoi
        {
            MaNguoiChoi = "uniqueId",
            TenNguoiChoi = "minh",
            CapDo = 0,
            TrangThai = "Online",
            MaTranDau = "",
            MaTaiKhoan = "123"
        };

        DatabaseCtr.Instance.AddData("NguoiChoi_Account", newPlayer, (addSuccess, addMessage) =>
        {
            if (addSuccess)
                Debug.Log("Người chơi mới đã được tạo!");
            else
                Debug.LogError("Tạo người chơi mới thất bại: " + addMessage);
        });
    }    

    public void nextMap(string _type)
    {

        //if(Main_Map!= null)
        //{
        //    Debug.Log(_type);
            if (_type.Contains("Map"))
            {
                if (currentMap != null) Destroy(currentMap);
                currentMap = Instantiate(Main_Map, Vector2.zero, Quaternion.identity);
            }
            else
            {
             if(isServer)
                {
                    if (Main_Player == null) { Main_Player = Instantiate(Player_Choice_Char, currentMap.GetComponent<MapController>().Instance.Poss_Player.position, Quaternion.identity); return; };
                    Main_Player.transform.position = currentMap.GetComponent<MapController>().Instance.Poss_Player.position;
                }    
              else
                {
                 if (Main_Player == null) { Main_Player = Instantiate(Player_Choice_Char, FindObjectOfType<MapController>().Instance.Poss_Player.position, Quaternion.identity); return; };
                Main_Player.transform.position = FindObjectOfType<MapController>().Instance.Poss_Player.position;
                }    
            } 
            
        //}

        
    }


    public void Get_Main_Player(PlayerMovementBase _Charactor)
    {
        switch (_Charactor)
        {
            case DuKichMovement:
                Player_Choice_Char = Chars["Du_Kich"];
                break;

            case BoBinhMovement:
                Player_Choice_Char = Chars["Bo_Binh"];
                break;
        }
    }


    public void In_Game()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded_InGame;
        SceneManager.LoadScene("Scene_MapEdit");
    }

/*    private void OnSceneLoaded_InGame(Scene scene, LoadSceneMode mode)
    {
        // Hủy đăng ký event để không bị gọi lại nhiều lần
        SceneManager.sceneLoaded -= OnSceneLoaded_InGame;
        if (Main_Player == null) Main_Player = Instantiate(Player_Choice_Char, Vector2.zero, Quaternion.identity);
        nextMap();


    }*/

    public void spawm_Player_Map(string _type)
    {

      //  if (Main_Player == null) Main_Player = Instantiate(Player_Choice_Char, Vector2.zero, Quaternion.identity);
        nextMap(_type);
    }    


    public bool check_Account_Players_AfterLogin(Account acc)
    {

        /*Main_Player_Acc = new GameDataModels.NguoiChoi();
        // Lấy dữ liệu người chơi từ Firebase
        DatabaseCtr.Instance.GetData<GameDataModels.NguoiChoi>($"NguoiChoi/{acc.accountId}", (success, data, message) =>
        {
            if (success && data != null)
            {
                // Người chơi đã tồn tại
                Main_Player_Acc = data;
                Debug.Log($"Người chơi đã tồn tại: {data.TenNguoiChoi}");
               
            }
            else
            {
                Debug.Log("Người chơi là người chơi mới");

            }
        });
*/

        Main_Player_Acc = new GameDataModels.NguoiChoi();
        DatabaseCtr.Instance.GetDataByField<GameDataModels.NguoiChoi>(
    "NguoiChoi_Account",
    "MaTaiKhoan",
    acc.accountId,
    (ok, msg, player) =>
    {
        if (ok)
        {
            Main_Player_Acc = player;
            Debug.Log($"Người chơi đã tồn tại: {player.TenNguoiChoi}");
        }
        else
        {
            Debug.Log("Người chơi là người chơi mới");
        }
    }
);


        return Main_Player_Acc.MaNguoiChoi != null;
    }

}
