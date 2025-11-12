using TMPro;
using UnityEngine;
using UnityEngine.UI;




public enum Type_Index
{
   Name_Room,
   Id_Room,
   Name_Player
}
public class UI_Index_Ctr : MonoBehaviour
{
    public Type_Index type_Index;
    private void Start()
    {
       switch(type_Index)
        {
            case Type_Index.Name_Room:
                Debug.Log(RoomController.Instance.Curent_Room.TenPhongChoi);
                GetComponent<Text>().text = GetComponent<Text>().text + " " + RoomController.Instance.Curent_Room.TenPhongChoi;
                break;
            case Type_Index.Id_Room:
                GetComponent<Text>().text = GetComponent<Text>().text + " "+  RoomController.Instance.Curent_Room.MaPhongChoi;
                break;
            case Type_Index.Name_Player:

                break;

        }
    }
}
