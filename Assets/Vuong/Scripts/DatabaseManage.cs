using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using System;
using System.Collections;
public class DatabaseManage : MonoBehaviour
{
    public InputField Name;
    public InputField Point;

    public Text name_Text;
    public Text point_Text;

    private string userID;
    private DatabaseReference dbReference;

    void Start()
    {
        userID = "Admin";
        dbReference = FirebaseDatabase.GetInstance("https://d-game-coop-fighting-default-rtdb.asia-southeast1.firebasedatabase.app/").RootReference;

    }

    public void CreateUser()
    {
        UserInfo newUser =new UserInfo(Name.text, float.Parse(Point.text));
        string json = JsonUtility.ToJson(newUser);
        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }
    public IEnumerator GetName(Action<string> onCallback)
    {
        var userNameData = dbReference.Child("users").Child(userID).Child("user_Name").GetValueAsync();
        yield return new WaitUntil(() => userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
            onCallback.Invoke(snapshot.Value.ToString());
        }
    }

    public IEnumerator GetPoint(Action<float> onCallback)
    {
        var userGoldData = dbReference.Child("users").Child(userID).Child("user_Point").GetValueAsync();
        yield return new WaitUntil(() => userGoldData.IsCompleted);

        if (userGoldData != null)
        {
            DataSnapshot snapshot = userGoldData.Result;
            onCallback.Invoke(float.Parse(snapshot.Value.ToString()));
        }
    }
    public void GetUserInfo()
    {
        StartCoroutine(GetName((string name) =>
        {
            name_Text.text = "Name: " + name;
        }));

        StartCoroutine(GetPoint((float point) =>
        {
            point_Text.text = "Point: " + point.ToString();
        }));
    }
}
