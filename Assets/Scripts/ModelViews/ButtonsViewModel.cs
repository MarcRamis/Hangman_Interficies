using UniRx;
using UnityEngine;


public class ButtonsViewModel : ViewModel
{
    public readonly ReactiveCommand HomeButtonPressed;
    public readonly ReactiveCommand ScoreButtonPressed;
    public readonly ReactiveCommand ConfigButtonPressed;
    
    public ButtonsViewModel()
    {
        HomeButtonPressed = new ReactiveCommand().AddTo(_disposables);
        ScoreButtonPressed = new ReactiveCommand().AddTo(_disposables);
        ConfigButtonPressed = new ReactiveCommand().AddTo(_disposables);
    }
}