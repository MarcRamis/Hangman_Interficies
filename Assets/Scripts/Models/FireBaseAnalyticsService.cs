using UnityEngine;
using System;


public class FireBaseAnalyticsService : IFireBaseAnalyticsService
{

    public FireBaseAnalyticsService()
    {

    }
    
    public void StartLevel()
    {
        Firebase.Analytics.FirebaseAnalytics
        .LogEvent(
            Firebase.Analytics.FirebaseAnalytics.EventLevelStart,
            Firebase.Analytics.FirebaseAnalytics.ParameterLevel,
            0
        );
    }

    public void Level(int _numWords)
    {

    }

}
