using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HangmanClient
{
    private readonly HttpClient _client;

    public HangmanClient()
    {
        _client = new HttpClient();
    }

    public async Task<TResponse> StartGame<TResponse>(string url)
        where TResponse : Response
    {
        const string jsonMediaType = "application/json";
        var response = await _client.PostAsync(url, null);
        var contents = await response.Content.ReadAsStringAsync();

        return JsonUtility.FromJson<TResponse>(contents);
    }

    public async Task<TResponse> GetSolution<TResponse>(string url, string token)
        where TResponse : Response
    {
        var uri = new Uri($"{url}?token={token}");

        var response = await _client.GetAsync(uri);
        var contents = await response.Content.ReadAsStringAsync();

        return JsonUtility.FromJson<TResponse>(contents);
    }

    public async Task<TResponse> GuessLetter<TResponse>(string url, string token, string letter)
        where TResponse : Response
    {
        var uri = new Uri($"{url}?token={token}&letter={letter}");

        var response = await _client.PutAsync(uri, null);
        var contents = await response.Content.ReadAsStringAsync();

        return JsonUtility.FromJson<TResponse>(contents);
    }
}