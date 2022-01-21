using UniRx;
using UnityEngine;

public class HomeController : Controller
{
    private readonly HomeViewModel _viewModel;
    private readonly IEditNameUseCase _editNameUseCase;
    private readonly SceneLoader _sceneLoader;
    private readonly ISendAnalyticsEventsUseCase _sendAnalyticsUseCase;

    public HomeController(HomeViewModel viewModel, IEditNameUseCase editNameUseCase, SceneLoader sceneLoader, ISendAnalyticsEventsUseCase sendAnalyticsUseCase)
    {
        _viewModel = viewModel;
        _editNameUseCase = editNameUseCase;
        _sceneLoader = sceneLoader;
        
        _sendAnalyticsUseCase = sendAnalyticsUseCase;

        _viewModel.PlayButtonPressed.Subscribe((_) =>
        {
            _sceneLoader.Load("Game");
            Debug.Log("Go Play");

            _sendAnalyticsUseCase.SendLevelStart(0);

        }).AddTo(_disposables);

        _viewModel.ProfileButtonPressed.Subscribe((_) =>
        {
            _viewModel.ChangeName.SetValueAndForceNotify(string.Empty);
            _viewModel.UserNameIsVisible.Value = true;
        }).AddTo(_disposables);
        
        _viewModel.SaveUserNameButtonPressed.Subscribe((changeNameText) =>
        {
            _editNameUseCase.Edit(changeNameText);
            _viewModel.UserNameIsVisible.Value = false;
        }).AddTo(_disposables);
    }
}