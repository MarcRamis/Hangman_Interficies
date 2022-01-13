using UniRx;

public class GameCheckButtonController : Controller
{
    GameCheckButtonViewModel _viewModel;
    IUpdateGameUseCase _updateGameUseCase;

    public GameCheckButtonController(GameCheckButtonViewModel viewModel, IUpdateGameUseCase updateGameUseCase)
    {
        _viewModel = viewModel;
        _updateGameUseCase = updateGameUseCase;

        _viewModel.CheckButtonImage.Value = _viewModel.NoCheckButton.Value;

        _viewModel.CheckButtonPressed.Subscribe((_) => {
            _updateGameUseCase.CheckButton(_viewModel.CheckButtonText.Value);
        });
    }
}