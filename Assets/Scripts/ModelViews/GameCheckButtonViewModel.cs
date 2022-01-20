using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameCheckButtonViewModel : ViewModel
{
    public readonly ReactiveProperty<string> CheckButtonText;
    public readonly ReactiveProperty<Sprite> CheckButtonImage;
    public readonly ReactiveProperty<Sprite> NoCheckButton;
    public readonly ReactiveProperty<Sprite> CorrectLetter;
    public readonly ReactiveProperty<Sprite> IncorrectLetter;
    public readonly ReactiveProperty<bool> Interactable;

    public readonly ReactiveCommand CheckButtonPressed;
    
    public GameCheckButtonViewModel(string letter)
    {
        CheckButtonPressed = new ReactiveCommand().AddTo(_disposables);
        CheckButtonText = new ReactiveProperty<string>(letter).AddTo(_disposables);
        
        CheckButtonImage = new ReactiveProperty<Sprite>().AddTo(_disposables);
        NoCheckButton = new ReactiveProperty<Sprite>().AddTo(_disposables);
        CorrectLetter = new ReactiveProperty<Sprite>().AddTo(_disposables);
        IncorrectLetter = new ReactiveProperty<Sprite>().AddTo(_disposables);
        Interactable = new ReactiveProperty<bool>().AddTo(_disposables);
    }
}
