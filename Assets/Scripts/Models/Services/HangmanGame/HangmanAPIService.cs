using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HangmanAPIService : IHangmanAPIService
{
    IEventDispatcherService _eventDispatcher;

    private string _token;
    private StringBuilder _correctLetters;
    private StringBuilder _incorrectLetters;
    private HangmanClient _hangmanClient;

    public HangmanAPIService(IEventDispatcherService eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;

        _hangmanClient = new HangmanClient();
        _correctLetters = new StringBuilder();
        _incorrectLetters = new StringBuilder();
    }
    
    public async Task GetRandomLetter()
    {
        var response = await _hangmanClient
            .StartGame<NewGameResponse>(EndPoints.NewGame);
        UpdateToken(response.token);
        _eventDispatcher.Dispatch(new HangmanRandomNameEvent(AddSpacesBetweenLetters(response.hangman)));

        GetSolution();
    }
    
    public void GetButtonLetters()
    {
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        foreach (char letter in alphabet)
        {
            _eventDispatcher.Dispatch(new CheckButtonPrefs(letter.ToString()));
        }
    }
    public async void GuessLetter(string letter)
    {
        var response = await
            _hangmanClient.GuessLetter<GuessLetterResponse>
                (EndPoints.GuessLetter, _token, letter);

        UpdateToken(response.token);
        SetGuessResponse(response, letter);
        if (IsCompleted(response.hangman))
        {
            _eventDispatcher.Dispatch(new PlayerHasWonEvent());
        }
    }

    private void SetGuessResponse(GuessLetterResponse response, string letter)
    {
        if (response.correct)
        {
            _correctLetters.Append($" {letter}");
            _eventDispatcher.Dispatch(new ButtonCheckedEvent(true, letter));
            _eventDispatcher.Dispatch(new VaryScoreEvent(1));
        }
        else
        {
            _incorrectLetters.Append($" {letter}");
            _eventDispatcher.Dispatch(new ButtonCheckedEvent(false, letter));
        }

        _eventDispatcher.Dispatch(new HangmanRandomNameEvent(AddSpacesBetweenLetters(response.hangman)));
    }

    public async void GetSolution()
    {
        var request = new GetSolutionRequest { token = _token };
        var response =
            await _hangmanClient
                .GetSolution<GetSolutionResponse>(EndPoints.GetSolution,
                    _token);

        Debug.Log(response.solution);
    }

    private static string AddSpacesBetweenLetters(string word)
    {
        return string.Join(" ", word.ToCharArray());
    }

    private void UpdateToken(string token)
    {
        _token = token;
    }

    private bool IsCompleted(string hangman)
    {
        const string secretCharacter = "_";
        return !hangman.Contains(secretCharacter);
    }
}
