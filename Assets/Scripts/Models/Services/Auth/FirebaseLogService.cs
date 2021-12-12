using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;
using System;

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

        if (PlayerPrefs.HasKey(_emailKey) && PlayerPrefs.HasKey(_passwordKey))
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

        var userNameLog = new UserNameLog(PlayerPrefs.GetString(_emailKey), PlayerPrefs.GetString(_passwordKey));

        return userNameLog;
    }
    private void DeleteUserName()
    {
        PlayerPrefs.DeleteKey(_emailKey);
        PlayerPrefs.DeleteKey(_passwordKey);
        PlayerPrefs.Save();

        _currentUser = null;
    }
    private void SaveUserNameOnPlayerPrefs(UserNameLog userNameLog)
    {
        PlayerPrefs.SetString(_emailKey, userNameLog.Email);
        PlayerPrefs.SetString(_passwordKey, userNameLog.Password);        
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