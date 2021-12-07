using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseLogService : IFirebaseLogService
{
    readonly IEventDispatcherService eventDispatcher;

    public FirebaseLogService(IEventDispatcherService _eventDispatcher)
    {
        eventDispatcher = _eventDispatcher;

        Init();
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
            eventDispatcher.Dispatch(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser != null);
        });
    }

    public void Login()
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
            eventDispatcher.Dispatch(new LogEvent(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId));
        });
    }

    public string GetID()
    {
        return Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    }
}
