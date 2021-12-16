public class ScorePresenter : Presenter
{
    IEventDispatcherService _eventDispatcher;
    ScoreViewModel _viewModel;

    public ScorePresenter(ScoreViewModel viewModel, EventDispatcherService eventDispatcher)
    {
        _viewModel = viewModel;
        _eventDispatcher = eventDispatcher;

        _eventDispatcher.Subscribe<ScoreUserPrefs>(OnScoreUser);
    }

    private void OnScoreUser(ScoreUserPrefs data)
    {
        var scoreUserItemViewModel = new ScoreUserItemViewModel(data.Username, data.Position.ToString(), data.Score.ToString(), data.Timer.ToString());
        _viewModel.ScoreUsers.Add(scoreUserItemViewModel);
    }

    public override void Dispose()
    {
        base.Dispose();
        _eventDispatcher.Unsubscribe<ScoreUserPrefs>(OnScoreUser);
    }
}