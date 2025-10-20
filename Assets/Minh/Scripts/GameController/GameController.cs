using System.Collections.Generic;
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
    public GameObject Player_Choice;
    public GameObject Main_Player;


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

        if (currentMap != null) Destroy(currentMap);
        currentMap = Instantiate(Maps[currentMapIndex = currentMapIndex < Maps.Count - 1 ? currentMapIndex + 1 : 0], Vector2.zero, Quaternion.identity);
        if (Main_Player == null) { Main_Player = Instantiate(Player_Choice, currentMap.GetComponent<MapController>().Instance.Poss_Player.position, Quaternion.identity); return; };
        Main_Player.transform.position = currentMap.GetComponent<MapController>().Instance.Poss_Player.position;
    }


    public void Get_Main_Player(PlayerMovementBase _Charactor)
    {
        switch (_Charactor)
        {
            case DuKichMovement:
                Player_Choice = Chars["Du_Kich"];
                break;

            case BoBinhMovement:
                Player_Choice = Chars["Bo_Binh"];
                break;
        }
    }


    public void In_Game()
    {
        SceneManager.sceneLoaded += OnSceneLoaded_InGame;
        SceneManager.LoadScene(1);
    }

    private void OnSceneLoaded_InGame(Scene scene, LoadSceneMode mode)
    {
        // Hủy đăng ký event để không bị gọi lại nhiều lần
        SceneManager.sceneLoaded -= OnSceneLoaded_InGame;
        if (Main_Player == null) Main_Player = Instantiate(Player_Choice,Vector2.zero, Quaternion.identity);
        nextMap();


    }
}
