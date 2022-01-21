using UnityEngine;
using System.Collections.Generic;
using System;
using UniRx;

public class MenuInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasParent;
    
    [SerializeField] private HomeView _homePrefab;
    [SerializeField] private ScoreView _scorePrefab;
    [SerializeField] private ConfigView _configPrefab;
    [SerializeField] private ButtonsView _buttonsPrefab;

    FirebaseLogService _firebaseLogService;
    LoadAllScoreUsersUseCase _loadAllScoreUsersUseCase;
    SendMessageUseCase _sendMessageUseCase;
    EditNameUseCase _editNameUseCase;

    private List<IDisposable> _disposables = new List<IDisposable>();

    private void Awake()
    {
        var homeView = Instantiate(_homePrefab, _canvasParent);
        var scoreView = Instantiate(_scorePrefab, _canvasParent);
        var configView = Instantiate(_configPrefab, _canvasParent);
        var buttonsView = Instantiate(_buttonsPrefab, _canvasParent);

        var homeViewModel = new HomeViewModel().AddTo(_disposables);
        var scoreViewModel = new ScoreViewModel().AddTo(_disposables);
        var configViewModel = new ConfigViewModel().AddTo(_disposables);
        var buttonsViewModel = new ButtonsViewModel().AddTo(_disposables);

        homeView.SetViewModel(homeViewModel);
        scoreView.SetViewModel(scoreViewModel);
        configView.SetViewModel(configViewModel);
        buttonsView.SetViewModel(buttonsViewModel);

        var eventDispatcherService = new EventDispatcherService();
        var firebaseStoreService = new FirebaseStoreService(eventDispatcherService);
        var firebaseRealtimeDatabaseService = new FirebaseRealtimeDatabaseService(eventDispatcherService);
        _firebaseLogService = new FirebaseLogService(eventDispatcherService);
        var firebaseMessageService = new FireBaseMessageService(eventDispatcherService);
        var sceneHandlerService = new UnitySceneHandler();
        var analyticsEventsService = new AnalyticsEventsService(eventDispatcherService);
        
        _editNameUseCase = new EditNameUseCase(firebaseStoreService, eventDispatcherService).AddTo(_disposables);
        var createAccountUseCase = new CreateAccountUseCase(eventDispatcherService, _firebaseLogService).AddTo(_disposables);
        var loginUseCase = new LoginUseCase(_firebaseLogService, eventDispatcherService).AddTo(_disposables);
        var logoutUseCase = new LogoutUseCase(eventDispatcherService, _firebaseLogService).AddTo(_disposables);
        _sendMessageUseCase = new SendMessageUseCase(firebaseMessageService, eventDispatcherService).AddTo(_disposables);
        var loadSceneUseCase = new LoadSceneUseCase(sceneHandlerService).AddTo(_disposables);
        var analyticsEventUseCase = new SendAnalyticsEventsUseCase(analyticsEventsService, eventDispatcherService).AddTo(_disposables);

        new HomeController(homeViewModel,_editNameUseCase,loadSceneUseCase,analyticsEventUseCase).AddTo(_disposables);
        new ConfigController(configViewModel, createAccountUseCase, loginUseCase, logoutUseCase, _sendMessageUseCase, _editNameUseCase).AddTo(_disposables);
        new ButtonsController(buttonsViewModel, homeViewModel, scoreViewModel, configViewModel).AddTo(_disposables);

        new HomePresenter(homeViewModel, eventDispatcherService).AddTo(_disposables);
        new ScorePresenter(scoreViewModel, eventDispatcherService).AddTo(_disposables);
        
        _loadAllScoreUsersUseCase = new LoadAllScoreUsersUseCase(firebaseRealtimeDatabaseService, eventDispatcherService).AddTo(_disposables);
    }

    private async void Start()
    {
        //await _firebaseLogService.InitByTask();
        await _loadAllScoreUsersUseCase.GetAll();
        //await _editNameUseCase.InitByTask(); //Poner esto en el init y cambiarlo por el nuevo set name de la use case
        _editNameUseCase.SetUserNameFromFirestore();
        await _sendMessageUseCase.InitByTask();
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}