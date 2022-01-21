using UniRx;
using UnityEngine;

public class HomeController : Controller
{
    private readonly HomeViewModel _viewModel;
    private readonly IEditNameUseCase _editNameUseCase;
    private readonly SceneLoader _sceneLoader;

    public HomeController(HomeViewModel viewModel, IEditNameUseCase editNameUseCase, SceneLoader sceneLoader)
    {
        _viewModel = viewModel;
        _editNameUseCase = editNameUseCase;
        _sceneLoader = sceneLoader;
        
        _viewModel.PlayButtonPressed.Subscribe((_) =>
        {
            _sceneLoader.Load("Game");
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