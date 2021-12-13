using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using static Firebase.Extensions.TaskExtension;

public partial class FirebaseRealtimeDatabaseService : IFirebaseRealtimeDatabaseService
{
    IEventDispatcherService _eventDispatcher;

    public FirebaseRealtimeDatabaseService(IEventDispatcherService eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
    }

    public void ReadDataBase()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users")
            .GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted){}
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot user in snapshot.Children)
                {
                    IDictionary dictUser = (IDictionary)user.Value;
                    
                    var scoreUser = new ScoreUserPrefs(dictUser["Username"].ToString(),
                        int.Parse(dictUser["Position"].ToString()),
                        int.Parse(dictUser["Score"].ToString()),
                        float.Parse(dictUser["Timer"].ToString()));

                    _eventDispatcher.Dispatch<ScoreUserPrefs>(scoreUser);
                }
            }

        });
    }

    public void OrderedListByScore()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("Score")
        .ValueChanged += HandleValueChanged;
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        int i = (int)args.Snapshot.ChildrenCount;
        foreach (DataSnapshot user in args.Snapshot.Children)
        {
            IDictionary dictUser = (IDictionary)user.Value;


            ScoreUserPrefs myUser = new ScoreUserPrefs(dictUser["Username"].ToString(), i, int.Parse(dictUser["Score"].ToString()), float.Parse(dictUser["Timer"].ToString()));
            i--;
            string json = JsonUtility.ToJson(myUser);
            reference.Child("users").Child(user.Key).SetRawJsonValueAsync(json);
            
            //var scoreUser = new ScoreUserPrefs(dictUser["Username"].ToString(),
            //    int.Parse(dictUser["Position"].ToString()),
            //    int.Parse(dictUser["Score"].ToString()),
            //    float.Parse(dictUser["Timer"].ToString()));
            //
            //_eventDispatcher.Dispatch<ScoreUserPrefs>(scoreUser);
        }
        OrderedListByPosition();
    }

    public void OrderedListByPosition()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("Position")
        .ValueChanged += HandleValueChanged2;
    }
    private void HandleValueChanged2(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        foreach (DataSnapshot user in args.Snapshot.Children)
        {
            IDictionary dictUser = (IDictionary)user.Value;

            var scoreUser = new ScoreUserPrefs(dictUser["Username"].ToString(),
                int.Parse(dictUser["Position"].ToString()),
                int.Parse(dictUser["Score"].ToString()),
                float.Parse(dictUser["Timer"].ToString()));

            _eventDispatcher.Dispatch<ScoreUserPrefs>(scoreUser);
        }
    }
}

