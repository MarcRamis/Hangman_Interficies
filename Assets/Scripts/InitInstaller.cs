using UnityEngine;

public class InitInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasParent;

    [SerializeField] private InitView _initPrefab;
    FirebaseLogService _firebaseLogService;
    LoadInitialDataUseCase _loadInitialDataUseCase;

    private void Awake()
    {
        var initView = Instantiate(_initPrefab, _canvasParent);

        var initViewModel = new InitViewModel();
        initView.SetViewModel(initViewModel);

        var sceneHandlerService = new UnitySceneHandler();
        var userRepository = new UserRepository();

        ServiceLocator.Instance.RegisterService<SceneHandlerService>(sceneHandlerService);
        ServiceLocator.Instance.RegisterService<UserDataAccess>(userRepository);

        var eventDispatcher = new EventDispatcherService();
        _firebaseLogService = new FirebaseLogService(eventDispatcher);

        var loginUseCase = new LoginUseCase(_firebaseLogService, eventDispatcher);
        var loadSceneUseCase = new LoadSceneUseCase(sceneHandlerService);

        _loadInitialDataUseCase = new LoadInitialDataUseCase(loadSceneUseCase);

        new InitController(initViewModel);
        new InitPresenter(initViewModel, eventDispatcher);
    }

    private void Start()
    {
        _firebaseLogService.Init();
        _loadInitialDataUseCase.Init();
    }
}