
public class UpdateGameUseCase : IUpdateGameUseCase
{
    IEventDispatcherService _eventDispatcher;
    IHangmanAPIService _hangmanAPI;
    IGoogleMobileAdsService _googleMobileAds;
    IFirebaseRealtimeDatabaseService _firebaseRealtime;

    public UpdateGameUseCase(IEventDispatcherService eventDispatcher, IHangmanAPIService hangmanAPI, IGoogleMobileAdsService googleMobileAds, IFirebaseRealtimeDatabaseService firebaseRealtime)
    {
        _eventDispatcher = eventDispatcher;
        _hangmanAPI = hangmanAPI;
        _googleMobileAds = googleMobileAds;
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
        }

        _eventDispatcher.Dispatch<ResetEvent>();
        await _hangmanAPI.GetRandomLetter();
    }
    public async void GoMenu()
    {
        var scoreuser = new ScoreUserPrefs();
        _firebaseRealtime.SaveScore(scoreuser);
    }
}