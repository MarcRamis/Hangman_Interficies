using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Collections.Generic;

public class GameViewModel : ViewModel
{
    public readonly ReactiveProperty<bool> LoadGameRectIsVisible;
    public readonly ReactiveProperty<bool> GameRectIsVisible;
    public readonly ReactiveProperty<bool> EndRectIsVisible;
    public readonly ReactiveProperty<bool> PauseRectIsVisible;
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
    public readonly ReactiveProperty<int> TotalScore;
    public readonly ReactiveProperty<int> CurrentCorrectLeters;

    public readonly ReactiveProperty<Color> StateColor;
    public readonly ReactiveProperty<bool> VictoryIsVisible;
    public readonly ReactiveProperty<string> TitleText;
    public readonly ReactiveProperty<string> TimeText;
    public readonly ReactiveProperty<string> ScoreText;
    public readonly ReactiveCommand MenuButtonPressed;
    public readonly ReactiveCommand ContinueButtonPressed;
    public readonly ReactiveProperty<bool> PlayerWin;

    public readonly ReactiveCommand PauseButtonPressed;
    public readonly ReactiveCommand HomeButtonPressed;
    public readonly ReactiveCommand RestartButtonPressed;
    public readonly ReactiveCommand ResumeButtonPressed;

    public GameViewModel()
    {
        LoadGameRectIsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        GameRectIsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        EndRectIsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        PauseRectIsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
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
        TotalScore = new ReactiveProperty<int>().AddTo(_disposables);

        StateColor = new ReactiveProperty<Color>().AddTo(_disposables);
        VictoryIsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        TitleText = new ReactiveProperty<string>().AddTo(_disposables);
        TimeText = new ReactiveProperty<string>().AddTo(_disposables);
        ScoreText = new ReactiveProperty<string>().AddTo(_disposables);
        MenuButtonPressed = new ReactiveCommand().AddTo(_disposables);
        ContinueButtonPressed = new ReactiveCommand().AddTo(_disposables);
        PlayerWin = new ReactiveProperty<bool>().AddTo(_disposables);

        PauseButtonPressed = new ReactiveCommand().AddTo(_disposables);
        HomeButtonPressed = new ReactiveCommand().AddTo(_disposables);
        RestartButtonPressed = new ReactiveCommand().AddTo(_disposables);
        ResumeButtonPressed = new ReactiveCommand().AddTo(_disposables);
    }
}
