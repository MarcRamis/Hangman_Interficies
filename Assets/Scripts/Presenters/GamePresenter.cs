using UnityEngine;

public class GamePresenter : Presenter
{
    GameViewModel _viewModel;
    EventDispatcherService _eventDispatcher;

    public GamePresenter(GameViewModel viewModel, EventDispatcherService eventDispatcher)
    {
        _viewModel = viewModel;
        _eventDispatcher = eventDispatcher;

        _eventDispatcher.Subscribe<HangmanRandomNameEvent>(SetHangmanText);
        _eventDispatcher.Subscribe<LoadScreenEvent>(ShowGame);
        _eventDispatcher.Subscribe<CheckButtonPrefs>(SetCheckButtons);
        _eventDispatcher.Subscribe<VaryLiveEvent>(ChangeLive);
        _eventDispatcher.Subscribe<VaryScoreEvent>(ChangeScore);
    }

    private void SetHangmanText(HangmanRandomNameEvent name)
    {
        _viewModel.HangmanRandomNameText.SetValueAndForceNotify(name.Text);
    }
    private void ShowGame(LoadScreenEvent data)
    {
        _viewModel.LoadGameRectIsVisible.Value = false;
        _viewModel.GameRectIsVisible.Value = true;
    }
    private void SetCheckButtons(CheckButtonPrefs data)
    {
        var gameCheckButtonsViewModel = new GameCheckButtonViewModel(data.letter);
        _viewModel.CheckButton.Add(gameCheckButtonsViewModel);
    }
    private void ChangeLive(VaryLiveEvent data)
    {
        _viewModel.TotalLives.Value += data._live;
        Debug.Log(_viewModel.TotalLives.Value);

        var totalLives = _viewModel.TotalLives.Value;

        if (totalLives < 1) { _viewModel.Live7Visible.Value = true; }
        else if (totalLives < 2) { _viewModel.Live6Visible.Value = true; }
        else if (totalLives < 3) { _viewModel.Live5Visible.Value = true; }
        else if (totalLives < 4) { _viewModel.Live4Visible.Value = true; }
        else if (totalLives < 5) { _viewModel.Live3Visible.Value = true; }
        else if (totalLives < 6) { _viewModel.Live2Visible.Value = true; }
        else if (totalLives < 7) { _viewModel.Live1Visible.Value = true; } // This means player has lost.
    }
    private void ChangeScore(VaryScoreEvent data)
    {
        //_viewModel.TotalScore.Value += data.
    }
}