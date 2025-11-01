using System.Collections.Generic;
using System;
using UnityEngine;

public interface IEDatabase
{
     void AddData<T>(string path, T data, Action<bool, string> callback = null);
     void UpdateData<T>(string path, Dictionary<string, object> updateData, Action<bool, string> callback = null);
     void DeleteData(string path, Action<bool, string> callback = null);
     void GetData<T>(string path, Action<bool, T, string> callback);
}
