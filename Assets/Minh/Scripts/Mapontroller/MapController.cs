using UnityEngine;
using PurrNet;


public class MapController : NetworkBehaviour
{
    [Header("InfoMap")]
    public int mapID;
    public TypeMap typeMap;
    public Transform Poss_Player;
    public bool Unlock;
    [SerializeField] private GameObject Map_Unlock_next;


    [Header("Score")]
    public SyncVar<int> score_Map = new(0);


    [Header("PvP")]
    public Transform Poss_Player_PvP1;
    public Transform Poss_Player_PvP2;

    public enum TypeMap
    {
        Tutorial,
        Normalmap,
        PvPMap
    }

    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        if (asServer)
            return;
        Resert_Point();

    }
    public MapController Instance;

    private void Awake()
    {
        Instance = this;

    }
    private void OnDestroy()
    {
        Instance = null;
    }

    [ServerRpc]
    public void Increast_Point(int _point) => score_Map.value += _point;
    [ServerRpc]
    public void Resert_Point() => score_Map.value =0;

    public string Unlock_NextMap()
    {
       // Map_Unlock_next.GetComponent<MapController>().Unlock = true;
        return Map_Unlock_next.name;
    }    
}
