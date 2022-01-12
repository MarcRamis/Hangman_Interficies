using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class GameView : View
{
    GameViewModel _viewModel;
    
    [SerializeField] private TMP_Text _hangmanText;
    [SerializeField] private RectTransform _loadRect;
    [SerializeField] private RectTransform _gameRect;

    [SerializeField] private GameCheckButtonView _checkButtonViewPrefab;
    [SerializeField] private RectTransform _checkButtonContainer;
    private List<GameCheckButtonView> _instantiatedGameCheckButtons;

    public void SetViewModel(GameViewModel viewModel)
    {
        _viewModel = viewModel;

        _instantiatedGameCheckButtons = new List<GameCheckButtonView>();
        _viewModel.CheckButton.ObserveAdd().Subscribe(InstantiateCheckButtons).AddTo(_disposables);

        _viewModel.LoadGameRectIsVisible.Subscribe((loadScreenVisible) =>
        {
            _loadRect.gameObject.SetActive(loadScreenVisible);
        }).AddTo(_disposables);

        _viewModel.GameRectIsVisible.Subscribe((gameVisible) =>
        {
            _gameRect.gameObject.SetActive(gameVisible);
        }).AddTo(_disposables);

        _viewModel.HangmanRandomNameText.Subscribe((randomName) =>
        {
            _hangmanText.text = randomName;
        }).AddTo(_disposables);

    }
    private void InstantiateCheckButtons(CollectionAddEvent<GameCheckButtonViewModel> gameCheckButtonViewModel)
    {
        var gameCheckButtonsView = Instantiate(_checkButtonViewPrefab, _checkButtonContainer);
        gameCheckButtonsView.SetViewModel(gameCheckButtonViewModel.Value);
        
        _instantiatedGameCheckButtons.Add(gameCheckButtonsView);
    }
}
