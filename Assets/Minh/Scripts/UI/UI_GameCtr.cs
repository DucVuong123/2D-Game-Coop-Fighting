using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;





public enum type_Ctr
{
   GameCtr_INGAME,
   RoomCtr_CreateRoom,
    RoomCtr_JoinRoom,
    RoomCtr_SelectCharacter

}



[RequireComponent(typeof(EventTrigger))]
public class UI_GameCtr : MonoBehaviour
{
    [SerializeField] private type_Ctr type_UI;

    [Header("Index_RoomCtr_NameRoom_IdRoom")]
    [SerializeField] private Text Name_Room;
    [SerializeField] private Text Id_Room;

    [Header("Charactor_Select")]
    [SerializeField] private GameObject Charactor;
    [SerializeField] private Image[] Img_Button_Color;


    EventTrigger events;
    private void Start()
    {
        events = GetComponent<EventTrigger>();
        EventTrigger.Entry clickEvents = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerClick
        };
        clickEvents.callback.AddListener(OnClickButton);
        events.triggers.Add(clickEvents);
    }

    public void OnClickButton(BaseEventData evenData)
    {
        switch (type_UI)
        {
            case type_Ctr.GameCtr_INGAME:
                GameController.Instance.In_Game();
                break;
            case type_Ctr.RoomCtr_CreateRoom:
                RoomController.Instance.Creat_Room(Name_Room); 
                break;

            case type_Ctr.RoomCtr_JoinRoom: 
                RoomController.Instance.Join_Room(Id_Room); 
                break;
            case type_Ctr.RoomCtr_SelectCharacter:
                RoomController.Instance.selcet_Charactor(Charactor);
                GetComponent<Image>().color = Color.red;
                foreach (Image img in Img_Button_Color)
                {
                        img.color = Color.white;
                }
                break;
        }
    }
}
