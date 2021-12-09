public class ConfigPresenter : Presenter
{
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly ConfigViewModel _viewModel;
    public ConfigPresenter(ConfigViewModel viewModel, IEventDispatcherService eventDispatcher)
    {
        _eventDispatcherService = eventDispatcher;
        _viewModel = viewModel;

        _eventDispatcherService.Dispatch<ActivateMessageEvent>();
    }
    
}
