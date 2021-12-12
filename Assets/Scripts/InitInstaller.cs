using UnityEngine;

public class InitInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasParent;

    [SerializeField] private InitView _initPrefab;
    FirebaseLogService _firebaseLogService;

    private void Awake()
    {
        var initView = Instantiate(_initPrefab, _canvasParent);

        var initViewModel = new InitViewModel();
        initView.SetViewModel(initViewModel);

        var eventDispatcher = new EventDispatcherService();
        _firebaseLogService = new FirebaseLogService(eventDispatcher);

        var loginUseCase = new LoginUseCase(_firebaseLogService, eventDispatcher);

        new InitController(initViewModel);
        new InitPresenter(initViewModel, eventDispatcher);
    }

    private void Start()
    {
        _firebaseLogService.Init();
    }
}
