using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Map")]
    public List<GameObject> Maps;
    public GameObject currentMap;
    private int currentMapIndex = -1;


    [Header("Charactor")]
    [SerializeField] private List<GameObject> charsList;
    private Dictionary<string, GameObject> Chars;

    [Header("MainPlayers")]
    public GameObject Player_Choice_Char;
    public GameObject Main_Player;


    [Header("Account_Player")]
    public GameDataModels.NguoiChoi Main_Player_Acc;

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

    public void nextMap()
    {
        if(currentMapIndex >= Maps.Count)
        {
            SceneManager.LoadScene(3);
            return;
        }
        if (currentMap != null) Destroy(currentMap);
        currentMap = Instantiate(Maps[currentMapIndex = currentMapIndex < Maps.Count - 1 ? currentMapIndex + 1 : 0], Vector2.zero, Quaternion.identity);
        if (Main_Player == null) { Main_Player = Instantiate(Player_Choice_Char, currentMap.GetComponent<MapController>().Instance.Poss_Player.position, Quaternion.identity); return; };
        Main_Player.transform.position = currentMap.GetComponent<MapController>().Instance.Poss_Player.position;
        
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
        SceneManager.sceneLoaded += OnSceneLoaded_InGame;
        SceneManager.LoadScene(5);
    }

    private void OnSceneLoaded_InGame(Scene scene, LoadSceneMode mode)
    {
        // Hủy đăng ký event để không bị gọi lại nhiều lần
        SceneManager.sceneLoaded -= OnSceneLoaded_InGame;
        if (Main_Player == null) Main_Player = Instantiate(Player_Choice_Char, Vector2.zero, Quaternion.identity);
        nextMap();


    }



    public bool check_Account_Players_AfterLogin(Account acc)
    {

        Main_Player_Acc = new GameDataModels.NguoiChoi();
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
             /*   DatabaseCtr.Instance.GenerateUniquePlayerId(uniqueId =>
                {
                    GameDataModels.NguoiChoi newPlayer = new GameDataModels.NguoiChoi
                    {
                        MaNguoiChoi = uniqueId,
                        TenNguoiChoi = acc.username,
                        CapDo = 1,
                        TrangThai = "Online",
                        MaTranDau = "",
                        MaTaiKhoan = acc.accountId
                    };

                    DatabaseCtr.Instance.AddData($"NguoiChoi/{acc.accountId}", newPlayer, (addSuccess, addMessage) =>
                    {
                        if (addSuccess)
                            Debug.Log("Người chơi mới đã được tạo!");
                        else
                            Debug.LogError("Tạo người chơi mới thất bại: " + addMessage);
                    });
                });*/
            }
        });


        return Main_Player_Acc != null;
    }

}
