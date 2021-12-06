using UniRx;
using UnityEngine;
using DG.Tweening;

public class ConfigView : View
{

    private ConfigViewModel _viewModel;

    public void SetViewModel(ConfigViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.Position.Subscribe((position) => {
            GetComponent<RectTransform>().DOAnchorPos(position, 1f, true);
        }).AddTo(_disposables);

        //_viewModel
        //    .isVisible
        //    .Subscribe((isVisible) =>
        //    {
        //        gameObject.SetActive(isVisible);
        //    });
    }
}