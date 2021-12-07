using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LoadAnim : MonoBehaviour
{
    [SerializeField] private Image loadCircle;

    // Start is called before the first frame update
    FirebaseLogService _firebaseLogService;

    private void Awake()
    {
        var eventDispatcher = new EventDispatcherService();
        _firebaseLogService = new FirebaseLogService(eventDispatcher);

        var doAuthUseCase = new DoAuthUseCase(_firebaseLogService, eventDispatcher);

        eventDispatcher.Subscribe<LogEvent>(ChangeScene);
    }

    void Start()
    {
        _firebaseLogService.Init();
        InvokeRepeating("LoadAnimation", 0, 1.5f);
    }

    private void LoadAnimation()
    {
        loadCircle.transform.DORotate(new Vector3(0, 0, -360), 1, RotateMode.WorldAxisAdd);
    }
    
    private void ChangeScene(LogEvent data)
    {
        Debug.Log(data.Text);
        SceneManager.LoadScene("Menu");
    }
}

public class InitView : View
{

    //public 
}

public class InitViewModel : ViewModel
{

}

public class InitController : Controller
{

}

public class InitPresenter : Presenter
{

}