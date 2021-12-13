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
        var userRepository = new UserNameLog;

        ServiceLocator.Instance.RegisterService<SceneHandlerService>(sceneHandlerService);

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

//public class UserRepository : UserDataAccess
//{

//}

public class UserEntity
{
    public readonly string UserId;
    public string Name { get; private set; }

    public UserEntity(string userid, string name)
    {
        UserId = userid;
        Name = name;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}