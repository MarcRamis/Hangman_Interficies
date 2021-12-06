using UniRx;
using UnityEngine;

public class ConfigViewModel : ViewModel
{
    public readonly ReactiveProperty<Vector2> Position;

    public ConfigViewModel()
    {
        Position = new ReactiveProperty<Vector2>();
    }
}