using UniRx;
using UnityEngine;


public class ButtonsViewModel : ViewModel
{
    public readonly ReactiveCommand HomeButtonPressed;
    public readonly ReactiveCommand ScoreButtonPressed;
    public readonly ReactiveCommand ConfigButtonPressed;
    
    public ButtonsViewModel()
    {
        HomeButtonPressed = new ReactiveCommand();
        ScoreButtonPressed = new ReactiveCommand();
        ConfigButtonPressed = new ReactiveCommand();
    }
}