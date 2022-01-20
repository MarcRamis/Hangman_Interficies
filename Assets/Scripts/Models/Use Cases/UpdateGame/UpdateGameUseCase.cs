
public class UpdateGameUseCase : IUpdateGameUseCase
{
    IEventDispatcherService _eventDispatcher;
    IHangmanAPIService _hangmanAPI;
    IGoogleMobileAdsService _googleMobileAds;

    public UpdateGameUseCase(IEventDispatcherService eventDispatcher, IHangmanAPIService hangmanAPI, IGoogleMobileAdsService googleMobileAds)
    {
        _eventDispatcher = eventDispatcher;
        _hangmanAPI = hangmanAPI;
        _googleMobileAds = googleMobileAds;
    }

    public void CheckButton(string letter)
    {
        _hangmanAPI.GuessLetter(letter);
    }
    public async void Reset(bool playerWin)
    {
        if (!playerWin)
        {
            _googleMobileAds.LoadReward();
            _googleMobileAds.ShowRewardAd();
        }

        _eventDispatcher.Dispatch<ResetEvent>();
        await _hangmanAPI.GetRandomLetter();
    }
}