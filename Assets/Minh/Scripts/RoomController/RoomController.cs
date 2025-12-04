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

[System.Serializable]
public class Players_Infor_Room
{

    public string maPhongChoi;
    public string MaNguoiChoi;

}


public class RoomController : MonoBehaviour
{
    [Header("Coop")]
    public GameDataModels.PhongChoi Curent_Room;
    public List<List_All_Room_Players> All_Room_Pl;
    public Players_Infor_Room curent_list_players_room;


    [Header("PvP")]
    public GameDataModels.PhongChoi Curent_Room_PvP;
    public List<List_All_Room_Players> All_Room_Pl_PvP;
    public Players_Infor_Room curent_list_players_room_PvP;

    public static RoomController Instance;

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
        All_Room_Pl_PvP = new List<List_All_Room_Players>();
        LoadAllRoomsAndPlayers_PvP();
    }



    #region Room_Coop
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
                }
                else
                {
                    Debug.LogError("❌ Lỗi khi thêm người chơi: " + msg2);
                }
            });
        });
        yield return new WaitForSeconds(3f);
        All_Room_Pl = new List<List_All_Room_Players>();
        LoadAllRoomsAndPlayers();
        yield return new WaitForSeconds(2f);
        foreach(List_All_Room_Players room_pl in All_Room_Pl)
        {
            if(room_pl.maPhongChoi == maPhongChoi)
            {
                curent_list_players_room = new Players_Infor_Room()
                {
                    maPhongChoi = maPhongChoi,
                    MaNguoiChoi = GameController.Instance.Main_Player_Acc.MaNguoiChoi
                };
                break;
            }    
        }
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("Scene_In_Room");
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

    public void selcet_Charactor(GameObject Charactor_Select)
    {
        if(GameController.Instance.Player_Choice_Char == null)
        {
            GameDataModels.NhanVat_NguoiChoi data = new GameDataModels.NhanVat_NguoiChoi()
            {
                MaNhanVat = Charactor_Select.name.ToString(),
                MaNguoiChoi = GameController.Instance.Main_Player_Acc.MaNguoiChoi
            };
            DatabaseCtr.Instance.AddData("NguoiChoi_NhanVat", data, (ok, msg2) =>
            {
                if (ok)
                {
                    Debug.Log("👤 Thêm nhân vật người chơi thành công");
                    GameController.Instance.Player_Choice_Char = Charactor_Select;
                }
                else
                {
                    Debug.LogError("Thêm nhân vật người chơi thất bại " + msg2);
                }
            });
        }
        else
        {
            Dictionary<string, object> update = new Dictionary<string, object>();
            update["MaNhanVat"] = Charactor_Select.name;

            DatabaseCtr.Instance.UpdateDataByField(
                "NguoiChoi_NhanVat",
                "MaNguoiChoi",
                GameController.Instance.Main_Player_Acc.MaNguoiChoi,
                update,
                (ok, msg) =>
                {
                    if (ok)
                    {
                        Debug.Log("🔄 Update nhân vật thành công");
                        GameController.Instance.Player_Choice_Char = Charactor_Select;
                    }
                    else Debug.LogError(msg);
                }
            );
        } 
            
    }

    public void selcet_Map(GameObject Map_Select)
    {

        GameController.Instance.Main_Map = Map_Select; 

    }
    #endregion


    //===============================================================PVP===================================================================
    //=====================================================================================================================================
    //=====================================================================================================================================
  
    
    #region PvP 
    public void Creat_Room_PvP(Text Name_Room)
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
        string maNguoiChoi = GameController.Instance.Main_Player_Acc.MaNguoiChoi;
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

        DatabaseCtr.Instance.AddData<GameDataModels.PhongChoi>("PhongChoi_PvP", room, (success, message) =>
        {
            if (success)
            {
                Debug.Log($"✅ Tạo phòng '{room.TenPhongChoi}' thành công! ID: {room.MaPhongChoi}");
                Curent_Room_PvP = room;
                StartCoroutine(AddPlayerToRoom_PvP(room.MaPhongChoi, maNguoiChoi));
                /*                // Hiển thị ID ra UI
                                if (Id_Room != null)
                                    Id_Room.text = room.MaPhongChoi;*/


            }
            else
            {
                Curent_Room_PvP = null;
                Debug.LogError($"❌ Lỗi khi tạo phòng: {message}");
            }
        });


    }


    public void Join_Room_PvP(Text Id_Room)
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
"PhongChoi_PvP",
"MaPhongChoi",
roomIdKey.ToString(),
(ok, msg, room) =>
{
    if (ok)
    {
        Debug.Log("phòng chơi tồn tại");
        Curent_Room_PvP = room;
        StartCoroutine(AddPlayerToRoom_PvP(room.MaPhongChoi, maNguoiChoi));
    }
    else
    {
        Debug.Log("phòng chơi không tồn tại");
        Curent_Room_PvP = null;
    }
});



    }



    private IEnumerator AddPlayerToRoom_PvP(string maPhongChoi, string maNguoiChoi)
    {
        DatabaseCtr.Instance.CheckPlayerInRoom_PvP(maPhongChoi, maNguoiChoi, (exist, msg) =>
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

            DatabaseCtr.Instance.AddData("PhongChoi_NguoiChoi_PvP", data, (ok, msg2) =>
            {
                if (ok)
                {
                    Debug.Log("👤 Thêm người chơi vào phòng thành công!");
                }
                else
                {
                    Debug.LogError("❌ Lỗi khi thêm người chơi: " + msg2);
                }
            });
        });
        yield return new WaitForSeconds(3f);
        All_Room_Pl_PvP = new List<List_All_Room_Players>();
        LoadAllRoomsAndPlayers_PvP();
        yield return new WaitForSeconds(2f);
        foreach (List_All_Room_Players room_pl in All_Room_Pl_PvP)
        {
            if (room_pl.maPhongChoi == maPhongChoi)
            {
                curent_list_players_room_PvP = new Players_Infor_Room()
                {
                    maPhongChoi = maPhongChoi,
                    MaNguoiChoi = GameController.Instance.Main_Player_Acc.MaNguoiChoi
                };
                break;
            }
        }
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("Scene_In_Room_PvP");
    }


    private void LoadAllRoomsAndPlayers_PvP()
    {
        All_Room_Pl_PvP = new List<List_All_Room_Players>();

        DatabaseCtr.Instance.GetAllData<GameDataModels.PhongChoi>("PhongChoi_PvP", (ok, msg, listRooms) =>
        {
            if (!ok || listRooms == null)
            {
                Debug.LogError("❌ Lỗi khi lấy danh sách phòng: " + msg);
                return;
            }

            Debug.Log($"📌 Có {listRooms.Count} phòng chơi trong hệ thống.");

            foreach (var room in listRooms)
            {
                LoadPlayersOfRoom_PvP(room);
            }
        });
    }


    private void LoadPlayersOfRoom_PvP(GameDataModels.PhongChoi room)
    {
        DatabaseCtr.Instance.GetDataListByField<GameDataModels.PhongChoi_NguoiChoi>(
            "PhongChoi_NguoiChoi_PvP",
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

                All_Room_Pl_PvP.Add(newRoom);

                Debug.Log($"📌 Phòng {room.MaPhongChoi} có {newRoom.list_MaNguoiChoi.Count} người chơi.");
            }
        );
    }


    #endregion
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
}
