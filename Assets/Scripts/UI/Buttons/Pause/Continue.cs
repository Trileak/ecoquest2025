using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Continue : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    
    [SerializeField] private Button button;

    private void Awake()
    {
        playerInputActions = GameObject.Find("Player").GetComponent<Player>().GetInputActions();
        button.onClick.AddListener(OnContinue);
    }

    private void OnContinue()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("PauseMenu");
        Events.MouseControl(true);
        playerInputActions.Enable();
    }
}
