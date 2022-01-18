public class GameController : Controller
{
    GameViewModel _gameViewModel;

    public GameController(GameViewModel gameViewModel)
    {
        _gameViewModel = gameViewModel;

        _gameViewModel.LoadGameRectIsVisible.Value = true;
        _gameViewModel.TotalLives.Value = 7;
        _gameViewModel.TotalScore.Value = 0;
    }
}
