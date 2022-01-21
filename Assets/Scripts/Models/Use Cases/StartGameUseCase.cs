using System.Threading.Tasks;

public class StartGameUseCase
{
    IEventDispatcherService _eventDispatcher;
    IHangmanAPIService _hangmanAPIService;
    IGoogleMobileAdsService _googleMobileAdsService;

    public StartGameUseCase(IEventDispatcherService eventDispatcher, IHangmanAPIService hangmanAPIService, IGoogleMobileAdsService googleMobileAdsService)
    {
        _eventDispatcher = eventDispatcher;
        _hangmanAPIService = hangmanAPIService;
        _googleMobileAdsService = googleMobileAdsService;
    }

    public async Task StartGame()
    {
        await _googleMobileAdsService.LoadReward();
        await _hangmanAPIService.GetRandomLetter();
        _hangmanAPIService.GetButtonLetters();
        
        _eventDispatcher.Dispatch<LoadScreenEvent>();
    }
}
