using UniRx;
using UnityEngine;

public class ConfigController : Controller
{
    private ConfigViewModel _viewModel;
    private ICreateAccountUseCase _createAccountUseCase;
    private ILoginUseCase _doAuthUseCase;
    private ILogoutUseCase _logoutUseCase;

    public  ConfigController(ConfigViewModel viewModel, ICreateAccountUseCase createAccountUseCase, ILoginUseCase doAuthUseCase, ILogoutUseCase logoutUseCase)
    {
        _viewModel = viewModel;
        _createAccountUseCase = createAccountUseCase;
        _doAuthUseCase = doAuthUseCase;
        _logoutUseCase = logoutUseCase;

        _viewModel.LogButtonIsVisible.Value = true;
        _viewModel.AudioColor.Value = Green1();
        _viewModel.MessagesColor.Value = Green1();

        LogController();

        AudioController();
        MessageController();
    }

    private void MessageController()
    {
        _viewModel.MessagesButtonPressed.Subscribe((_) =>
        {
            if (_viewModel.MessagesColor.Value == Red1())
            {
                _viewModel.MessagesColor.Value = Green1();
            }
            else
            {
                _viewModel.MessagesColor.Value = Red1();
            }
        }).AddTo(_disposables);
    }

    private void AudioController()
    {
        _viewModel.AudioButtonPressed.Subscribe((_) =>
        {
            if (_viewModel.AudioColor.Value == Red1())
            {
                _viewModel.AudioColor.Value = Green1();
            }
            else
            {
                _viewModel.AudioColor.Value = Red1();
            }
        }).AddTo(_disposables);
    }

    private void LogController()
    {
        _viewModel.LoginButtonPressed.Subscribe((_) =>
        {
            _viewModel.EmailText.SetValueAndForceNotify(string.Empty);
            _viewModel.PasswordText.SetValueAndForceNotify(string.Empty);

            _viewModel.LogButtonIsVisible.Value = false;
            _viewModel.LogTextIsVisible.Value = true;

            _viewModel.IsRegister.Value = false;
            _viewModel.IsLogin.Value = true;
        }).AddTo(_disposables);

        _viewModel.RegisterButtonPressed.Subscribe((_) =>
        {
            _viewModel.EmailText.SetValueAndForceNotify(string.Empty);
            _viewModel.PasswordText.SetValueAndForceNotify(string.Empty);

            _viewModel.LogButtonIsVisible.Value = false;
            _viewModel.LogTextIsVisible.Value = true;

            _viewModel.IsRegister.Value = true;
            _viewModel.IsLogin.Value = false;
        }).AddTo(_disposables);

        _viewModel.SaveButtonPressed.Subscribe((userNameLog) =>
        {
            _viewModel.LogoutIsVisible.Value = true;
            _viewModel.LogTextIsVisible.Value = false;

            if (_viewModel.IsRegister.Value)
            {
                _createAccountUseCase.Register(userNameLog);
            }
            else if (_viewModel.IsLogin.Value)
            {
                _doAuthUseCase.LoginEmail(userNameLog);
            }

        }).AddTo(_disposables);

        _viewModel.ExitButtonPressed.Subscribe((_) =>
        {
            _viewModel.LogButtonIsVisible.Value = true;
            _viewModel.LogTextIsVisible.Value = false;
        }).AddTo(_disposables);


        _viewModel.LogoutButtonPressed.Subscribe((_) =>
        {
            _logoutUseCase.Logout();

            _viewModel.LogoutIsVisible.Value = false;
            _viewModel.LogButtonIsVisible.Value = true;
        }).AddTo(_disposables);
    }

    Color Green1()
    {
        return Convert(0f, 200f, 0f);
    }
    Color Red1()
    {
        return Convert(200f, 0f, 0f);
    }

    Color Convert(float r, float g, float b)
    {
        return new Color(r/255f, g/255f, b/255f);
    }
}
