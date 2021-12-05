using UniRx;

public class ConfigViewModel : ViewModel
{
    public readonly ReactiveProperty<bool> isVisible;

    public ConfigViewModel()
    {
        isVisible = new ReactiveProperty<bool>();
    }
}