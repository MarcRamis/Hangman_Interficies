using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class ButtonsView : View
{
    private ButtonsViewModel _viewModel;
    
    [SerializeField] private RectTransform _homeRectTransform;
    [SerializeField] private RectTransform _scoreRectTransform;
    [SerializeField] private RectTransform _configRectTransform;

    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _scoreButton;
    [SerializeField] private Button _configButton;
    
    public void SetViewModel(ButtonsViewModel viewModel)
    {
        _viewModel = viewModel;

        //_viewModel.HomeRectTransform.Subscribe((newPos) =>{
        //    _homeRectTransform.DOAnchorPos(newPos, 1f,true);
        //});
        //
        //_viewModel.ScoreRectTransform.Subscribe((newPos) => {
        //    _scoreRectTransform.DOAnchorPos(newPos, 1f, true);
        //});
        //
        //_viewModel.ConfigRectTransform.Subscribe((newPos) => {
        //    _configRectTransform.DOAnchorPos(newPos, 1f, true);
        //});

        _homeButton.onClick.AddListener(() =>
        {
            _viewModel.HomeButtonPressed.Execute();
        });

        _scoreButton.onClick.AddListener(() =>
        {
            _viewModel.ScoreButtonPressed.Execute();
        });

        _configButton.onClick.AddListener(() =>
        {
            _viewModel.ConfigButtonPressed.Execute();
        });
    }
}