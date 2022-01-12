using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class GameCheckButtonView : View
{
    [SerializeField] private Button _checkButton;
    [SerializeField] private TMP_Text _checkButtonText;
    [SerializeField] private Image _checkButtonImage;

    [SerializeField] private Texture2D _correctButtonTexture;
    [SerializeField] private Texture2D _incorrectButtonTexture;
    
    GameCheckButtonViewModel _viewModel;
    public void SetViewModel(GameCheckButtonViewModel viewModel)
    {
        _viewModel = viewModel;
    
        _viewModel.CheckButtonText.Subscribe((letter) =>
        {
            _checkButtonText.text = letter;
        }).AddTo(_disposables);
    }
}
