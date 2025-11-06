using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(EventTrigger))]
public class UI_GameCtr : MonoBehaviour
{
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
        Debug.Log("111111111111111111");
        GameController.Instance.In_Game();
    }
}
