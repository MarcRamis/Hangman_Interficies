using UniRx;
using UnityEngine;

public class HomeController : Controller
{
    private readonly HomeViewModel _viewModel;
    private readonly IEditNameUseCase _editNameUseCase;
    private readonly IFireBaseAnalyticsService _fireBaseAnalytics;

    public HomeController(HomeViewModel viewModel, IEditNameUseCase editNameUseCase, IFireBaseAnalyticsService fireBaseAnalytics)
    {
        _viewModel = viewModel;
        _editNameUseCase = editNameUseCase;
        _fireBaseAnalytics = fireBaseAnalytics;

        editNameUseCase.SetUserNameFromFirestore();

        _viewModel.PlayButtonPressed.Subscribe((_) =>
        {
            Debug.Log("Go Play");
            _fireBaseAnalytics.StartLevel();
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