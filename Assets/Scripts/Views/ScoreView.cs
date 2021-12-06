using UniRx;
using UnityEngine;
using DG.Tweening;


public class ScoreView : View
{

    private ScoreViewModel _viewModel;

    public void SetViewModel(ScoreViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.Position.Subscribe((position) => {
            GetComponent<RectTransform>().DOAnchorPos(position, 1f, true);
        }).AddTo(_disposables);
    }
}
