using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Analytics;
using System.Threading.Tasks;


public class AnalyticsEventsService : IAnalyticsEventsService
{
    readonly IEventDispatcherService eventDispatcher;
    public AnalyticsEventsService(IEventDispatcherService _eventDispatcher)
    {
        eventDispatcher = _eventDispatcher;

    }

    public void StartNewLevel(int _level)
    {
        Firebase.Analytics.FirebaseAnalytics
        .LogEvent(
            "level_start",
            "level",
            _level
        );
    }

    public void ChooseNewChance(bool _value)
    {
        Firebase.Analytics.FirebaseAnalytics
        .LogEvent(
            "new_chance",
            "view_ad",
            _value.ToString()
        );
    }

    public void ShowAd()
    {
        Firebase.Analytics.FirebaseAnalytics
        .LogEvent(
            "show_ad"
        );
    }

}
