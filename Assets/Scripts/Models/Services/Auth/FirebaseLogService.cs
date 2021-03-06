using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;
using System;
using System.Collections;
using System.Threading.Tasks;

public class FirebaseLogService : IFirebaseLogService
{
    readonly IEventDispatcherService eventDispatcher;

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

    public async Task InitByTask()
    {
        var dependencyStatus = await Firebase.FirebaseApp.CheckAndFixDependenciesAsync();

        if (dependencyStatus == Firebase.DependencyStatus.Available)
        {
            var app = Firebase.FirebaseApp.DefaultInstance;
        }
        else
        {
            UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            return;
        }

        //eventDispatcher.Dispatch(new LoggedEvent(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser != null));

        if (Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            if (GetCurrentUser() != null)
            {
                await LogEmailByTask(GetCurrentUser());
            }
            else
            {
                ServiceLocator.Instance.playerInfo.SetUserID(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);
            }
        }
        else
        {
            await LogAnonymByTask();
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
    public IEnumerator Init(float time)
    {
        yield return new WaitForSeconds(time);
        Init();
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
            ServiceLocator.Instance.playerInfo.SetUserID(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);
            Debug.Log("Usuario anonimo creado");

            SetDefaultData();
            eventDispatcher.Dispatch(new LogEvent(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId));
            
        });
        
    }

    public async Task LogAnonymByTask()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        var dependencyStatus = await auth.SignInAnonymouslyAsync();

        Debug.Log("Usuario anonimo creado");

        ServiceLocator.Instance.playerInfo.SetUserID(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);

        SetDefaultData();
        eventDispatcher.Dispatch(new LogEvent(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId));
        


    }

    public void LogEmail(UserNameLog userNameLog)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignInWithEmailAndPasswordAsync(userNameLog.Email, userNameLog.Password).ContinueWithOnMainThread(task =>
        {
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
            ServiceLocator.Instance.playerInfo.SetUserID(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            if (_currentUser == null)
            {
                SaveUserNameOnPlayerPrefs(userNameLog);
                SetCurrentUser();
            }

        });
        
    }
    public async Task LogEmailByTask(UserNameLog userNameLog)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        var dependencyStatus = await auth.SignInWithEmailAndPasswordAsync(userNameLog.Email, userNameLog.Password);
            

        Firebase.Auth.FirebaseUser newUser = dependencyStatus;
        ServiceLocator.Instance.playerInfo.SetUserID(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);
        Debug.LogFormat("User signed in successfully: {0} ({1})",
            newUser.DisplayName, newUser.UserId);

        if (_currentUser == null)
        {
            SaveUserNameOnPlayerPrefs(userNameLog);
            SetCurrentUser();
        }


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

        auth.CreateUserWithEmailAndPasswordAsync(userNameLog.Email, userNameLog.Password).ContinueWithOnMainThread(task =>
        {
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
            ServiceLocator.Instance.playerInfo.SetUserID(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);
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
        DocumentReference docRef = db.Collection("users").Document(ServiceLocator.Instance.playerInfo.GetUserID());

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
        var userNameLog = new UserNameLog(
            EncryptedPlayerPrefs.GetString(_emailKey, "default"),
            EncryptedPlayerPrefs.GetString(_passwordKey, "default"));

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
    }
    public UserNameLog GetCurrentUser()
    {
        return _currentUser;
    }
}
