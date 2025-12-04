using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using GameDataModels;

public class DatabaseCtr : MonoBehaviour, IEDatabase
{
    public static DatabaseCtr Instance;

    private DatabaseReference dbRef;

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
            return;
        }

        dbRef = FirebaseDatabase.GetInstance("https://d-game-coop-fighting-default-rtdb.asia-southeast1.firebasedatabase.app/").RootReference;
    }

    // ===============================
    // ADD (CREATE)
    // ===============================
    public void AddData<T>(string path, T data, Action<bool, string> callback = null)
    {
        string newKey = dbRef.Child(path).Push().Key;

        string json = JsonUtility.ToJson(data);

        dbRef.Child(path).Child(newKey).SetRawJsonValueAsync(json)
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                callback?.Invoke(false, "Add error: " + task.Exception);
            }
            else
            {
                callback?.Invoke(true, "Add success");
            }
        });
    }

    // ===============================
    // UPDATE (PATCH)
    // ===============================
    public void UpdateData<T>(string path, Dictionary<string, object> updateData, Action<bool, string> callback = null)
    {
        dbRef.Child(path).UpdateChildrenAsync(updateData)
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                callback?.Invoke(false, "Update error: " + task.Exception);
            }
            else
            {
                callback?.Invoke(true, "Update success");
            }
        });
    }



    public void UpdateDataByField(
    string path,
    string field,
    string value,
    Dictionary<string, object> updateData,
    Action<bool, string> callback)
    {
        dbRef.Child(path)
            .OrderByChild(field)
            .EqualTo(value)
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    callback(false, "Query error: " + task.Exception);
                    return;
                }

                DataSnapshot snap = task.Result;

                if (!snap.Exists)
                {
                    callback(false, "Không tìm thấy dữ liệu để update");
                    return;
                }

                // Update bản ghi đầu tiên phù hợp
                foreach (var child in snap.Children)
                {
                    dbRef.Child(path).Child(child.Key)
                        .UpdateChildrenAsync(updateData)
                        .ContinueWithOnMainThread(updateTask =>
                        {
                            if (updateTask.IsFaulted)
                                callback(false, "Update error: " + updateTask.Exception);
                            else
                                callback(true, "Update success");
                        });

                    return;
                }
            });
    }

    // ===============================
    // DELETE
    // ===============================
    public void DeleteData(string path, Action<bool, string> callback = null)
    {
        dbRef.Child(path).RemoveValueAsync()
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                callback?.Invoke(false, "Delete error: " + task.Exception);
            }
            else
            {
                callback?.Invoke(true, "Delete success");
            }
        });
    }

    // ===============================
    // GET (READ)
    // ===============================
    public void GetData<T>(string path, Action<bool, T, string> callback)
    {
        dbRef.Child(path).GetValueAsync()
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                callback?.Invoke(false, default(T), "Get error: " + task.Exception);
                return;
            }

            if (!task.Result.Exists)
            {
                callback?.Invoke(false, default(T), "Data not found");
                return;
            }

            string json = task.Result.GetRawJsonValue();
            T data = JsonUtility.FromJson<T>(json);

            callback?.Invoke(true, data, "Get success");
        });
    }




    public void GetDataByField<T>(string path, string fieldName, string fieldValue,
    Action<bool, string, T> callback)
    {
        dbRef.Child(path)
            .OrderByChild(fieldName)
            .EqualTo(fieldValue)
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    callback(false, "Get error: " + task.Exception, default);
                    return;
                }

                DataSnapshot snapshot = task.Result;

                if (!snapshot.Exists)
                {
                    callback(false, "Không tìm thấy dữ liệu với " + fieldName + " = " + fieldValue, default);
                    return;
                }

                // lấy item đầu tiên
                foreach (var child in snapshot.Children)
                {
                    T data = JsonUtility.FromJson<T>(child.GetRawJsonValue());
                    callback(true, "success", data);
                    return;
                }
            });
    }



    public void GetAllData<T>(string path, System.Action<bool, string, List<T>> callback)
    {
        dbRef.Child(path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                callback(false, "Lỗi khi đọc dữ liệu", null);
                return;
            }

            if (!task.IsCompleted)
            {
                callback(false, "Không thể load dữ liệu", null);
                return;
            }

            DataSnapshot snapshot = task.Result;
            List<T> list = new List<T>();

            foreach (var child in snapshot.Children)
            {
                try
                {
                    string json = child.GetRawJsonValue();
                    T obj = JsonUtility.FromJson<T>(json);
                    list.Add(obj);
                }
                catch
                {
                    Debug.LogError($"⚠ Lỗi convert JSON ở node: {child.Key}");
                }
            }

            callback(true, "OK", list);
        });
    }




    public void GetDataListByField<T>(
    string path,
    string field,
    string value,
    System.Action<bool, string, List<T>> callback)
    {
        dbRef.Child(path)
            .OrderByChild(field)
            .EqualTo(value)
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    callback(false, "Lỗi khi truy vấn", null);
                    return;
                }

                if (!task.IsCompleted)
                {
                    callback(false, "Query chưa hoàn thành", null);
                    return;
                }

                DataSnapshot snapshot = task.Result;
                List<T> list = new List<T>();

                if (!snapshot.Exists)
                {
                    callback(true, "Không có dữ liệu", list);
                    return;
                }

                foreach (var child in snapshot.Children)
                {
                    try
                    {
                        string json = child.GetRawJsonValue();
                        T obj = JsonUtility.FromJson<T>(json);
                        list.Add(obj);
                    }
                    catch
                    {
                        Debug.LogError("⚠ Lỗi convert JSON khi đọc dữ liệu query!");
                    }
                }

                callback(true, "OK", list);
            });
    }


    // ===============================
    // GENERATE_UNQUIE_ID_PLAYER_AC (READ)
    // ===============================
    public void GenerateUniquePlayerId(Action<string> callback)
    {
        string newId = "PLR_" + UnityEngine.Random.Range(100000, 999999).ToString();

        // Kiểm tra xem ID này đã tồn tại trong "NguoiChoi" chưa
        dbRef.Child("NguoiChoi")
            .OrderByChild("MaNguoiChoi")
            .EqualTo(newId)
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Lỗi kiểm tra ID: " + task.Exception);
                    // Nếu lỗi, cứ trả ID hiện tại
                    callback?.Invoke(newId);
                    return;
                }

                var snapshot = task.Result;
                if (snapshot.Exists)
                {
                    // ID đã tồn tại → thử lại
                    GenerateUniquePlayerId(callback);
                }
                else
                {
                    // ID chưa tồn tại → trả về
                    callback?.Invoke(newId);
                }
            });
    }

    // ===============================
    // GET_ALL_PALYERS
    // ===============================
    public void GetPlayersList(Action<bool, List<NguoiChoi>, string> callback)
    {
        dbRef.Child("NguoiChoi_Account").GetValueAsync()
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                callback?.Invoke(false, null, "Get list error: " + task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;

            List<NguoiChoi> list = new List<NguoiChoi>();

            if (!snapshot.Exists)
            {
                callback?.Invoke(true, list, "No data");
                return;
            }

            foreach (DataSnapshot child in snapshot.Children)
            {
                try
                {
                    string json = child.GetRawJsonValue();
                    if (string.IsNullOrEmpty(json)) continue;

                    NguoiChoi player = JsonUtility.FromJson<NguoiChoi>(json);
                    if (player != null)
                        list.Add(player);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning("JSON error: " + ex.Message);
                }
            }

            callback?.Invoke(true, list, "Get list success");
        });
    }

    // ===============================
    // CHECK_PALYERS_ROOM
    // ===============================

    public void CheckPlayerInRoom(string roomId, string playerId, Action<bool, string> callback)
    {
        dbRef.Child("PhongChoi_NguoiChoi")
            .OrderByChild("MaPhongChoi")
            .EqualTo(roomId)
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    callback(false, "error");
                    return;
                }

                DataSnapshot snap = task.Result;

                if (!snap.Exists)
                {
                    callback(false, "not_found");
                    return;
                }

                foreach (var child in snap.Children)
                {
                    var item = JsonUtility.FromJson<GameDataModels.PhongChoi_NguoiChoi>(child.GetRawJsonValue());
                    if (item.MaNguoiChoi == playerId)
                    {
                        callback(true, "exist");
                        return;
                    }
                }

                callback(false, "not_found");
            });
    }


    public void CheckPlayerInRoom_PvP(string roomId, string playerId, Action<bool, string> callback)
    {
        dbRef.Child("PhongChoi_NguoiChoi_PvP")
            .OrderByChild("MaPhongChoi")
            .EqualTo(roomId)
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    callback(false, "error");
                    return;
                }

                DataSnapshot snap = task.Result;

                if (!snap.Exists)
                {
                    callback(false, "not_found");
                    return;
                }

                foreach (var child in snap.Children)
                {
                    var item = JsonUtility.FromJson<GameDataModels.PhongChoi_NguoiChoi>(child.GetRawJsonValue());
                    if (item.MaNguoiChoi == playerId)
                    {
                        callback(true, "exist");
                        return;
                    }
                }

                callback(false, "not_found");
            });
    }
}
