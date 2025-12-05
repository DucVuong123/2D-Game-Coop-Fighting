using UnityEngine;



public class MapController : MonoBehaviour
{
    [Header("InfoMap")]
    public int mapID;
    public TypeMap typeMap;
    public Transform Poss_Player;
    public bool Unlock;
    [SerializeField] private GameObject Map_Unlock_next;


    [Header("Score")]
    public int score_Map;


    [Header("PvP")]
    public Transform Poss_Player_PvP1;
    public Transform Poss_Player_PvP2;

    public enum TypeMap
    {
        Tutorial,
        Normalmap,
        PvPMap
    }


    public MapController Instance;

    private void Awake()
    {
        Instance = this;
        score_Map = 0;
    }
    private void OnDestroy()
    {
        Instance = null;
    }


    public void Increast_Point(int _point) => score_Map += _point;

    public string Unlock_NextMap()
    {
        Map_Unlock_next.GetComponent<MapController>().Unlock = true;
        return Map_Unlock_next.name;
    }    
}
