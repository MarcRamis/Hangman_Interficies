using UniRx;
using UnityEngine;

public class ConfigController : Controller
{
    private ConfigViewModel _viewModel;
    public  ConfigController(ConfigViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.LogButtonIsVisible.Value = true;

        _viewModel.AudioColor.Value = Green1();
        _viewModel.MessagesColor.Value = Green1();

        _viewModel.LoginButtonPressed.Subscribe((_) =>
        {
            _viewModel.LogButtonIsVisible.Value = false;
            _viewModel.LogTextIsVisible.Value = true;
        }).AddTo(_disposables);
        
        _viewModel.RegisterButtonPressed.Subscribe((_) =>
        {
            _viewModel.LogButtonIsVisible.Value = false;
            _viewModel.LogTextIsVisible.Value = true;
        }).AddTo(_disposables);
        
        _viewModel.SaveButtonPressed.Subscribe((_) =>
        {
            _viewModel.LogoutIsVisible.Value = true;
            _viewModel.LogTextIsVisible.Value = false;
        }).AddTo(_disposables);

        _viewModel.ExitButtonPressed.Subscribe((_) =>
        {
            _viewModel.LogButtonIsVisible.Value = true;
            _viewModel.LogTextIsVisible.Value = false;
        }).AddTo(_disposables);

        _viewModel.LogoutButtonPressed.Subscribe((_) =>
        {
            _viewModel.LogoutIsVisible.Value = false;
            _viewModel.LogButtonIsVisible.Value = true;
        }).AddTo(_disposables);

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