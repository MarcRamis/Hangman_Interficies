using UniRx;

public class ConfigController : Controller
{
    private ConfigViewModel _viewModel;
    public  ConfigController(ConfigViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.LogButtonIsVisible.Value = true;

        _viewModel.LoginButtonPressed.Subscribe((_) =>
        {
            _viewModel.LogButtonIsVisible.Value = false;
            _viewModel.LogTextIsVisible.Value = true;
        }).AddTo(_disposables);
        
        _viewModel.RegisterButtonPressed.Subscribe((_) =>
        {
            _viewModel.LogButtonIsVisible.Value = false;
            _viewModel.LogTextIsVisible.Value = true;
        }).AddTo(_disposables);
        
        _viewModel.SaveButtonPressed.Subscribe((_) =>
        {
            _viewModel.LogoutIsVisible.Value = true;
            _viewModel.LogTextIsVisible.Value = false;
        }).AddTo(_disposables);

        _viewModel.ExitButtonPressed.Subscribe((_) =>
        {
            _viewModel.LogButtonIsVisible.Value = true;
            _viewModel.LogTextIsVisible.Value = false;
        }).AddTo(_disposables);

        _viewModel.LogoutButtonPressed.Subscribe((_) =>
        {
            _viewModel.LogoutIsVisible.Value = false;
            _viewModel.LogButtonIsVisible.Value = true;
        }).AddTo(_disposables);
    }
}