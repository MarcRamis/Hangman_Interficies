using UniRx;

public class GameCheckButtonPresenter : Presenter
{
    private GameCheckButtonViewModel _viewModel;
    private IEventDispatcherService _eventDispatcher;

    public GameCheckButtonPresenter(GameCheckButtonViewModel viewModel, IEventDispatcherService eventDispatcher)
    {
        _viewModel = viewModel;
        _eventDispatcher = eventDispatcher;

        _eventDispatcher.Subscribe<ButtonCheckedEvent>(CheckButtonSolution);
    }

    private void CheckButtonSolution(ButtonCheckedEvent data)
    {
        if(data.letterID == _viewModel.CheckButtonText.Value)
        {
            if (data.isCorrect)
            {
                _viewModel.CheckButtonImage.Value = _viewModel.CorrectLetter.Value;
            }
            else
            {
                _viewModel.CheckButtonImage.Value = _viewModel.IncorrectLetter.Value;
                _eventDispatcher.Dispatch(new VaryLiveEvent(-1));
            }
        }
    }
    public override void Dispose()
    {
        base.Dispose();
        _eventDispatcher.Unsubscribe<ButtonCheckedEvent>(CheckButtonSolution);
    }
}
