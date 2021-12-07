using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using System;
using UniRx;

public class FirebaseStoreService : IFirebaseStoreService
{
    IEventDispatcherService _eventDispatcher;
    public FirebaseStoreService(IEventDispatcherService eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
    }
    
    public void GetCurrentUserName()
    {
        string currentUserName = "Default";
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference usersRef = db.Collection("users").Document(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);

        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                currentUserName = snapshot.GetValue<string>("Name");
                _eventDispatcher.Dispatch(new UserNameEvent(currentUserName));
            }
        });
    }
}