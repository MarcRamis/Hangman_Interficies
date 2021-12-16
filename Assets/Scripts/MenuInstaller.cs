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

        _editNameUseCase = new EditNameUseCase(firebaseStoreService, eventDispatcherService);
        var createAccountUseCase = new CreateAccountUseCase(eventDispatcherService, _firebaseLogService);
        var loginUseCase = new LoginUseCase(_firebaseLogService, eventDispatcherService).AddTo(_disposables);
        var logoutUseCase = new LogoutUseCase(eventDispatcherService, _firebaseLogService);
        _sendMessageUseCase = new SendMessageUseCase(firebaseMessageService, eventDispatcherService);

        new HomeController(homeViewModel,_editNameUseCase).AddTo(_disposables);
        new ConfigController(configViewModel, createAccountUseCase, loginUseCase, logoutUseCase, _sendMessageUseCase, _editNameUseCase).AddTo(_disposables);
        new ButtonsController(buttonsViewModel, homeViewModel, scoreViewModel, configViewModel).AddTo(_disposables);

        new HomePresenter(homeViewModel, eventDispatcherService).AddTo(_disposables);
        new ScorePresenter(scoreViewModel, eventDispatcherService).AddTo(_disposables);

        _loadAllScoreUsersUseCase = new LoadAllScoreUsersUseCase(firebaseRealtimeDatabaseService, eventDispatcherService);
    }

    private void Start()
    {
        StartCoroutine(_firebaseLogService.Init(0.5f));

        StartCoroutine(_loadAllScoreUsersUseCase.GetAll(1.0f));
        StartCoroutine(_editNameUseCase.Init(1.0f));
        StartCoroutine(_sendMessageUseCase.Init(1.0f));
        
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}