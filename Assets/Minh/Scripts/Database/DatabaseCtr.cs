using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;

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
        string json = JsonUtility.ToJson(data);
        dbRef.Child(path).SetRawJsonValueAsync(json)
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
}
