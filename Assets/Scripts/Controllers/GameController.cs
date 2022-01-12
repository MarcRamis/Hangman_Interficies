public class GameController : Controller
{
    GameViewModel _gameViewModel;

    public GameController(GameViewModel gameViewModel)
    {
        _gameViewModel = gameViewModel;

        _gameViewModel.LoadGameRectIsVisible.Value = true;
    }
}
