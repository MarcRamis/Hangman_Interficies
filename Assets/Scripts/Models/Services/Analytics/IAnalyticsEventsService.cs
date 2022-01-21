using System.Collections;
public interface IAnalyticsEventsService
{
    void StartNewLevel(int _level);
    void ChooseNewChance(bool _value);
    void ShowAd();
}
