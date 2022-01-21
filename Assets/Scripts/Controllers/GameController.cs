using UniRx;

public class GameController : Controller
{
    private readonly GameViewModel _viewModel;
    private readonly IUpdateGameUseCase _updateGame;
    private readonly SceneLoader _sceneLoader;

    public GameController(GameViewModel gameViewModel, IUpdateGameUseCase updateGame, SceneLoader sceneLoader)
    {
        _viewModel = gameViewModel;
        _updateGame = updateGame;
        _sceneLoader = sceneLoader;

        _viewModel.LoadGameRectIsVisible.Value = true;
        _viewModel.TotalLives.Value = 7;
        _viewModel.TotalScore.Value = 0;

        _viewModel.MenuButtonPressed.Subscribe((_) =>
        {
            _sceneLoader.Load("Menu");
            _updateGame.GoMenu();
        }).AddTo(_disposables);

        _viewModel.ContinueButtonPressed.Subscribe((_) =>
        {
            _updateGame.Reset(_viewModel.PlayerWin.Value);
        }).AddTo(_disposables);
    }
}
