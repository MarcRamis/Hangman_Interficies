using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using static Firebase.Extensions.TaskExtension;

public partial class FirebaseRealtimeDatabaseService : IFirebaseRealtimeDatabaseService
{
    IEventDispatcherService _eventDispatcher;
    
    private List<ScoreUserPrefs> _scoreUserPrefs;

    public FirebaseRealtimeDatabaseService(IEventDispatcherService eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
    }

    public IReadOnlyList<ScoreUserPrefs> GetAll()
    {
        var scoreUsers = new List<ScoreUserPrefs>();

        foreach (var scoreUser in scoreUsers)
        {
            var taskEntity = new ScoreUserPrefs(scoreUser.Username, scoreUser.Position, scoreUser.Score, scoreUser.Timer);
            _scoreUserPrefs.Add(taskEntity);
        }
        
        return _scoreUserPrefs;
    }

    public void ReadDataBase()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users")
            .GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                // Do something with snapshot...
                foreach (DataSnapshot user in snapshot.Children)
                {
                    IDictionary dictUser = (IDictionary)user.Value;
                    Debug.Log("P" + dictUser["position"] + " - " + dictUser["username"] + " -S " + dictUser["score"] + " -T " + dictUser["timer"]);
                }
            }

        });
    }

    public void OrderedListByScore()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("score")
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

        // Do something with the data in args.Snapshot
        Debug.Log("Number of players - " + (args.Snapshot.ChildrenCount));
        int i = (int)args.Snapshot.ChildrenCount;
        foreach (DataSnapshot user in args.Snapshot.Children)
        {
            IDictionary dictUser = (IDictionary)user.Value;
            //Debug.Log("P" + dictUser["position"] + " - " + dictUser["username"] + " -S " + dictUser["score"] + " -T " + dictUser["timer"]);

            ScoreUserPrefs myUser = new ScoreUserPrefs(dictUser["username"].ToString(), i, int.Parse(dictUser["score"].ToString()), float.Parse(dictUser["timer"].ToString()));
            i--;
            string json = JsonUtility.ToJson(myUser);
            reference.Child("users").Child(user.Key).SetRawJsonValueAsync(json);
        }
    }

    public void OrderedListByPosition()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("position")
        .ValueChanged += HandleValueChanged2;
    }
    private void HandleValueChanged2(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        // Do something with the data in args.Snapshot
        Debug.Log("Number of players - " + (args.Snapshot.ChildrenCount));
        int i = 0;
        foreach (DataSnapshot user in args.Snapshot.Children)
        {
            IDictionary dictUser = (IDictionary)user.Value;
            Debug.Log("2 - P" + dictUser["position"] + " - " + dictUser["username"] + " -S " + dictUser["score"] + " -T " + dictUser["timer"]);
            //userScores[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dictUser["position"].ToString();
            //userScores[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = dictUser["username"].ToString();
            //userScores[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = dictUser["score"].ToString();
            //userScores[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = dictUser["timer"].ToString();
            i++;
        }

    }
}

