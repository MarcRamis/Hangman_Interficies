public class HomeController : Controller
{
    private readonly HomeViewModel _viewModel;

    public HomeController(HomeViewModel viewModel)
    {
        _viewModel = viewModel;
    }
}
