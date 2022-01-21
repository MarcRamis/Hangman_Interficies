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

        _viewModel.PauseButtonPressed.Subscribe((_) =>
        {
            // Stop time
            _viewModel.PauseRectIsVisible.Value = true;
        }).AddTo(_disposables);

        _viewModel.ResumeButtonPressed.Subscribe((_) =>
        {
            // Resume time
            _viewModel.PauseRectIsVisible.Value = false;
        }).AddTo(_disposables);

        _viewModel.RestartButtonPressed.Subscribe((_) =>
        {
            _sceneLoader.Load("Game");
        }).AddTo(_disposables);

        _viewModel.HomeButtonPressed.Subscribe((_) =>
        {
            _sceneLoader.Load("Menu");
        }).AddTo(_disposables);
    }
}
