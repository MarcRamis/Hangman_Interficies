using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;
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
            if (task.IsFaulted) { }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot user in snapshot.Children)
                {
                    IDictionary dictUser = (IDictionary)user.Value;

                    var scoreUser = new ScoreUserPrefs(dictUser["Username"].ToString(),
                        int.Parse(dictUser["Position"].ToString()),
                        int.Parse(dictUser["Score"].ToString()),
                        int.Parse(dictUser["Timer"].ToString()));

                    _eventDispatcher.Dispatch<ScoreUserPrefs>(scoreUser);
                }
            }
        });
    }
    public async Task ReadDataBase1()
    {
        DataSnapshot snapshot = await FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync();
        foreach (DataSnapshot user in snapshot.Children)
        {
            IDictionary dictUser = (IDictionary)user.Value;

            var scoreUser = new ScoreUserPrefs(dictUser["Username"].ToString(),
                int.Parse(dictUser["Position"].ToString()),
                int.Parse(dictUser["Score"].ToString()),
                int.Parse(dictUser["Timer"].ToString()));

            _eventDispatcher.Dispatch<ScoreUserPrefs>(scoreUser);
        }
    }
    public void OrderedListByScore()
    {
        //Hacer un dispatch para pasar un bool al presenter de score
        _eventDispatcher.Dispatch<DeleteScoreList>();
        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("Score")
        .ValueChanged += HandleValueChanged;
    }

    public async Task OrderedListByScoreByTask()
    {
        //Hacer un dispatch para pasar un bool al presenter de score
        //_eventDispatcher.Dispatch<DeleteScoreList>();
        await FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync();
        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("Score")
        .ValueChanged += HandleValueChangedByTask;
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


            ScoreUserPrefs myUser = new ScoreUserPrefs(dictUser["Username"].ToString(), i, int.Parse(dictUser["Score"].ToString()), int.Parse(dictUser["Timer"].ToString()));
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

    private async void HandleValueChangedByTask(object sender, ValueChangedEventArgs args)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        _eventDispatcher.Dispatch<DeleteScoreList>();
        int i = (int)args.Snapshot.ChildrenCount;
        foreach (DataSnapshot user in args.Snapshot.Children)
        {
            IDictionary dictUser = (IDictionary)user.Value;


            ScoreUserPrefs myUser = new ScoreUserPrefs(dictUser["Username"].ToString(), i, int.Parse(dictUser["Score"].ToString()), int.Parse(dictUser["Timer"].ToString()));
            i--;
            string json = JsonUtility.ToJson(myUser);
            reference.Child("users").Child(user.Key).SetRawJsonValueAsync(json).GetAwaiter();

            //var scoreUser = new ScoreUserPrefs(dictUser["Username"].ToString(),
            //    int.Parse(dictUser["Position"].ToString()),
            //    int.Parse(dictUser["Score"].ToString()),
            //    float.Parse(dictUser["Timer"].ToString()));
            //
            //_eventDispatcher.Dispatch<ScoreUserPrefs>(scoreUser);
        }
        await OrderedListByPositionByTask();
    }

    public void OrderedListByPosition()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("Position")
        .ValueChanged += HandleValueChanged2;
    }
    public async Task OrderedListByPositionByTask()
    {
        _eventDispatcher.Dispatch<DeleteScoreList>();
        DataSnapshot snapshot = await FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync();
        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("Position")
        .ValueChanged += HandleValueChanged2ByTask;
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
                int.Parse(dictUser["Timer"].ToString()));

            _eventDispatcher.Dispatch<ScoreUserPrefs>(scoreUser);
        }
    }

    private void HandleValueChanged2ByTask(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        _eventDispatcher.Dispatch<DeleteScoreList>();
        foreach (DataSnapshot user in args.Snapshot.Children)
        {
            IDictionary dictUser = (IDictionary)user.Value;

            var scoreUser = new ScoreUserPrefs(dictUser["Username"].ToString(),
                int.Parse(dictUser["Position"].ToString()),
                int.Parse(dictUser["Score"].ToString()),
                int.Parse(dictUser["Timer"].ToString()));

            _eventDispatcher.Dispatch<ScoreUserPrefs>(scoreUser);
        }
    }

    public void SaveScore(ScoreUserPrefs scoreUser)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        //reference.Child("users").Child(ServiceLocator.Instance.playerInfo.GetUserID()).Child("Username").SetValueAsync(ServiceLocator.Instance.playerInfo.GetUserName());
        //reference.Child("users").Child(ServiceLocator.Instance.playerInfo.GetUserID()).Child("Timer").SetValueAsync(scoreUser.Timer);
        //reference.Child("users").Child(ServiceLocator.Instance.playerInfo.GetUserID()).Child("Score").SetValueAsync(scoreUser.Score);
        //reference.Child("users").Child(ServiceLocator.Instance.playerInfo.GetUserID()).Child("Position").SetValueAsync(0);
    }
}