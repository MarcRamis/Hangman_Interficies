using System.Threading.Tasks;

public class StartGameUseCase
{
    IEventDispatcherService _eventDispatcher;
    IHangmanAPIService _hangmanAPIService;

    public StartGameUseCase(IEventDispatcherService eventDispatcher, IHangmanAPIService hangmanAPIService)
    {
        _eventDispatcher = eventDispatcher;
        _hangmanAPIService = hangmanAPIService;
    }

    public async Task StartGame()
    {
        await _hangmanAPIService.GetRandomLetter();
        _hangmanAPIService.GetButtonLetters();
        
        _eventDispatcher.Dispatch<LoadScreenEvent>();
    }
}
