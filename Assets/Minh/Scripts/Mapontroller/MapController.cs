using UnityEngine;



public class MapController : MonoBehaviour
{
    [Header("InfoMap")]
    public int mapID;
    public TypeMap typeMap;
    public Transform Poss_Player;
    public bool Unlock;
    [SerializeField] private GameObject Map_Unlock_next;

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
