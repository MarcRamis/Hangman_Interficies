using UniRx;
using UnityEngine;


public class ButtonsViewModel : ViewModel
{
    public readonly ReactiveCommand HomeButtonPressed;
    public readonly ReactiveCommand ScoreButtonPressed;
    public readonly ReactiveCommand ConfigButtonPressed;

    //public readonly ReactiveProperty<Vector2> HomeRectTransform;
    //public readonly ReactiveProperty<Vector2> ScoreRectTransform;
    //public readonly ReactiveProperty<Vector2> ConfigRectTransform;
    
    public ButtonsViewModel()
    {
        HomeButtonPressed = new ReactiveCommand();
        ScoreButtonPressed = new ReactiveCommand();
        ConfigButtonPressed = new ReactiveCommand();

        //HomeRectTransform = new ReactiveProperty<Vector2>();
        //ScoreRectTransform = new ReactiveProperty<Vector2>();
        //ConfigRectTransform = new ReactiveProperty<Vector2>();
    }
}