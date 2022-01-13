
public class UpdateGameUseCase : IUpdateGameUseCase
{
    IEventDispatcherService _eventDispatcher;
    IHangmanAPIService _hangmanAPI;

    public UpdateGameUseCase(IEventDispatcherService eventDispatcher, IHangmanAPIService hangmanAPI)
    {
        _eventDispatcher = eventDispatcher;
        _hangmanAPI = hangmanAPI;
    }

    public void CheckButton(string letter)
    {
        _hangmanAPI.GuessLetter(letter);
    }
}