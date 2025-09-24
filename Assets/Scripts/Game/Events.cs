using UnityEngine;
using System;

public class Events
{
    public static event Action OnGameStart;

    public static void TriggerGameStart()
    {
        OnGameStart?.Invoke();
    }
}
