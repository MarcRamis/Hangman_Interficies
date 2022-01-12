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
    private RestClientAdapter _restClientAdapter;

    public HangmanAPIService(IEventDispatcherService eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
        
        _restClientAdapter = new RestClientAdapter();
        _correctLetters = new StringBuilder();
        _incorrectLetters = new StringBuilder();
    }
    
    public async Task GetRandomLetter()
    {
        var request = new NewGameRequest();
        var response = await _restClientAdapter.Post<NewGameRequest, NewGameResponse>(EndPoints.NewGame, request);
        UpdateToken(response.token);
        _eventDispatcher.Dispatch(new HangmanRandomNameEvent(AddSpacesBetweenLetters(response.hangman)));
    }
    
    public void GetButtonLetters()
    {
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        foreach (char letter in alphabet)
        {
            _eventDispatcher.Dispatch(new CheckButtonPrefs(letter.ToString()));
        }
    }

    private static string AddSpacesBetweenLetters(string word)
    {
        return string.Join(" ", word.ToCharArray());
    }

    private void UpdateToken(string token)
    {
        _token = token;
    }
}