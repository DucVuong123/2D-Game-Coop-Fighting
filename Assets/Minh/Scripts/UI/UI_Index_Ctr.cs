using TMPro;
using UnityEngine;
using UnityEngine.UI;




public enum Type_Index
{
   Name_Room,
   Id_Room,
   Name_Player,
   Id_Player,
   Level_player,
   state_player
}
public class UI_Index_Ctr : MonoBehaviour
{
    public Type_Index type_Index;
    private void Start()
    {
       switch(type_Index)
        {
            case Type_Index.Name_Room:
                GetComponent<Text>().text = GetComponent<Text>().text + " " + RoomController.Instance.Curent_Room.TenPhongChoi;
                break;
            case Type_Index.Id_Room:
                GetComponent<Text>().text = GetComponent<Text>().text + " "+  RoomController.Instance.Curent_Room.MaPhongChoi;
                break;
            case Type_Index.Name_Player:
                GetComponent<TextMeshProUGUI>().text = GameController.Instance.Main_Player_Acc.TenNguoiChoi;
                break;
            case Type_Index.Id_Player:
                GetComponent<TextMeshProUGUI>().text = GameController.Instance.Main_Player_Acc.MaNguoiChoi;
                break;
            case Type_Index.Level_player:
                GetComponent<TextMeshProUGUI>().text = GameController.Instance.Main_Player_Acc.CapDo.ToString();
                break;
            case Type_Index.state_player:
                GetComponent<TextMeshProUGUI>().text = GameController.Instance.Main_Player_Acc.TrangThai;
                break;

        }
    }
}
