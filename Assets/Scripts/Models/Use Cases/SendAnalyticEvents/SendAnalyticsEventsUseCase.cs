using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
public class SendAnalyticsEventsUseCase : ISendAnalyticsEventsUseCase
{
    private readonly IAnalyticsEventsService _analyticsService;
    private readonly IEventDispatcherService _eventDispatcherService;
    public SendAnalyticsEventsUseCase(IAnalyticsEventsService analyticsService, IEventDispatcherService eventDispatcherService)
    {
        _analyticsService = analyticsService;
        _eventDispatcherService = eventDispatcherService;
    }

    public void SendLevelStart(int _level)
    {
        _analyticsService.StartNewLevel(_level);
    }

    public void SendNewChance(bool _value)
    {
        _analyticsService.ChooseNewChance(_value);
    }

    public void ShowAd()
    {
        _analyticsService.ShowAd();
    }
}
