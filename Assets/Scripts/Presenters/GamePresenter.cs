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
        _eventDispatcher.Subscribe<PlayerHasWonEvent>(PlayerWins);
        _eventDispatcher.Subscribe<PlayerHasLostEvent>(PlayerLoses);
        _eventDispatcher.Subscribe<ResetEvent>(ResetGame);
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

        var totalLives = _viewModel.TotalLives.Value;

        if (totalLives < 1) { _viewModel.Live7Visible.Value = true; _eventDispatcher.Dispatch(new PlayerHasLostEvent()); }
        else if (totalLives < 2) { _viewModel.Live6Visible.Value = true; }
        else if (totalLives < 3) { _viewModel.Live5Visible.Value = true; }
        else if (totalLives < 4) { _viewModel.Live4Visible.Value = true; }
        else if (totalLives < 5) { _viewModel.Live3Visible.Value = true; }
        else if (totalLives < 6) { _viewModel.Live2Visible.Value = true; }
        else if (totalLives < 7) { _viewModel.Live1Visible.Value = true; } 
    }
    private void ChangeScore(VaryScoreEvent data)
    {   
        _viewModel.TotalScore.Value += data._score * 100;
        _viewModel.TotalCorrectLeters.Value += data._score;
       
    }
    private void PlayerLoses(PlayerHasLostEvent data)
    {
        _viewModel.EndRectIsVisible.Value = true;
        _viewModel.TitleText.SetValueAndForceNotify("Defeat");
        int time = _viewModel.TotalTime.Value;
        _viewModel.TimeText.SetValueAndForceNotify((time.ToString()));
        _viewModel.StateColor.Value = new Color(237f / 255f, 33f / 255f, 124f / 255f);
        _viewModel.VictoryIsVisible.Value = false;
        _viewModel.ScoreText.SetValueAndForceNotify(_viewModel.TotalScore.Value.ToString());
        _viewModel.PlayerWin.Value = false;
    }
    private void PlayerWins(PlayerHasWonEvent data)
    {
        _viewModel.EndRectIsVisible.Value = true;
        _viewModel.TitleText.SetValueAndForceNotify("Victory");
        int time = _viewModel.TotalTime.Value;
        _viewModel.TimeText.SetValueAndForceNotify((time.ToString()));
        _viewModel.StateColor.Value = new Color(203f / 255f, 205f / 255f, 77f / 255f);
        _viewModel.VictoryIsVisible.Value = true;
        _viewModel.ScoreText.SetValueAndForceNotify(_viewModel.TotalScore.Value.ToString());
        _viewModel.PlayerWin.Value = true;
    }
    private void ResetGame(ResetEvent data)
    {
        _viewModel.EndRectIsVisible.Value = false;
        _viewModel.TotalLives.Value = 7;

        foreach (GameCheckButtonViewModel checkButton in _viewModel.CheckButton)
        {
            checkButton.CheckButtonImage.Value = checkButton.NoCheckButton.Value;
            checkButton.Interactable.Value = true;
        }
        _viewModel.Live1Visible.Value = false;
        _viewModel.Live2Visible.Value = false;
        _viewModel.Live3Visible.Value = false;
        _viewModel.Live4Visible.Value = false;
        _viewModel.Live5Visible.Value = false;
        _viewModel.Live6Visible.Value = false;
        _viewModel.Live7Visible.Value = false;
    }

    public override void Dispose()
    {
        base.Dispose();
        _eventDispatcher.Unsubscribe<HangmanRandomNameEvent>(SetHangmanText);
        _eventDispatcher.Unsubscribe<LoadScreenEvent>(ShowGame);
        _eventDispatcher.Unsubscribe<CheckButtonPrefs>(SetCheckButtons);
        _eventDispatcher.Unsubscribe<VaryLiveEvent>(ChangeLive);
        _eventDispatcher.Unsubscribe<VaryScoreEvent>(ChangeScore);
        _eventDispatcher.Unsubscribe<PlayerHasWonEvent>(PlayerWins);
        _eventDispatcher.Unsubscribe<PlayerHasLostEvent>(PlayerLoses);
        _eventDispatcher.Unsubscribe<ResetEvent>(ResetGame);
    }
}
