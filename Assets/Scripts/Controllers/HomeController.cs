using UniRx;
using UnityEngine;

public class HomeController : Controller
{
    private readonly HomeViewModel _viewModel;

    public HomeController(HomeViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.PlayButtonPressed.Subscribe((_) =>
        {
            Debug.Log("Go Play");
        }).AddTo(_disposables);

        _viewModel.ProfileButtonPressed.Subscribe((_) =>
        {
            _viewModel.UserNameIsVisible.Value = true;
        }).AddTo(_disposables);

        _viewModel.SaveUserNameButtonPressed.Subscribe((changeNameText) =>
        {
            // Use case que guarda el nombre en la firestore
            _viewModel.UserNameIsVisible.Value = false;
        }).AddTo(_disposables);
    }
}