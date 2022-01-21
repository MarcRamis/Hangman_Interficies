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
        var googleMobileAdsServoce = new GoogleMobileAdsService();
        var realtimeDatabase = new FirebaseRealtimeDatabaseService(eventDispatcher);
        var analyticsEventsService = new AnalyticsEventsService(eventDispatcher);

        var updateGameUseCase = new UpdateGameUseCase(eventDispatcher, hangmanService, googleMobileAdsServoce, analyticsEventsService/*, realtimeDatabase*/);
        var loadSceneUseCase = new LoadSceneUseCase(sceneHandlerService);
        var analyticsEventUseCase = new SendAnalyticsEventsUseCase(analyticsEventsService, eventDispatcher);

        gameView.SetViewModel(gameViewModel, updateGameUseCase, eventDispatcher);

        _startGameUseCase = new StartGameUseCase(eventDispatcher, hangmanService);

        new GameController(gameViewModel,updateGameUseCase,loadSceneUseCase, analyticsEventUseCase).AddTo(_disposables);
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