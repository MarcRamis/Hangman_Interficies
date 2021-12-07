using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using static Firebase.Extensions.TaskExtension;

public class RealTimeDataBase_Code : MonoBehaviour
{
    DatabaseReference reference;
    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        OrderedListByScore();
        OrderedListByPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReadDataBase()
    {
        FirebaseDatabase.DefaultInstance
      .GetReference("users")
      .GetValueAsync().ContinueWithOnMainThread(task => {
          if (task.IsFaulted)
          {
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              // Do something with snapshot...
              foreach(DataSnapshot user in snapshot.Children)
              {
                  IDictionary dictUser = (IDictionary)user.Value;
                  Debug.Log("P" + dictUser["position"] + " - " + dictUser["username"] + " -S " + dictUser["score"] + " -T " + dictUser["timer"]);
              }
          }
      });
    }

    private void OrderedListByScore()
    {
        FirebaseDatabase.DefaultInstance
        .GetReference("users").OrderByChild("score")
        .ValueChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
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
           
            UserDataBase myUser = new UserDataBase(dictUser["username"].ToString(), i, int.Parse(dictUser["score"].ToString()), float.Parse(dictUser["timer"].ToString()));
            i--;
            string json = JsonUtility.ToJson(myUser);
            reference.Child("users").Child(user.Key).SetRawJsonValueAsync(json);
        }

    }

    private void OrderedListByPosition()
    {
        FirebaseDatabase.DefaultInstance
        .GetReference("users").OrderByChild("position")
        .ValueChanged += HandleValueChanged2;
    }
    void HandleValueChanged2(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        // Do something with the data in args.Snapshot
        Debug.Log("Number of players - " + (args.Snapshot.ChildrenCount));
        foreach (DataSnapshot user in args.Snapshot.Children)
        {
            IDictionary dictUser = (IDictionary)user.Value;
            Debug.Log("2 - P" + dictUser["position"] + " - " + dictUser["username"] + " -S " + dictUser["score"] + " -T " + dictUser["timer"]);

        }

    }
}

public class UserDataBase
{
    public string username;
    public int position;
    public int score;
    public float timer;

    public UserDataBase()
    {
    }

    public UserDataBase(string _username, int _position, int _score, float _timer)
    {
        username = _username;
        position = _position;
        score = _score;
        timer = _timer;
    }
}