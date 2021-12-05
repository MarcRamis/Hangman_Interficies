using UniRx;

public class HomeView : View
{
    private HomeViewModel _viewModel;

    public void SetViewModel(HomeViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel
            .isVisible
            .Subscribe((isVisible) =>
            {
                gameObject.SetActive(isVisible);
            });
    }
}
