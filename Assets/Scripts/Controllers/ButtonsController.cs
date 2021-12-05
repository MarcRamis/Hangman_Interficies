public class ButtonsController : Controller
{
    private readonly ButtonsViewModel _viewModel;

    private readonly HomeViewModel _homeViewModel;
    private readonly ScoreViewModel _scoreViewModel;
    private readonly ConfigViewModel _configViewModel;

    public ButtonsController(ButtonsViewModel viewModel, HomeViewModel homeViewModel, ScoreViewModel scoreViewModel, ConfigViewModel configViewModel)
    {
        _viewModel = viewModel;
        _homeViewModel = homeViewModel;
        _scoreViewModel = scoreViewModel;
        _configViewModel = configViewModel;
    }
}