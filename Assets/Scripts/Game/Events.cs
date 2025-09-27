using UnityEngine;
using System;
using UnityEngine.InputSystem;
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
        SceneManager.LoadScene("WinPopup", LoadSceneMode.Additive);
        MouseControl(false);
        Time.timeScale = 0f;
    }

    public static void TriggerLoss()
    {
        OnLoss?.Invoke();
        MouseControl(false);
        Time.timeScale = 0f;
    }

    public static void TriggerGameEnd()
    {
        OnGameOver?.Invoke();
        Time.timeScale = 1f;
    }

    public static void MouseControl(bool doLock)
    {
        if (!doLock)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
