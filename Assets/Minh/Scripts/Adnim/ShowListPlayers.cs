using TMPro;
using UnityEngine;



public class ShowListPlayers : MonoBehaviour
{
    public Transform content;           // Content của ScrollView
    public GameObject playerItemPrefab; // Prefab item
    public GameObject panel_list_players;

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
        DatabaseCtr.Instance.GetPlayersList((success, list, msg) =>
        {
            if (!success)
            {
                Debug.LogError(msg);
                return;
            }

            foreach (GameDataModels.NguoiChoi player in list)
            {
                GameObject item = Instantiate(playerItemPrefab, content);
                item.GetComponent<Players_Index_List>().SetData(player);
            }
        });
    }

}
