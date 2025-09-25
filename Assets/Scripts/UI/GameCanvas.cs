using System;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
        Events.OnGameStart += EventsOnGameStart;
    }
    
    private void EventsOnGameStart()
    {
        gameObject.SetActive(true);
    }
}
