using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using System;
using System.Collections;
using System.Security.Cryptography;
using Firebase.Extensions;
public class AccountAuthManager : MonoBehaviour
{
    public static AccountAuthManager Instance;

    private DatabaseReference dbRef;

    // PBKDF2 params
    private const int SaltSize = 16; // bytes
    private const int HashBytes = 32; // 256-bit
    private const int Iterations = 10000;
    private void Awake()
    {
        Instance = this;
        dbRef = FirebaseDatabase.GetInstance("https://d-game-coop-fighting-default-rtdb.asia-southeast1.firebasedatabase.app/").RootReference;
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    void Start()
    {
       
    }
    private static byte[] GenerateSalt(int size = SaltSize)
    {
        var salt = new byte[size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }
    private static byte[] HashPassword(string password, byte[] salt, int iterations = Iterations, int hashBytes = HashBytes)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
        {
            return pbkdf2.GetBytes(hashBytes);
        }
    }
    private static bool CompareByteArrays(byte[] a, byte[] b)
    {
        if (a == null || b == null || a.Length != b.Length) return false;
        int diff = 0;
        for (int i = 0; i < a.Length; i++)
            diff |= a[i] ^ b[i];
        return diff == 0;
    }

    public void RegisterAccount(string username, string email, string phone, string plainPassword, Action<bool, string> callback)
    {
        // basic validation
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(plainPassword))
        {
            callback?.Invoke(false, "Username, email và password không được rỗng.");
            return;
        }

        // 1) kiểm tra email & username đã tồn tại chưa (unique)
        // check email
        dbRef.Child("accounts").OrderByChild("email").EqualTo(email).GetValueAsync()
        .ContinueWithOnMainThread(emailTask =>
        {
            if (emailTask.IsFaulted)
            {
                callback?.Invoke(false, "Lỗi kết nối: " + emailTask.Exception?.ToString());
                return;
            }

            if (emailTask.Result.Exists)
            {
                callback?.Invoke(false, "Email đã được sử dụng.");
                return;
            }

            // check username
            dbRef.Child("accounts").OrderByChild("username").EqualTo(username).GetValueAsync()
            .ContinueWithOnMainThread(userTask =>
            {
                if (userTask.IsFaulted)
                {
                    callback?.Invoke(false, "Lỗi kết nối: " + userTask.Exception?.ToString());
                    return;
                }

                if (userTask.Result.Exists)
                {
                    callback?.Invoke(false, "Username đã tồn tại.");
                    return;
                }

                // 2) tạo account mới
                string accountId = dbRef.Child("accounts").Push().Key; // unique key
                byte[] salt = GenerateSalt();
                byte[] hash = HashPassword(plainPassword, salt);

                Account account = new Account()
                {
                    accountId = accountId,
                    username = username,
                    email = email,
                    phone = phone,
                    passwordSalt = Convert.ToBase64String(salt),
                    passwordHash = Convert.ToBase64String(hash),
                };

                string json = JsonUtility.ToJson(account);

                dbRef.Child("accounts").Child(accountId).SetRawJsonValueAsync(json)
                .ContinueWithOnMainThread(writeTask =>
                {
                    if (writeTask.IsFaulted)
                    {
                        callback?.Invoke(false, "Lỗi khi ghi dữ liệu: " + writeTask.Exception?.ToString());
                        return;
                    }
                    callback?.Invoke(true, "Đăng ký thành công.");
                });
            });
        });
    }

    public void LoginWithUsername(string username, string plainPassword, Action<bool, string, Account> callback)
    {
        Debug.Log(username);
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(plainPassword))
        {
            callback?.Invoke(false, "Tên đăng nhập và mật khẩu không được rỗng.", null);
            return;
        }

        // Tìm account theo username
        dbRef.Child("accounts").OrderByChild("username").EqualTo(username).GetValueAsync()
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                callback?.Invoke(false, "Lỗi kết nối: " + task.Exception?.ToString(), null);
                return;
            }

            DataSnapshot snap = task.Result;
            if (!snap.Exists)
            {
                callback?.Invoke(false, "Không tìm thấy tài khoản với tên đăng nhập này.", null);
                return;
            }

            // Lấy tài khoản đầu tiên khớp username
            foreach (var child in snap.Children)
            {
                try
                {
                    string json = child.GetRawJsonValue();
                    Account acc = JsonUtility.FromJson<Account>(json);

                    // Giải mã salt + hash
                    byte[] salt = Convert.FromBase64String(acc.passwordSalt);
                    byte[] expectedHash = Convert.FromBase64String(acc.passwordHash);
                    byte[] actualHash = HashPassword(plainPassword, salt);

                    bool passwordMatches = CompareByteArrays(expectedHash, actualHash);

                    if (passwordMatches)
                    {
                        callback?.Invoke(true, "Đăng nhập thành công.", acc);
                        return;
                    }
                    else
                    {
                        callback?.Invoke(false, "Mật khẩu không đúng.", null);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    callback?.Invoke(false, "Lỗi xử lý dữ liệu: " + ex.Message, null);
                    return;
                }
            }

            callback?.Invoke(false, "Không tìm thấy tài khoản hợp lệ.", null);
        });
    }
}
