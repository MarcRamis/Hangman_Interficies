using UniRx;

public class HomeController : Controller
{
    private readonly HomeViewModel _viewModel;

    public HomeController(HomeViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.ChangeUserNameButtonPressed.Subscribe((_) => {
              _viewModel.changeUserNameIsVisible.Value = true;
          }).AddTo(_disposables);
    }
}
