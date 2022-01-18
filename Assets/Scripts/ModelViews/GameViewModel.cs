using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Collections.Generic;

public class GameViewModel : ViewModel
{
    public readonly ReactiveProperty<bool> LoadGameRectIsVisible;
    public readonly ReactiveProperty<bool> GameRectIsVisible;
    public readonly ReactiveProperty<string> HangmanRandomNameText;
    
    public readonly ReactiveCollection<GameCheckButtonViewModel> CheckButton;
    public readonly ReactiveProperty<bool> Live1Visible;
    public readonly ReactiveProperty<bool> Live2Visible;
    public readonly ReactiveProperty<bool> Live3Visible;
    public readonly ReactiveProperty<bool> Live4Visible;
    public readonly ReactiveProperty<bool> Live5Visible;
    public readonly ReactiveProperty<bool> Live6Visible;
    public readonly ReactiveProperty<bool> Live7Visible;
    
    public readonly ReactiveProperty<int> TotalLives;

    public GameViewModel()
    {
        LoadGameRectIsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        GameRectIsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        HangmanRandomNameText = new ReactiveProperty<string>().AddTo(_disposables);
        
        CheckButton = new ReactiveCollection<GameCheckButtonViewModel>().AddTo(_disposables);
        
        Live1Visible = new ReactiveProperty<bool>().AddTo(_disposables);
        Live2Visible = new ReactiveProperty<bool>().AddTo(_disposables);
        Live3Visible = new ReactiveProperty<bool>().AddTo(_disposables);
        Live4Visible = new ReactiveProperty<bool>().AddTo(_disposables);
        Live5Visible = new ReactiveProperty<bool>().AddTo(_disposables);
        Live6Visible = new ReactiveProperty<bool>().AddTo(_disposables);
        Live7Visible = new ReactiveProperty<bool>().AddTo(_disposables);

        TotalLives = new ReactiveProperty<int>().AddTo(_disposables);
    }
}
