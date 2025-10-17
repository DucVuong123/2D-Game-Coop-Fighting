using UnityEngine;



public class MapController : MonoBehaviour
{
    [Header("InfoMap")]
    public int mapID;
    public TypeMap typeMap;
    public Transform Poss_Player;

    public enum TypeMap
    {
        Tutorial,
        Normalmap
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
}
