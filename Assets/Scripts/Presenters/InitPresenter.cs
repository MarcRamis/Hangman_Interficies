using UnityEngine;
using UnityEngine.SceneManagement;

public class InitPresenter : Presenter
{
    IEventDispatcherService _eventDispatcher;
    InitViewModel _viewModel;

    public InitPresenter(InitViewModel viewModel, EventDispatcherService eventDispatcher)
    {
        _viewModel = viewModel;
        _eventDispatcher = eventDispatcher;

        //_eventDispatcher.Subscribe<LogEvent>(ChangeScene);
    }

    private void ChangeScene(LogEvent data)
    {
        Debug.Log(data.Text);
        SceneManager.LoadScene("Menu");
    }
}
