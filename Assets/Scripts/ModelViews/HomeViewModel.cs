using UniRx;

public class HomeViewModel : ViewModel
{
    public readonly ReactiveProperty<bool> isVisible;

    public HomeViewModel()
    {
        isVisible = new ReactiveProperty<bool>();
    }
}