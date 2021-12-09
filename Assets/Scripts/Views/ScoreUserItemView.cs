using UniRx;
using UnityEngine;
using TMPro;

public class ScoreUserItemView : View
{
    ScoreUserItemViewModel _viewModel;
    [SerializeField] private TMP_Text _position;
    [SerializeField] private TMP_Text _userName;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _time;

    public void SetViewModel(ScoreUserItemViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.Position.Subscribe((position) =>
        {
            _position.text = position;
        }).AddTo(_disposables);

        _viewModel.UserName.Subscribe((userName) =>
        {
            _userName.text = userName;
        }).AddTo(_disposables);

        _viewModel.Score.Subscribe((score) =>
        {
            _score.text = score;
        }).AddTo(_disposables);

        _viewModel.Time.Subscribe((time) =>
        {
            _time.text = time;
        }).AddTo(_disposables);
    }
}
