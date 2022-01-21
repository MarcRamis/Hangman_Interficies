
public class UpdateGameUseCase : IUpdateGameUseCase
{
    IEventDispatcherService _eventDispatcher;
    IHangmanAPIService _hangmanAPI;
    IGoogleMobileAdsService _googleMobileAds;
    IFirebaseRealtimeDatabaseService _firebaseRealtime;
    IAnalyticsEventsService _sendAnalytic;

    public UpdateGameUseCase(IEventDispatcherService eventDispatcher, IHangmanAPIService hangmanAPI, IGoogleMobileAdsService googleMobileAds, IAnalyticsEventsService sendAnalytic, IFirebaseRealtimeDatabaseService firebaseRealtime)
    {
        _eventDispatcher = eventDispatcher;
        _hangmanAPI = hangmanAPI;
        _googleMobileAds = googleMobileAds;
        _sendAnalytic = sendAnalytic;
        _firebaseRealtime = firebaseRealtime;
    }

    public void CheckButton(string letter)
    {
        _hangmanAPI.GuessLetter(letter);
    }
    public async void Reset(bool playerWin)
    {
        if (!playerWin)
        {
            await _googleMobileAds.LoadReward();
            _googleMobileAds.ShowRewardAd();
            _sendAnalytic.ChooseNewChance(true);
            _sendAnalytic.ShowAd();
        }
        
        _sendAnalytic.ChooseNewChance(false);
        _eventDispatcher.Dispatch<ResetEvent>();
        await _hangmanAPI.GetRandomLetter();
    }
    public async void GoMenu(ScoreUserPrefs scoreuser)
    {
        _firebaseRealtime.SaveScore(scoreuser);
    }
    public void UpdateTime(float delta)
    {
        
    }
}