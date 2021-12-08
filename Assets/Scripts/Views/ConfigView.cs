using UniRx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ConfigView : View
{                   
    [SerializeField] private Button _registerButton;
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _logoutButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _audioButton;
    [SerializeField] private Button _messagesButton;

    [SerializeField] private TMP_InputField _emailInputField;
    [SerializeField] private TMP_InputField _passwordInputField;

    [SerializeField] private Image _audioImage;
    [SerializeField] private Image _messagesImage;

    private ConfigViewModel _viewModel;

    public void SetViewModel(ConfigViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.Position.Subscribe((position) => {
            GetComponent<RectTransform>().DOAnchorPos(position, 1f, true);
        }).AddTo(_disposables);


        _viewModel.LogButtonIsVisible.Subscribe((isVisible) =>
        {
            _registerButton.gameObject.SetActive(isVisible);
            _loginButton.gameObject.SetActive(isVisible);
        }).AddTo(_disposables);

        _viewModel.LogoutIsVisible.Subscribe((isVisible) =>
        {
            _logoutButton.gameObject.SetActive(isVisible);
        }).AddTo(_disposables);

        _viewModel.LogTextIsVisible.Subscribe((isVisible) =>
        {
            _emailInputField.gameObject.SetActive(isVisible);
            _passwordInputField.gameObject.SetActive(isVisible);
            _saveButton.gameObject.SetActive(isVisible);
            _exitButton.gameObject.SetActive(isVisible);
        }).AddTo(_disposables);


        _viewModel.EmailText.Subscribe((textEmail) =>
        {
            _emailInputField.SetTextWithoutNotify(textEmail);
        }).AddTo(_disposables);
        
        _viewModel.PasswordText.Subscribe((textPassword) =>
        {
            _passwordInputField.SetTextWithoutNotify(textPassword);
        }).AddTo(_disposables);
        
        _viewModel.AudioColor.Subscribe((newColor) =>
        {
            _audioImage.DOColor(newColor, 0.5f);
        }).AddTo(_disposables);

        _viewModel.MessagesColor.Subscribe((newColor) =>
        {
            _messagesImage.DOColor(newColor, 0.5f);
        }).AddTo(_disposables);

        ButtonsExecute();
    }

    private void ButtonsExecute()
    {
        _registerButton.onClick.AddListener(() =>
        {
            _viewModel.RegisterButtonPressed.Execute();
        });

        _loginButton.onClick.AddListener(() =>
        {
            _viewModel.LoginButtonPressed.Execute();
        });

        _saveButton.onClick.AddListener(() =>
        {
            _viewModel.SaveButtonPressed.Execute(new UserNameLogEvent(_emailInputField.text, _passwordInputField.text));
        });

        _exitButton.onClick.AddListener(() =>
        {
            _viewModel.ExitButtonPressed.Execute();
        });

        _logoutButton.onClick.AddListener(() =>
        {
            _viewModel.LogoutButtonPressed.Execute();
        });

        _audioButton.onClick.AddListener(() =>
        {
            _viewModel.AudioButtonPressed.Execute();
        });

        _messagesButton.onClick.AddListener(() =>
        {
            _viewModel.MessagesButtonPressed.Execute();
        });

    }
}