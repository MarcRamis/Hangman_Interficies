using UniRx;
using UnityEngine;

public class ConfigViewModel : ViewModel
{
    public readonly ReactiveCommand RegisterButtonPressed;
    public readonly ReactiveCommand LoginButtonPressed;
    public readonly ReactiveCommand LogoutButtonPressed;
    public readonly ReactiveCommand<UserNameLogEvent> SaveButtonPressed;
    public readonly ReactiveCommand ExitButtonPressed;

    public readonly ReactiveProperty<Vector2> Position;
    public readonly ReactiveProperty<bool> LogButtonIsVisible;
    public readonly ReactiveProperty<bool> LogoutIsVisible;
    public readonly ReactiveProperty<bool> LogTextIsVisible;
    public readonly ReactiveProperty<bool> IsRegister;
    public readonly ReactiveProperty<bool> IsLogin;
    public readonly ReactiveProperty<string> EmailText;
    public readonly ReactiveProperty<string> PasswordText;

    public ConfigViewModel()
    {
        RegisterButtonPressed = new ReactiveCommand().AddTo(_disposables);
        LoginButtonPressed = new ReactiveCommand().AddTo(_disposables);
        LogoutButtonPressed = new ReactiveCommand().AddTo(_disposables);
        SaveButtonPressed = new ReactiveCommand<UserNameLogEvent>().AddTo(_disposables);
        ExitButtonPressed = new ReactiveCommand().AddTo(_disposables);

        Position = new ReactiveProperty<Vector2>().AddTo(_disposables);
        LogButtonIsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        LogoutIsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        LogTextIsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        IsRegister = new ReactiveProperty<bool>().AddTo(_disposables);
        IsLogin = new ReactiveProperty<bool>().AddTo(_disposables);
        EmailText = new ReactiveProperty<string>(string.Empty).AddTo(_disposables);
        PasswordText = new ReactiveProperty<string>(string.Empty).AddTo(_disposables);
    }
}