public class HomePresenter : Presenter
{
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly HomeViewModel _viewModel;

    public HomePresenter(HomeViewModel viewModel, IEventDispatcherService eventDispatcherService)
    {
        _viewModel = viewModel;
        _eventDispatcherService = eventDispatcherService;
        
        _eventDispatcherService.Subscribe<UserNameEvent>(SetUserName);
    }

    private void SetUserName(UserNameEvent data)
    {
        _viewModel.UserNameText.SetValueAndForceNotify(data.UserName);
    }

    public override void Dispose()
    {
        base.Dispose();
        _eventDispatcherService.Unsubscribe<UserNameEvent>(SetUserName);
    }
}
