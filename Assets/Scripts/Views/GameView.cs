using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameView : View
{
    GameViewModel _viewModel;
    
    [SerializeField] private TMP_Text _hangmanText;
    [SerializeField] private RectTransform _loadRect;
    [SerializeField] private RectTransform _gameRect;
    [SerializeField] private RectTransform _endRect;
    [SerializeField] private RectTransform _pauseRect;

    [SerializeField] private Image load_image;
    
    // Letters
    [SerializeField] private GameCheckButtonView _checkButtonViewPrefab;
    [SerializeField] private RectTransform _checkButtonContainer;
    private List<GameCheckButtonView> _instantiatedGameCheckButtons;
    private IUpdateGameUseCase _updateGame;
    private IEventDispatcherService _eventDispatcher;

    // Lives
    [SerializeField] private Image[] _lives;

    // EndGame
    [SerializeField] private Image _victoryImage;
    [SerializeField] private Image _defeatImage;
    [SerializeField] private Image _timeImage;
    [SerializeField] private Image _scoreImage;
    [SerializeField] private TMP_Text _endText;
    [SerializeField] private TMP_Text _endTimeText;
    [SerializeField] private TMP_Text _endScoreText;
    [SerializeField] private TMP_Text _endMenuText;
    [SerializeField] private TMP_Text _endPlayAgainText;
    [SerializeField] private Button _endMenuButton;
    [SerializeField] private Button _endContinueButton;

    // Pause
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;

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

        _viewModel.EndRectIsVisible.Subscribe((gameVisible) =>
        {
            _endRect.gameObject.SetActive(gameVisible);
        }).AddTo(_disposables);

        _viewModel.PauseRectIsVisible.Subscribe((gameVisible) =>
        {
            _pauseRect.gameObject.SetActive(gameVisible);
        }).AddTo(_disposables);

        _viewModel.HangmanRandomNameText.Subscribe((randomName) =>
        {
            _hangmanText.text = randomName;
        }).AddTo(_disposables);

        OnSubscribeLives();
        OnEndPanel();
        OnPausePanel();

        InvokeRepeating("LoadAnimation", 0, 1.0f);
    }

    private void OnPausePanel()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            _viewModel.PauseButtonPressed.Execute();
        });
        _homeButton.onClick.AddListener(() =>
        {
            _viewModel.HomeButtonPressed.Execute();
        });
        _restartButton.onClick.AddListener(() =>
        {
            _viewModel.RestartButtonPressed.Execute();
        });
        _resumeButton.onClick.AddListener(() =>
        {
            _viewModel.ResumeButtonPressed.Execute();
        });
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

    private void OnEndPanel()
    {
        _viewModel.StateColor.Subscribe((color) =>
        {
            _endTimeText.color = color;
            _endScoreText.color = color;
            _endText.color = color;
            _endMenuText.color = color;
            _endPlayAgainText.color = color;
            _timeImage.color = color;
            _scoreImage.color = color;
        }).AddTo(_disposables);

        _viewModel.VictoryIsVisible.Subscribe((visible) =>
        {
            _victoryImage.gameObject.SetActive(visible);
            _defeatImage.gameObject.SetActive(!visible);
        }).AddTo(_disposables);

        _viewModel.TitleText.Subscribe((titleText) =>
        {
            _endText.text = titleText;
        }).AddTo(_disposables);

        _viewModel.TimeText.Subscribe((timeText) =>
        {
            _endTimeText.text = timeText;
        }).AddTo(_disposables);

        _viewModel.ScoreText.Subscribe((scoreText) =>
        {
            _endScoreText.text = scoreText;
        }).AddTo(_disposables);

        _endMenuButton.onClick.AddListener(() =>
        {
            _viewModel.MenuButtonPressed.Execute();
        });
        
        _endContinueButton.onClick.AddListener(() =>
        {
            _viewModel.ContinueButtonPressed.Execute();
        });
    }

    private void LoadAnimation()
    {
        load_image.transform.DORotate(new Vector3(0, 0, -360), 1, RotateMode.WorldAxisAdd);
    }
}
