using UniRx;
using UnityEngine;

public class HomeViewModel : ViewModel
{
    public readonly ReactiveCommand PlayButtonPressed;
    public readonly ReactiveCommand ProfileButtonPressed;
    public readonly ReactiveCommand<string> SaveUserNameButtonPressed;

    public readonly ReactiveProperty<Vector2> Position;
    public readonly ReactiveProperty<bool> UserNameIsVisible;
    public readonly ReactiveProperty<string> ChangeName;
    public readonly ReactiveProperty<string> UserNameText;

    public HomeViewModel()
    {
        PlayButtonPressed = new ReactiveCommand().AddTo(_disposables);
        ProfileButtonPressed = new ReactiveCommand().AddTo(_disposables);
        SaveUserNameButtonPressed = new ReactiveCommand<string>().AddTo(_disposables);

        UserNameIsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        Position = new ReactiveProperty<Vector2>().AddTo(_disposables);
        ChangeName = new ReactiveProperty<string>().AddTo(_disposables); 
        UserNameText = new ReactiveProperty<string>().AddTo(_disposables);
    }
}