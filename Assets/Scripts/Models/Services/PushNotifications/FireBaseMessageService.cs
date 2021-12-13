using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;
using System;
using Firebase.Messaging;

public class FireBaseMessageService : IFireBaseMessageService
{
    readonly IEventDispatcherService eventDispatcher;

    public FireBaseMessageService(IEventDispatcherService _eventDispatcher)
    {
        eventDispatcher = _eventDispatcher;

    }

    public void Init()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        Debug.Log("Suscribe");
    }
    public void UnSuscribe()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived -= OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived -= OnMessageReceived;
        Debug.Log("UnSuscribe");
    }
    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    }

}
