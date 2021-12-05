using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class HomeView : View
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _profileButton;
    [SerializeField] private GameObject _popupChangeUserName;
    
    private HomeViewModel _viewModel;

    public void SetViewModel(HomeViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.changeUserNameIsVisible.Subscribe((isVisible) =>{
            _popupChangeUserName.SetActive(isVisible);
            }).AddTo(_disposables);

        _profileButton.onClick.AddListener(() =>
        {
            _viewModel.ChangeUserNameButtonPressed.Execute();
        });

        _playButton.onClick.AddListener(() =>
        {
            _viewModel.ChangeUserNameButtonPressed.Execute();
        });
    }
}
