using GameDataModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class List_All_Room_Players
{
    public string maPhongChoi;
    public  List<string> list_MaNguoiChoi = new List<string>();
}



public class RoomController : MonoBehaviour
{

    public GameDataModels.PhongChoi Curent_Room;

    public static RoomController Instance;

    public List<List_All_Room_Players> All_Room_Pl;
    public List_All_Room_Players curent_list_players_room;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        All_Room_Pl = new List<List_All_Room_Players>();
        LoadAllRoomsAndPlayers();
    }

    public void Creat_Room(Text Name_Room)
    {
        if (Name_Room == null)
        {
            Debug.LogError("⚠️ Thiếu Text Name_Room trong Inspector!");
            return;
        }

        if (string.IsNullOrWhiteSpace(Name_Room.text))
        {
            Debug.LogError("⚠️ Vui lòng nhập tên phòng trước khi tạo!");
            return;
        }


        // Lấy mã người chơi hiện tại
        string maNguoiChoi =  GameController.Instance.Main_Player_Acc.MaNguoiChoi;
        if (string.IsNullOrEmpty(maNguoiChoi))
        {
            Debug.LogError("⚠️ Không tìm thấy mã người chơi!");
            return;
        }

        // Tự sinh ID phòng 
        string roomId = GenerateRoomID(6);

        GameDataModels.PhongChoi room = new GameDataModels.PhongChoi()
        {

            MaPhongChoi = roomId,
            TenPhongChoi = Name_Room.text,  

        };

        DatabaseCtr.Instance.AddData<GameDataModels.PhongChoi>("PhongChoi", room, (success, message) =>
        {
            if (success)
            {
                Debug.Log($"✅ Tạo phòng '{room.TenPhongChoi}' thành công! ID: {room.MaPhongChoi}");
                Curent_Room = room;
                StartCoroutine(AddPlayerToRoom(room.MaPhongChoi, maNguoiChoi));
                /*                // Hiển thị ID ra UI
                                if (Id_Room != null)
                                    Id_Room.text = room.MaPhongChoi;*/


            }
            else
            {
                Curent_Room = null;
                Debug.LogError($"❌ Lỗi khi tạo phòng: {message}");
            }
        });


 }

    public void Join_Room(Text Id_Room)
    {
        if (Id_Room == null || string.IsNullOrWhiteSpace(Id_Room.text))
        {
            Debug.LogError("⚠️ Vui lòng nhập ID phòng để tham gia!");
            return;
        }

        // Lấy ID người chơi nhập
        string inputRoomId = Id_Room.text;

        // Loại bỏ # nếu người chơi nhập
        string roomIdKey = inputRoomId.StartsWith("#") ? inputRoomId.Substring(1) : inputRoomId;

        string maNguoiChoi = GameController.Instance.Main_Player_Acc.MaNguoiChoi;
        if (string.IsNullOrEmpty(maNguoiChoi))
        {
            Debug.LogError("⚠️ Không tìm thấy mã người chơi!");
            return;
        }

        // Lấy dữ liệu phòng từ Firebase
        DatabaseCtr.Instance.GetDataByField<GameDataModels.PhongChoi>(
"PhongChoi",
"MaPhongChoi",
roomIdKey.ToString(),
(ok, msg, room) =>
{
    if (ok)
    {
        Debug.Log("phòng chơi tồn tại");
        Curent_Room = room;
        StartCoroutine(AddPlayerToRoom(room.MaPhongChoi, maNguoiChoi));
    }
    else
    {
        Debug.Log("phòng chơi không tồn tại");
        Curent_Room = null;
    }
});



    }
 

    private string GenerateRoomID(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < length; i++)
        {
            sb.Append(chars[Random.Range(0, chars.Length)]);
        }
        return sb.ToString();
    }


    private IEnumerator AddPlayerToRoom(string maPhongChoi, string maNguoiChoi)
    {
        DatabaseCtr.Instance.CheckPlayerInRoom(maPhongChoi, maNguoiChoi, (exist, msg) =>
        {
            if (exist)
            {
                Debug.Log("⚠ Người chơi đã có trong phòng → bỏ qua");
               // SceneManager.LoadScene(7);
                return;
            }

            // Nếu chưa tồn tại → thêm mới với PUSH KEY
            GameDataModels.PhongChoi_NguoiChoi data = new GameDataModels.PhongChoi_NguoiChoi()
            {
                MaPhongChoi = maPhongChoi,
                MaNguoiChoi = maNguoiChoi
            };

            DatabaseCtr.Instance.AddData("PhongChoi_NguoiChoi", data, (ok, msg2) =>
            {
                if (ok)
                {
                    Debug.Log("👤 Thêm người chơi vào phòng thành công!");
                    SceneManager.LoadScene(7);
                }
                else
                {
                    Debug.LogError("❌ Lỗi khi thêm người chơi: " + msg2);
                }
            });
        });
        All_Room_Pl = new List<List_All_Room_Players>();
        LoadAllRoomsAndPlayers();
        yield return new WaitForSeconds(0.2f);
        foreach(List_All_Room_Players room_pl in All_Room_Pl)
        {
            if(room_pl.maPhongChoi == maPhongChoi)
            {
                curent_list_players_room = room_pl;
                break;
            }    
        }    
        SceneManager.LoadScene(1);
    }


    private void LoadAllRoomsAndPlayers()
    {
        All_Room_Pl = new List<List_All_Room_Players>();

        DatabaseCtr.Instance.GetAllData<GameDataModels.PhongChoi>("PhongChoi", (ok, msg, listRooms) =>
        {
            if (!ok || listRooms == null)
            {
                Debug.LogError("❌ Lỗi khi lấy danh sách phòng: " + msg);
                return;
            }

            Debug.Log($"📌 Có {listRooms.Count} phòng chơi trong hệ thống.");

            foreach (var room in listRooms)
            {
                LoadPlayersOfRoom(room);
            }
        });
    }


    private void LoadPlayersOfRoom(GameDataModels.PhongChoi room)
    {
        DatabaseCtr.Instance.GetDataListByField<GameDataModels.PhongChoi_NguoiChoi>(
            "PhongChoi_NguoiChoi",
            "MaPhongChoi",
            room.MaPhongChoi,
            (ok, msg, listPlayers) =>
            {
                if (!ok)
                {
                    Debug.LogError($"❌ Lỗi khi lấy người chơi của phòng {room.MaPhongChoi}: {msg}");
                    return;
                }

                List_All_Room_Players newRoom = new List_All_Room_Players();
                newRoom.maPhongChoi = room.MaPhongChoi;

                foreach (var p in listPlayers)
                {
                    newRoom.list_MaNguoiChoi.Add(p.MaNguoiChoi);
                }

                All_Room_Pl.Add(newRoom);

                Debug.Log($"📌 Phòng {room.MaPhongChoi} có {newRoom.list_MaNguoiChoi.Count} người chơi.");
            }
        );
    }


}
