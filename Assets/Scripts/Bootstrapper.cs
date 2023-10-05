using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System;
using UnityEngine.InputSystem;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField]
    private JengaStack[] _stacks;

    [SerializeField]
    private PlayerInput _input;

    private void Awake()
    {
        CoreController.JengaStacks = _stacks;
        CoreController.Input = _input;

        CoreController.InitializeGame();
    }
}

public static class CoreController
{
    public static JengaStack[] JengaStacks;
    public static PlayerInput Input;

    public static Action OnInitializationComplete;

    private static readonly string _apiUri = @"https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

    public static void InitializeGame()
    {
        HttpService.GetJSON<List<BlockModel>>(_apiUri, InitializeStacks, NetworkError);
    }

    private static void InitializeStacks(List<BlockModel> data)
    {
        for (int i = 0; i < JengaStacks.Length; i++)
        {
            JengaStacks[i].Initialize(data);
        }

        OnInitializationComplete?.Invoke();
    }

    private static void NetworkError(HttpResponseMessage message)
    {
        Debug.LogError($"Error retrieving data: {message.StatusCode}");
    }
}
