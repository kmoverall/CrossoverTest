using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class HttpService
{
    private static readonly HttpClient _client;

    static HttpService()
    {
        _client = new HttpClient();
    }

    public static void GetJSON<T>(string uri, Action<T> callback, Action<HttpResponseMessage> failureCallback = null)
    {
        GetJSONAsync(uri, callback, failureCallback);
    }

    public static async void GetJSONAsync<T>(string uri, Action<T> callback, Action<HttpResponseMessage> failureCallback)
    {
        using HttpResponseMessage response = await _client.GetAsync(uri);

        if (response == null || !response.IsSuccessStatusCode)
        {
            failureCallback?.Invoke(response);
            return;
        }

        var json = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<T>(json);

        callback?.Invoke(result);
    }

}
