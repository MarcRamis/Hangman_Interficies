using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
using TMPro;

public class HomeView : View
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _profileButton;
    [SerializeField] private Button _saveUserNameButton;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_Text _userNameText;
    
    private HomeViewModel _viewModel;

    public void SetViewModel(HomeViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.Position.Subscribe((position) =>
        {
            GetComponent<RectTransform>().DOAnchorPos(position, 1f, true);
        }).AddTo(_disposables);

        _viewModel.UserNameIsVisible.Subscribe((changeUserNameIsVisible) => {
            _inputField.gameObject.SetActive(changeUserNameIsVisible);
        }).AddTo(_disposables);

        _viewModel.ChangeName.Subscribe(changeName =>
        {
            _inputField.SetTextWithoutNotify(changeName);
        }).AddTo(_disposables);

        _viewModel.UserNameText.Subscribe((nameText) =>
        {
            _userNameText.SetText(nameText);
        }).AddTo(_disposables);



        _profileButton.onClick.AddListener(() =>
        {
            _viewModel.ProfileButtonPressed.Execute();
        });

        _playButton.onClick.AddListener(() =>
        {
            _viewModel.PlayButtonPressed.Execute();
        });

        _saveUserNameButton.onClick.AddListener(() =>
        {
            _viewModel.SaveUserNameButtonPressed.Execute(_inputField.text);
        });
    }
}
