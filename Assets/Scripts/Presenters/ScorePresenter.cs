public class ScorePresenter : Presenter
{
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly ScoreViewModel _viewModel;

    public ScorePresenter(ScoreViewModel viewModel, IEventDispatcherService eventDispatcher)
    {
        _eventDispatcherService = eventDispatcher;
        _viewModel = viewModel;

    }
}
