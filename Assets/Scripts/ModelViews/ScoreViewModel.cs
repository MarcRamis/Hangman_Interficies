using UniRx;
using UnityEngine;

public class ScoreViewModel : ViewModel
{
    public readonly ReactiveProperty<Vector2> Position;

    public ScoreViewModel()
    {
        Position = new ReactiveProperty<Vector2>();
    }
}
