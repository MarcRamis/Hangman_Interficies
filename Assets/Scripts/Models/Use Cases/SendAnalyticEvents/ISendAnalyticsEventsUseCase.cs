using System.Collections;

public interface ISendAnalyticsEventsUseCase
{
    void SendLevelStart(int _level);
    void SendNewChance(bool _value);
    void ShowAd();
}
