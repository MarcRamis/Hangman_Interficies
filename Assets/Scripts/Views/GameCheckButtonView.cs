using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class GameCheckButtonView : View
{
    [SerializeField] private Button _checkButton;
    [SerializeField] private TMP_Text _checkButtonText;
    [SerializeField] private Image _checkButtonImage;
    
    [SerializeField] private Sprite _correctButtonSprite;
    [SerializeField] private Sprite _incorrectButtonSprite;
    
    GameCheckButtonViewModel _viewModel;
    public void SetViewModel(GameCheckButtonViewModel viewModel)
    {
        _viewModel = viewModel;
    
        _viewModel.CheckButtonText.Subscribe((letter) =>
        {
            _checkButtonText.text = letter;
        }).AddTo(_disposables);

        _viewModel.CheckButtonImage.Subscribe((checkedImage) =>
        {
            _checkButtonImage.sprite = checkedImage;
        }).AddTo(_disposables);

        _checkButton.onClick.AddListener(() =>
        {
            _viewModel.CheckButtonPressed.Execute();
        });

        // ESTO ESTÁ MAL FIJO
        _viewModel.CorrectLetter.Value = _correctButtonSprite;
        _viewModel.IncorrectLetter.Value = _incorrectButtonSprite;
    }
}
