using UnityEngine;

public class Show_list_players_in_room : MonoBehaviour
{
    public Transform content;           // Content của ScrollView
    public GameObject player_InRoom_ItemPrefab; // Prefab item

    private void Start()
    {
        RefreshList();
    }
    public void RefreshList()
    {
        // Xóa item cũ
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("Refreshing player list...");
        // Lấy danh sách từ Firebase
      for(int i=0;i<RoomController.Instance.curent_list_players_room.list_MaNguoiChoi.Count; i++)
        {

           /* GameDataModels.NguoiChoi players = new GameDataModels.NguoiChoi();
            DatabaseCtr.Instance.GetDataByField<GameDataModels.NguoiChoi>(
        "NguoiChoi_Account",
        "MaNguoiChoi",
        RoomController.Instance.curent_list_players_room.list_MaNguoiChoi[i],
        (ok, msg, player) =>
        {
            if (ok)
            {

            }
            else
            {

                return;
            }
        }
    );*/






            GameObject item = Instantiate(player_InRoom_ItemPrefab, content);
            item.GetComponent<Inddex_Players_In_Room>().SetData( RoomController.Instance.curent_list_players_room.list_MaNguoiChoi[i]);
        }
    }
}
