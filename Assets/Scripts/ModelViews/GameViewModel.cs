using UnityEngine;
using UniRx;

public class GameViewModel : ViewModel
{
    public readonly ReactiveProperty<bool> LoadGameRectIsVisible;
    public readonly ReactiveProperty<bool> GameRectIsVisible;
    public readonly ReactiveProperty<string> HangmanRandomNameText;
    
    public readonly ReactiveCollection<GameCheckButtonViewModel> CheckButton;
    
    public GameViewModel()
    {
        LoadGameRectIsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        GameRectIsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        HangmanRandomNameText = new ReactiveProperty<string>().AddTo(_disposables);
        
        CheckButton = new ReactiveCollection<GameCheckButtonViewModel>().AddTo(_disposables);
    }
}
