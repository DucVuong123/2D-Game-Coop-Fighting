using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{

    public GameDataModels.PhongChoi Curent_Room;

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

        // Tự sinh ID phòng 
        string roomId = GenerateRoomID(6);

        GameDataModels.PhongChoi room = new GameDataModels.PhongChoi()
        {

            MaPhongChoi = roomId,
            TenPhongChoi = Name_Room.text,  

        };

        DatabaseCtr.Instance.AddData<GameDataModels.PhongChoi>($"PhongChoi/{room.MaPhongChoi}", room, (success, message) =>
        {
            if (success)
            {
                Debug.Log($"✅ Tạo phòng '{room.TenPhongChoi}' thành công! ID: {room.MaPhongChoi}");
                Curent_Room = room;
                SceneManager.LoadScene(7);
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

        /*       // Gán ID ra Text hiển thị
               if (Id_Room != null)
                   Id_Room.text = roomId;
               else
                   Debug.LogWarning("⚠️ Chưa gán Text Id_Room trong Inspector!");*/
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

        // Lấy dữ liệu phòng từ Firebase
        DatabaseCtr.Instance.GetData<GameDataModels.PhongChoi>($"PhongChoi/{roomIdKey}", (success, data, message) =>
        {
            if (success)
            {
                Debug.Log($"✅ Tham gia phòng thành công: {data.TenPhongChoi} (ID hiển thị: {data.MaPhongChoi})");
                Curent_Room = data;
                // TODO: Chuyển scene hoặc logic vào phòng
            }
            else
            {
                Curent_Room = null;
                Debug.LogError($"❌ Không thể tham gia phòng: {message}");
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
}
