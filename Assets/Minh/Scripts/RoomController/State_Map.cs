using UnityEngine;
using UnityEngine.TextCore.Text;

public class State_Map : MonoBehaviour
{
    [SerializeField] private GameObject Map;
    [SerializeField] private GameObject[] Others_Map_Img;
    [SerializeField] private GameObject Lock_Img;
    [SerializeField] private GameObject Select_map_img;



    public State_Map Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    private void Start()
    {
        if (Map.GetComponent<MapController>().Unlock)
        {
            Lock_Img.SetActive(false);
        }
            
    }
    public void click_Select()
    {
        if(Map.GetComponent<MapController>().Unlock)
        {
            Select_map_img.SetActive(true);
            RoomController.Instance.selcet_Map(Map);
            transform.GetChild(0).gameObject.SetActive(true);
            foreach (GameObject img in Others_Map_Img)
            {
                if(img.GetComponent<State_Map>().Instance.GetMap().GetComponent<MapController>().Unlock) img.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        else
        {

        } 
            
    }

    public GameObject GetMap() { return Map; }
}
