using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitIntro : MonoBehaviour
{
    [SerializeField] private Button myButton;
    
    private void Start()
    {
        myButton?.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        SceneManager.UnloadSceneAsync("IntroScene");
    }
}
