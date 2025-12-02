using UnityEngine;
using UnityEngine.TextCore.Text;

public class State_Map : MonoBehaviour
{
    [SerializeField] private GameObject Map;
    [SerializeField] private GameObject[] Others_Map_Img;



    public State_Map Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    public void click_Select()
    {
        if(Map.GetComponent<MapController>().Unlock)
        {
            RoomController.Instance.selcet_Map(Map);
            transform.GetChild(0).gameObject.SetActive(true);
            foreach (GameObject img in Others_Map_Img)
            {
                if(img.GetComponent<State_Map>().Instance.GetMap().GetComponent<MapController>().Unlock) img.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {

        } 
            
    }

    public GameObject GetMap() { return Map; }
}
