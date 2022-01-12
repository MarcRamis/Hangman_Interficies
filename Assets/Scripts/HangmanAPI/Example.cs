using System;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Example : MonoBehaviour
{
    [SerializeField] private Button _guessLetterButton;
    [SerializeField] private Button _getSolutionButton;
    [SerializeField] private TMP_InputField _inputField;

    [SerializeField] private TextMeshProUGUI _hangmanText;
    [SerializeField] private TextMeshProUGUI _tokenText;
    [SerializeField] private TextMeshProUGUI _correctLettersText;
    [SerializeField] private TextMeshProUGUI _incorrectLettersText;
    private string _token;
    private StringBuilder _correctLetters;
    private StringBuilder _incorrectLetters;
    private RestClientAdapter _restClientAdapter;

    private void Awake()
    {
        _restClientAdapter = new RestClientAdapter();
        _correctLetters = new StringBuilder();
        _incorrectLetters = new StringBuilder();

        _guessLetterButton.onClick.AddListener(GuessLetter);
        _getSolutionButton.onClick.AddListener(GetSolution);
    }

    private async void Start()
    {
        await StartGame();
    }

    private async Task StartGame()
    {
        var request = new NewGameRequest();
        var response = await _restClientAdapter.Post<NewGameRequest, NewGameResponse>(EndPoints.NewGame, request);
        UpdateToken(response.token);
        _hangmanText.SetText(AddSpacesBetweenLetters(response.hangman));
    }

    private void UpdateToken(string token)
    {
        _token = token;
        _tokenText.SetText(_token);
    }

    private static string AddSpacesBetweenLetters(string word)
    {
        return string.Join(" ", word.ToCharArray());
    }

    private async void GuessLetter()
    {
        var letter = _inputField.text;
        if (string.IsNullOrEmpty(letter))
        {
            Debug.LogError("Input text is null");
            return;
        }

        if (letter.Length > 1)
        {
            Debug.LogError("Only 1 letter");
            return;
        }

        var request = new GuessLetterRequest { letter = letter, token = _token };
        var response = await
            _restClientAdapter
                .PutWithParametersOnUrl<GuessLetterRequest, GuessLetterResponse>
                (
                    EndPoints.GuessLetter,
                    request
                );

        UpdateToken(response.token);
        SetGuessResponse(response, letter);
        if (IsCompleted(response.hangman))
        {
            Debug.Log("Complete");
        }
    }

    private void SetGuessResponse(GuessLetterResponse response, string letter)
    {
        if (response.correct)
        {
            _correctLetters.Append($" {letter}");
            _correctLettersText.SetText(_correctLetters.ToString());
        }
        else
        {
            _incorrectLetters.Append($" {letter}");
            _incorrectLettersText.SetText(_incorrectLetters.ToString());
        }

        _hangmanText.SetText(AddSpacesBetweenLetters(response.hangman));
    }

    private async void GetSolution()
    {
        var request = new GetSolutionRequest { token = _token };
        var response =
            await _restClientAdapter.Get<GetSolutionRequest, GetSolutionResponse>(EndPoints.GetSolution,
                request);

        UpdateToken(response.token);
        _hangmanText.SetText(response.solution);
    }

    private bool IsCompleted(string hangman)
    {
        const string secretCharacter = "_";
        return !hangman.Contains(secretCharacter);
    }
}