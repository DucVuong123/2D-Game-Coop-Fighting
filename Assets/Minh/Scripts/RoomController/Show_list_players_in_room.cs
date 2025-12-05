using UnityEngine;
using PurrNet;
using System;
public class Show_list_players_in_room : NetworkBehaviour
{
    public Transform content;           // Content của ScrollView
    public GameObject player_InRoom_ItemPrefab; // Prefab item
    protected override void OnSpawned()
    {
        base.OnSpawned();
        RefreshList();

    }
    private void Start()
    {
       // RefreshList();
    }
    public void RefreshList()
    {
        // Xóa item cũ
        //foreach (Transform child in content)
        //{
        //    Destroy(child.gameObject);
        //}
        //Debug.Log("Refreshing player list...");
        // Lấy danh sách từ Firebase


        /*  GameDataModels.NguoiChoi players = new GameDataModels.NguoiChoi();
          DatabaseCtr.Instance.GetDataByField<GameDataModels.NguoiChoi>(
      "NguoiChoi_Account",
      "MaNguoiChoi",
      RoomController.Instance.curent_list_players_room_PvP.MaNguoiChoi,
      (ok, msg, player) =>
      {
          if (ok)
          {
              GameObject item = Instantiate(player_InRoom_ItemPrefab, content);
              if (GameController.Instance.mode is mode_Select.Co_op) item.GetComponent<Inddex_Players_In_Room>().SetData(RoomController.Instance.curent_list_players_room.MaNguoiChoi);
              else item.GetComponent<Inddex_Players_In_Room>().SetData(RoomController.Instance.curent_list_players_room_PvP.MaNguoiChoi);
          }
          else
          {
              Debug.Log("Enrrol");
              return;
          }
      }
  );*/






        GameObject item = Instantiate(player_InRoom_ItemPrefab, content);
        if (GameController.Instance.mode is mode_Select.Co_op) item.GetComponent<Inddex_Players_In_Room>().SetData(GameController.Instance.Main_Player_Acc.TenNguoiChoi, "");
        else item.GetComponent<Inddex_Players_In_Room>().SetData(GameController.Instance.Main_Player_Acc.TenNguoiChoi, "");

    }
}
