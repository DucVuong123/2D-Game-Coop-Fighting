using System.Collections.Generic;
using UnityEngine;

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
        if(currentMap!=null) Destroy(currentMap);
        currentMap = Instantiate(Maps[currentMapIndex = currentMapIndex < Maps.Count - 1 ? currentMapIndex++ : 0], Vector2.zero, Quaternion.identity);
        Main_Player.transform.position = currentMap.GetComponent<MapController>().Instance.Poss_Player.position;
    }    


    public void Get_Main_Player(PlayerMovementBase _Charactor)
    {
        switch (_Charactor)
        {
            case DuKichMovement:
                Main_Player = Chars["Du_Kich"];
                break;

            case BoBinhMovement:
                Main_Player = Chars["Bo_Binh"];
                break;
        }
    }
}
