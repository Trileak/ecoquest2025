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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OnWin?.Invoke();
        SceneManager.LoadScene("WinScene", LoadSceneMode.Additive);
        Time.timeScale = 0f;
    }

    public static void TriggerLoss()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OnLoss?.Invoke();
        Time.timeScale = 0f;
    }

    public static void TriggerGameEnd()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OnGameOver?.Invoke();
        Time.timeScale = 1f;
    }
}
