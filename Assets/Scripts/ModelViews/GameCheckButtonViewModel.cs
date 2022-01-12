using UniRx;

public class GameCheckButtonViewModel : ViewModel
{
    public readonly ReactiveProperty<string> CheckButtonText;
    public GameCheckButtonViewModel(string letter)
    {
        CheckButtonText = new ReactiveProperty<string>(letter).AddTo(_disposables);
    }
}
