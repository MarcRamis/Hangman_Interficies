using UniRx;
using UnityEngine;

public class HomeController : Controller
{
    private readonly HomeViewModel _viewModel;
    private readonly IEditNameUseCase _editNameUseCase;

    public HomeController(HomeViewModel viewModel, IEditNameUseCase editNameUseCase)
    {
        _viewModel = viewModel;
        _editNameUseCase = editNameUseCase;

        editNameUseCase.SetUserNameFromFirestore();

        _viewModel.PlayButtonPressed.Subscribe((_) =>
        {
            Debug.Log("Go Play");
        }).AddTo(_disposables);

        _viewModel.ProfileButtonPressed.Subscribe((_) =>
        {
            _viewModel.ChangeName.SetValueAndForceNotify(string.Empty);
            _viewModel.UserNameIsVisible.Value = true;
        }).AddTo(_disposables);
        
        _viewModel.SaveUserNameButtonPressed.Subscribe((changeNameText) =>
        {
            _editNameUseCase.Edit(changeNameText);
            // Use case que cambia el nombre en la firestore
            _viewModel.UserNameIsVisible.Value = false;
        }).AddTo(_disposables);
    }
}