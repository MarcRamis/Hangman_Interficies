using UniRx;
using UnityEngine;

public class HomeViewModel : ViewModel
{
    public readonly ReactiveProperty<bool> changeUserNameIsVisible;
    public readonly ReactiveCommand ChangeUserNameButtonPressed;

    public readonly ReactiveProperty<Vector2> Position;

    public HomeViewModel()
    {
        changeUserNameIsVisible = new ReactiveProperty<bool>();
        ChangeUserNameButtonPressed = new ReactiveCommand();

        Position = new ReactiveProperty<Vector2>();
    }
}