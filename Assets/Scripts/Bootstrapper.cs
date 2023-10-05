using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField]
    private JengaStack[] _stacks;

    private string _apiUri = @"https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

    public static Action OnInitializationComplete;

    private void Start()
    {
        HttpService.GetJSON<List<BlockModel>>(_apiUri, InitializeStacks, NetworkError);
    }

    private void InitializeStacks(List<BlockModel> data)
    {
        for (int i = 0; i < _stacks.Length; i++)
        {
            _stacks[i].Initialize(data);
        }

        OnInitializationComplete?.Invoke();
    }

    private void NetworkError(HttpResponseMessage message)
    {
        Debug.LogError($"Error retrieving data: {message.StatusCode}");
    }
}
