using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameView : View
{
    GameViewModel _viewModel;
    
    [SerializeField] private TMP_Text _hangmanText;
    [SerializeField] private RectTransform _loadRect;
    [SerializeField] private RectTransform _gameRect;
    
    // Buttons
    [SerializeField] private GameCheckButtonView _checkButtonViewPrefab;
    [SerializeField] private RectTransform _checkButtonContainer;
    private List<GameCheckButtonView> _instantiatedGameCheckButtons;
    private IUpdateGameUseCase _updateGame;
    private IEventDispatcherService _eventDispatcher;

    // Lives
    [SerializeField] private Image[] _lives;

    public void SetViewModel(GameViewModel viewModel, IUpdateGameUseCase updateGame, IEventDispatcherService eventDispatcher)
    {
        _viewModel = viewModel;
        _updateGame = updateGame;
        _eventDispatcher = eventDispatcher;

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

        OnSubscribeLives();
    }

    private void InstantiateCheckButtons(CollectionAddEvent<GameCheckButtonViewModel> gameCheckButtonViewModel)
    {
        var gameCheckButtonsView = Instantiate(_checkButtonViewPrefab, _checkButtonContainer);
        gameCheckButtonsView.SetViewModel(gameCheckButtonViewModel.Value);
        
        new GameCheckButtonController(gameCheckButtonViewModel.Value, _updateGame);
        new GameCheckButtonPresenter(gameCheckButtonViewModel.Value, _eventDispatcher);

        _instantiatedGameCheckButtons.Add(gameCheckButtonsView);
    }
    private void OnSubscribeLives()
    {
        _viewModel.Live1Visible.Subscribe((liveVisible) =>
        {
            _lives[0].gameObject.SetActive(liveVisible);
        }).AddTo(_disposables);

        _viewModel.Live2Visible.Subscribe((liveVisible) =>
        {
            _lives[1].gameObject.SetActive(liveVisible);
        }).AddTo(_disposables);

        _viewModel.Live3Visible.Subscribe((liveVisible) =>
        {
            _lives[2].gameObject.SetActive(liveVisible);
        }).AddTo(_disposables);

        _viewModel.Live4Visible.Subscribe((liveVisible) =>
        {
            _lives[3].gameObject.SetActive(liveVisible);
        }).AddTo(_disposables);

        _viewModel.Live5Visible.Subscribe((liveVisible) =>
        {
            _lives[4].gameObject.SetActive(liveVisible);
        }).AddTo(_disposables);

        _viewModel.Live6Visible.Subscribe((liveVisible) =>
        {
            _lives[5].gameObject.SetActive(liveVisible);
        }).AddTo(_disposables);

        _viewModel.Live7Visible.Subscribe((liveVisible) =>
        {
            _lives[6].gameObject.SetActive(liveVisible);
        }).AddTo(_disposables);

    }
}
