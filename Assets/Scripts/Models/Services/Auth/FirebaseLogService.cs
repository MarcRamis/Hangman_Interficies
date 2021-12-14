using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Security.Cryptography;

public class FirebaseLogService : IFirebaseLogService
{
    readonly IEventDispatcherService eventDispatcher;

    //private readonly string _userNameKey = "UserNameKey";
    private readonly string _emailKey = "EmailKey";
    private readonly string _passwordKey = "PasswordKey";

    private UserNameLog _currentUser = null;

    public FirebaseLogService(IEventDispatcherService _eventDispatcher)
    {
        eventDispatcher = _eventDispatcher;

        if (EncryptedPlayerPrefs.HasKey(_emailKey) && EncryptedPlayerPrefs.HasKey(_passwordKey))
        {
            SetCurrentUser();
        }
    }

    public void Init()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                
                var app = Firebase.FirebaseApp.DefaultInstance;
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                return;
            }

            eventDispatcher.Dispatch(new LoggedEvent(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser != null));
            Debug.Log(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);
        });
    }
    public void Init2()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {

                var app = Firebase.FirebaseApp.DefaultInstance;
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                return;
            }

        });
    }

    public void LogAnonym()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.Log("Usuario anonimo creado");
            
            SetDefaultData();
            eventDispatcher.Dispatch(new LogEvent(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId));
        });
    }

    public void LogEmail(UserNameLog userNameLog)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignInWithEmailAndPasswordAsync(userNameLog.Email, userNameLog.Password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            if (_currentUser == null)
            {
                SaveUserNameOnPlayerPrefs(userNameLog);
                SetCurrentUser();
            }

        });
    }
    public void Logout()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignOut();
        
        DeleteUserName();
    }

    public void RegisterEmail(UserNameLog userNameLog)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        
        auth.CreateUserWithEmailAndPasswordAsync(userNameLog.Email, userNameLog.Password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            SaveUserNameOnPlayerPrefs(userNameLog);
            SetCurrentUser();
            SetDefaultData();
        });
    }

    public void SetDefaultData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        var user = new User("DefaultName");
        DocumentReference docRef = db.Collection("users").Document(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);

        docRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                LoadData();
            }
            else
                Debug.LogError(task.Exception);
        });
    }
    public void LoadData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        CollectionReference usersRef = db.Collection("users");
        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Debug.Log(String.Format("User: {0}", document.Id));
                var user = document.ConvertTo<User>();
            }

            Debug.Log("Read all data from the users collection.");
        });
    }
    public string GetID()
    {
        return Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    }
    private UserNameLog ReadUserName()
    {
        //var defaultValue = new UserNameLog("DefaultEmail", ("DefaultPw"));
        //var userNameLog = PlayerPrefs.GetString(_userNameKey, JsonUtility.ToJson(defaultValue));
        //var user = JsonUtility.FromJson<UserNameLog>(userNameLog);

        var userNameLog = new UserNameLog(EncryptedPlayerPrefs.GetString(_emailKey, "default"), EncryptedPlayerPrefs.GetString(_passwordKey, "default"));

        return userNameLog;
    }
    private void DeleteUserName()
    {
        EncryptedPlayerPrefs.DeleteKey(_emailKey);
        EncryptedPlayerPrefs.DeleteKey(_passwordKey);
        PlayerPrefs.Save();

        _currentUser = null;
    }
    private void SaveUserNameOnPlayerPrefs(UserNameLog userNameLog)
    {
        EncryptedPlayerPrefs.SetString(_emailKey, userNameLog.Email);
        EncryptedPlayerPrefs.SetString(_passwordKey, userNameLog.Password);
        PlayerPrefs.Save();
    }
    public void SetCurrentUser()
    {
        _currentUser = ReadUserName();
        //Debug.Log("E: " + _currentUser.Email + ' ' + "P: " + _currentUser.Password);
    }
    public UserNameLog GetCurrentUser()
    {
        return _currentUser;
    }
}

public class EncryptedPlayerPrefs
{

    // Encrypted PlayerPrefs
    // Written by Sven Magnus
    // MD5 code by Matthew Wegner (from [url]http://www.unifycommunity.com/wiki/index.php?title=MD5[/url])


    // Modify this key in this file :
    private static string privateKey = "9ETrEsWaFRach3gexaDr";

    // Add some values to this array before using EncryptedPlayerPrefs
    public static string[] keys = new string[] { "25648", "dmkldsm", "561df31f" };


    public static string Md5(string strToEncrypt)
    {
        UTF8Encoding ue = new UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    public static void SaveEncryption(string key, string type, string value)
    {
        int keyIndex = (int)Mathf.Floor(UnityEngine.Random.value * keys.Length);
        string secretKey = keys[keyIndex];
        string check = Md5(type + "_" + privateKey + "_" + secretKey + "_" + value);
        PlayerPrefs.SetString(key + "_encryption_check", check);
        PlayerPrefs.SetInt(key + "_used_key", keyIndex);
    }

    public static bool CheckEncryption(string key, string type, string value)
    {
        int keyIndex = PlayerPrefs.GetInt(key + "_used_key");
        string secretKey = keys[keyIndex];
        string check = Md5(type + "_" + privateKey + "_" + secretKey + "_" + value);
        if (!PlayerPrefs.HasKey(key + "_encryption_check")) return false;
        string storedCheck = PlayerPrefs.GetString(key + "_encryption_check");
        return storedCheck == check;
    }

    public static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        SaveEncryption(key, "int", value.ToString());
    }

    public static void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        SaveEncryption(key, "float", Mathf.Floor(value * 1000).ToString());
    }

    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        SaveEncryption(key, "string", value);
    }

    public static int GetInt(string key)
    {
        return GetInt(key, 0);
    }

    public static float GetFloat(string key)
    {
        return GetFloat(key, 0f);
    }

    public static string GetString(string key)
    {
        return GetString(key, "");
    }

    public static int GetInt(string key, int defaultValue)
    {
        int value = PlayerPrefs.GetInt(key);
        if (!CheckEncryption(key, "int", value.ToString())) return defaultValue;
        return value;
    }

    public static float GetFloat(string key, float defaultValue)
    {
        float value = PlayerPrefs.GetFloat(key);
        if (!CheckEncryption(key, "float", Mathf.Floor(value * 1000).ToString())) return defaultValue;
        return value;
    }

    public static string GetString(string key, string defaultValue)
    {
        string value = PlayerPrefs.GetString(key);
        if (!CheckEncryption(key, "string", value)) return defaultValue;
        return value;
    }

    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.DeleteKey(key + "_encryption_check");
        PlayerPrefs.DeleteKey(key + "_used_key");
    }

}