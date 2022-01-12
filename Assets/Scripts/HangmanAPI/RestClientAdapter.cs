using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RestClientAdapter
{
    private readonly HttpClient _client;

    public RestClientAdapter()
    {
        _client = new HttpClient();
    }

    public async Task<TResponse> Post<TRequest, TResponse>(string url, TRequest request)
        where TRequest : Request where TResponse : Response
    {
        const string jsonMediaType = "application/json";
        var data = new StringContent(JsonUtility.ToJson(request), Encoding.UTF8, jsonMediaType);
        var response = await _client.PostAsync(url, data);
        var contents = await response.Content.ReadAsStringAsync();

        return JsonUtility.FromJson<TResponse>(contents);
    }

    public async Task<TResponse> Get<TRequest, TResponse>(string url, TRequest request)
        where TRequest : Request where TResponse : Response
    {
        var uri = new Uri(url);
        var finalUri = uri.ExtendQuery(request);

        var response = await _client.GetAsync(finalUri);
        var contents = await response.Content.ReadAsStringAsync();

        return JsonUtility.FromJson<TResponse>(contents);
    }

    // This is not full REST but the API we are using need to send the parameters on the url
    public async Task<TResponse> PutWithParametersOnUrl<TRequest, TResponse>(string url, TRequest request)
        where TRequest : Request where TResponse : Response
    {
        var uri = new Uri(url);
        var finalUri = uri.ExtendQuery(request);

        var response = await _client.PutAsync(finalUri, null);
        var contents = await response.Content.ReadAsStringAsync();

        return JsonUtility.FromJson<TResponse>(contents);
    }
}