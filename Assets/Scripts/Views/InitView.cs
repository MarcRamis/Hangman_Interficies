using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InitView : View
{
    [SerializeField] private Image load_image;

    InitViewModel _viewModel;
    public void SetViewModel(InitViewModel viewModel)
    {
        _viewModel = viewModel;
        InvokeRepeating("LoadAnimation", 0, 1.0f);
    }

    private void LoadAnimation()
    {
        load_image.transform.DORotate(new Vector3(0, 0, -360), 1, RotateMode.WorldAxisAdd);
    }
}
