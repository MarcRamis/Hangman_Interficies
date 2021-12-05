using UniRx;

public class ScoreViewModel : ViewModel
{
    public readonly ReactiveProperty<bool> isVisible;

    public ScoreViewModel()
    {
        isVisible = new ReactiveProperty<bool>();
    }
}
