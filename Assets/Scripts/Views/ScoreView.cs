using System.Collections.Generic;
using UniRx;
using UnityEngine;
using DG.Tweening;

public class ScoreView : View
{

    private ScoreViewModel _viewModel;

    
    [SerializeField] private ScoreUserItemView _userItemViewPrefab;
    [SerializeField] private RectTransform _userItemContainer;
    private List<ScoreUserItemView> _instantiatedScoreUserItems;

    public void SetViewModel(ScoreViewModel viewModel)
    {
        _instantiatedScoreUserItems = new List<ScoreUserItemView>();
        _viewModel = viewModel;

        _viewModel.ScoreUsers.ObserveAdd().Subscribe(InstantiateScoreUser).AddTo(_disposables);

        _viewModel.Position.Subscribe((position) =>
        {
            GetComponent<RectTransform>().DOAnchorPos(position, 1f, true);
        }).AddTo(_disposables);
    }

    private void InstantiateScoreUser(CollectionAddEvent<ScoreUserItemViewModel> scoreUserItemViewModel)
    {
        var scoreUserItemView = Instantiate(_userItemViewPrefab, _userItemContainer);
        scoreUserItemView.SetViewModel(scoreUserItemViewModel.Value);

        _instantiatedScoreUserItems.Add(scoreUserItemView);
    }
}