using UniRx;

public class ConfigView : View
{

    private ConfigViewModel _viewModel;

    public void SetViewModel(ConfigViewModel viewModel)
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