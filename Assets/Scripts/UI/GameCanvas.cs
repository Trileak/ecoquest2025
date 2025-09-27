using System;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
        Events.OnGameStart += EventsOnGameStart;
    }
    
    private void EventsOnGameStart()
    {
        if (this)
        {
            gameObject.SetActive(true);
        }
    }
}
