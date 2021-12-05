using UniRx;

public class ScoreView : View
{

    private ScoreViewModel _viewModel;

    public void SetViewModel(ScoreViewModel viewModel)
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
