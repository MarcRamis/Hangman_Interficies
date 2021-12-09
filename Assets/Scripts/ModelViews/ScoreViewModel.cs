using UniRx;
using UnityEngine;

public class ScoreViewModel : ViewModel
{
    public readonly ReactiveProperty<Vector2> Position;
    public readonly ReactiveCollection<ScoreUserItemViewModel> ScoreUsers;

    public ScoreViewModel()
    {
        Position = new ReactiveProperty<Vector2>().AddTo(_disposables);
        ScoreUsers = new ReactiveCollection<ScoreUserItemViewModel>().AddTo(_disposables);
    }
}
