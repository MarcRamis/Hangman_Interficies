﻿using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using System.Threading.Tasks;
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
                ServiceLocator.Instance.playerInfo.SetUserName(currentUserName);
                _eventDispatcher.Dispatch(new UserNameEvent(currentUserName));
            }
        });
    }
    public async Task GetCurrentUserNameByTask()
    {
        string currentUserName = "Default";
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference usersRef = db.Collection("users").Document(ServiceLocator.Instance.playerInfo.GetUserID());

        DocumentSnapshot snapshot = await usersRef.GetSnapshotAsync();
        if (snapshot.Exists)
        {
            currentUserName = snapshot.GetValue<string>("Name");
            //_eventDispatcher.Dispatch(new UserNameEvent(currentUserName));
            ServiceLocator.Instance.playerInfo.SetUserName(currentUserName);
        }
    }
    public void SetCurrentUserNameInHome()
    {
        _eventDispatcher.Dispatch(new UserNameEvent(ServiceLocator.Instance.playerInfo.GetUserName()));
    }
    public void StoreNewUserName(string newUserName)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        var user = new User(newUserName);
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
        ServiceLocator.Instance.playerInfo.SetUserName(newUserName);
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
                var user = document.ConvertTo<User>();
            }
        });
    }
}