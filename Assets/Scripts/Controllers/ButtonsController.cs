using UnityEngine;
using UniRx;

public class ButtonsController : Controller
{
    private readonly ButtonsViewModel _viewModel;

    private readonly HomeViewModel _homeViewModel;
    private readonly ScoreViewModel _scoreViewModel;
    private readonly ConfigViewModel _configViewModel;

    public ButtonsController(ButtonsViewModel viewModel, HomeViewModel homeViewModel, ScoreViewModel scoreViewModel, ConfigViewModel configViewModel)
    {
        _viewModel = viewModel;
        _homeViewModel = homeViewModel;
        _scoreViewModel = scoreViewModel;
        _configViewModel = configViewModel;

        _homeViewModel.Position.Value = new Vector2(0f, 0f);
        _scoreViewModel.Position.Value = new Vector2(1440f, 0f);
        _configViewModel.Position.Value = new Vector2(1440f, 0f);

        _viewModel.HomeButtonPressed.Subscribe((_) =>
        {
            _homeViewModel.Position.Value = new Vector2(0, 0f);
            _scoreViewModel.Position.Value = new Vector2(1440f, 0f);
            _configViewModel.Position.Value = new Vector2(1440f, 0f);
        });
        
        _viewModel.ScoreButtonPressed.Subscribe((_) =>
        {
            _homeViewModel.Position.Value = new Vector2(-1440f, 0f);
            _scoreViewModel.Position.Value = new Vector2(0f, 0f);
            _configViewModel.Position.Value = new Vector2(1440f, 0f);
        });
        
        _viewModel.ConfigButtonPressed.Subscribe((_) =>
        {
            _homeViewModel.Position.Value = new Vector2(-1440f, 0f);
            _scoreViewModel.Position.Value = new Vector2(-1440f, 0f);
            _configViewModel.Position.Value = new Vector2(0f, 0f);
        });
    }
}