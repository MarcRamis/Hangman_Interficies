using UniRx;

public class HomeViewModel : ViewModel
{
    public readonly ReactiveProperty<bool> changeUserNameIsVisible;
    public readonly ReactiveCommand ChangeUserNameButtonPressed;
    
    public HomeViewModel()
    {
        changeUserNameIsVisible = new ReactiveProperty<bool>();
        ChangeUserNameButtonPressed = new ReactiveCommand();
    }
}