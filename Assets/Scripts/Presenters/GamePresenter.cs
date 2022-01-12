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
}