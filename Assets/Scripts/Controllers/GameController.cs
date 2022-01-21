using UniRx;
using UnityEngine;

public class GameController : Controller
{
    private readonly GameViewModel _viewModel;
    private readonly IUpdateGameUseCase _updateGame;
    private readonly SceneLoader _sceneLoader;
    private readonly ISendAnalyticsEventsUseCase _sendAnalyticsUseCase;

    public GameController(GameViewModel gameViewModel, IUpdateGameUseCase updateGame, SceneLoader sceneLoader, ISendAnalyticsEventsUseCase sendAnalyticsUseCase)
    {
        _viewModel = gameViewModel;
        _updateGame = updateGame;
        _sceneLoader = sceneLoader;
        _sendAnalyticsUseCase = sendAnalyticsUseCase;

        _viewModel.LoadGameRectIsVisible.Value = true;
        _viewModel.TotalLives.Value = 7;
        _viewModel.TotalScore.Value = 0;
        _viewModel.TotalTime.Value = 0;

        _viewModel.MenuButtonPressed.Subscribe((_) =>
        {
            _sceneLoader.Load("Menu");
            _updateGame.GoMenu(new ScoreUserPrefs(_viewModel.TotalScore.Value, _viewModel.TotalTime.Value));
            //Time.timeScale = 1;
        }).AddTo(_disposables);

        _viewModel.ContinueButtonPressed.Subscribe((_) =>
        {
            _updateGame.Reset(_viewModel.PlayerWin.Value);
            _sendAnalyticsUseCase.SendLevelStart(_viewModel.TotalCorrectLeters.Value);
            //Time.timeScale = 1;
        }).AddTo(_disposables);

        _viewModel.PauseButtonPressed.Subscribe((_) =>
        {
            // Close
            _viewModel.PauseRectIsVisible.Value = true;
            Time.timeScale = 0;

        }).AddTo(_disposables);

        _viewModel.ResumeButtonPressed.Subscribe((_) =>
        {
            Time.timeScale = 1;
            _viewModel.PauseRectIsVisible.Value = false;
        }).AddTo(_disposables);

        _viewModel.RestartButtonPressed.Subscribe((_) =>
        {
            Time.timeScale = 1;
            _viewModel.TotalTime.Value = 0;
            _sceneLoader.Load("Game");
            _sendAnalyticsUseCase.SendLevelStart(0);
        }).AddTo(_disposables);

        _viewModel.HomeButtonPressed.Subscribe((_) =>
        {
            Time.timeScale = 1;
            _viewModel.TotalTime.Value = 0;
            _sceneLoader.Load("Menu");
        }).AddTo(_disposables);
    }
}
