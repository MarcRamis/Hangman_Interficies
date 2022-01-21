using UnityEngine;
using System.Collections.Generic;
using System;
using UniRx;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasParent;

    [SerializeField] private GameView _gamePrefab;

    private List<IDisposable> _disposables = new List<IDisposable>();
    StartGameUseCase _startGameUseCase;

    private void Awake()
    {
        var gameView = Instantiate(_gamePrefab, _canvasParent);

        var gameViewModel = new GameViewModel().AddTo(_disposables);

        var eventDispatcher = new EventDispatcherService();
        var hangmanService = new HangmanAPIService(eventDispatcher);
        var sceneHandlerService = new UnitySceneHandler();
        var googleMobileAdsService = new GoogleMobileAdsService();
        var realtimeDatabase = new FirebaseRealtimeDatabaseService(eventDispatcher);
        var analyticsEventsService = new AnalyticsEventsService(eventDispatcher);

        var _updateGameUseCase = new UpdateGameUseCase(eventDispatcher, hangmanService, googleMobileAdsService, analyticsEventsService, realtimeDatabase);
        var loadSceneUseCase = new LoadSceneUseCase(sceneHandlerService);
        var analyticsEventUseCase = new SendAnalyticsEventsUseCase(analyticsEventsService, eventDispatcher);

        gameView.SetViewModel(gameViewModel, _updateGameUseCase, eventDispatcher);

        _startGameUseCase = new StartGameUseCase(eventDispatcher, hangmanService, googleMobileAdsService);

        new GameController(gameViewModel, _updateGameUseCase, loadSceneUseCase, analyticsEventUseCase).AddTo(_disposables);
        new GamePresenter(gameViewModel, eventDispatcher).AddTo(_disposables);
    }

    private async void Start()
    {
        await _startGameUseCase.StartGame();
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}