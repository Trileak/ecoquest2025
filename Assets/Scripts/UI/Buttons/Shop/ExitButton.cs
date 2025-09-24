using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private Button myButton;
    [SerializeField] private Player playerReference; // Drag your Player GameObject here

    private void Awake()
    {
        playerReference = FindObjectOfType<Player>();
    }
    
    private void Start()
    {
        myButton?.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        SceneManager.UnloadSceneAsync("Shop");
        Time.timeScale = 1f; 
        var inputActions = playerReference?.GetInputActions(); 
        inputActions.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
