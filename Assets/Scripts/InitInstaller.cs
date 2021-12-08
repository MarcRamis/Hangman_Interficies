using UnityEngine;

public class InitInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasParent;

    [SerializeField] private InitView _initPrefab;
    FirebaseLogService _firebaseLogService;

    Firebase.FirebaseApp app;

    private void Awake()
    {
        var initView = Instantiate(_initPrefab, _canvasParent);

        var initViewModel = new InitViewModel();
        initView.SetViewModel(initViewModel);

        var eventDispatcher = new EventDispatcherService();
        _firebaseLogService = new FirebaseLogService(eventDispatcher);

        var doAuthUseCase = new DoAuthUseCase(_firebaseLogService, eventDispatcher);

        new InitController(initViewModel);
        new InitPresenter(initViewModel, eventDispatcher);
    }

    private void Start()
    {
        _firebaseLogService.Init();

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
}
