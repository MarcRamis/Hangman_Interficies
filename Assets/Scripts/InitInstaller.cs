using UnityEngine;
using System.Collections.Generic;
using System;
using UniRx;

public class InitInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasParent;

    [SerializeField] private InitView _initPrefab;
    //FirebaseLogService _firebaseLogService;
    LoadInitialDataUseCase _loadInitialDataUseCase;
    EditNameUseCase _editNameUseCase;

    private List<IDisposable> _disposables = new List<IDisposable>();

    private void Awake()
    {
        var initView = Instantiate(_initPrefab, _canvasParent);

        var initViewModel = new InitViewModel().AddTo(_disposables);
        initView.SetViewModel(initViewModel);

        var sceneHandlerService = new UnitySceneHandler();
        //var userRepository = new UserRepository();

        //ServiceLocator.Instance.RegisterService<SceneHandlerService>(sceneHandlerService);
        //ServiceLocator.Instance.RegisterService<UserDataAccess>(userRepository);

        var eventDispatcher = new EventDispatcherService();
        var firebaseStoreService = new FirebaseStoreService(eventDispatcher);
        var firebaseLogService = new FirebaseLogService(eventDispatcher);
        


        ServiceLocator.Instance.RegisterService<IFirebaseStoreService>(firebaseStoreService);
        ServiceLocator.Instance.RegisterService<IFirebaseLogService>(firebaseLogService);

        var loginUseCase = new LoginUseCase(ServiceLocator.Instance.GetService<IFirebaseLogService>(), eventDispatcher).AddTo(_disposables);
        var loadSceneUseCase = new LoadSceneUseCase(sceneHandlerService);
        _editNameUseCase = new EditNameUseCase(ServiceLocator.Instance.GetService<IFirebaseStoreService>(), eventDispatcher);
        _loadInitialDataUseCase = new LoadInitialDataUseCase(loadSceneUseCase);

        new InitController(initViewModel).AddTo(_disposables);
        new InitPresenter(initViewModel, eventDispatcher).AddTo(_disposables);
    }

    private async void Start()
    {
        var firebaseLogService = ServiceLocator.Instance.GetService<IFirebaseLogService>();
        await firebaseLogService.Init3();
        //await _editNameUseCase.Init1();
        await _loadInitialDataUseCase.Init();
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}