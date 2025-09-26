using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Events
{
    public static event Action OnGameStart;
    public static event Action OnWin;
    public static event Action OnLoss;
    public static event Action OnGameOver;

    public static void TriggerGameStart()
    {
        OnGameStart?.Invoke();
    }

    public static void TriggerWin()
    {
        OnWin?.Invoke();
        SceneManager.LoadScene("WinScene", LoadSceneMode.Additive);
        Time.timeScale = 0f;
    }

    public static void TriggerLoss()
    {
        OnLoss?.Invoke();
        Time.timeScale = 0f;
    }

    public static void TriggerGameEnd()
    {
        OnGameOver?.Invoke();
        Time.timeScale = 1f;
    }
}
