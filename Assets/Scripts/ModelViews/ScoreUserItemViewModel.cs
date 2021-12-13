using UniRx;

public class ScoreUserItemViewModel : ViewModel
{
    public readonly ReactiveProperty<string> Position;
    public readonly ReactiveProperty<string> UserName;
    public readonly ReactiveProperty<string> Score;
    public readonly ReactiveProperty<string> Time;
    
    public ScoreUserItemViewModel(string userName, string postion, string score, string time)
    {
        Position = new ReactiveProperty<string>(postion).AddTo(_disposables);
        UserName = new ReactiveProperty<string>(userName).AddTo(_disposables);
        Score = new ReactiveProperty<string>(score).AddTo(_disposables);
        Time = new ReactiveProperty<string>(time).AddTo(_disposables);
    }
}