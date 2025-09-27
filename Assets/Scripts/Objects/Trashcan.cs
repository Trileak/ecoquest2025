using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trashcan : MonoBehaviour
{
    private bool playingEndless = false;
    
    [SerializeField] private int totalTrashThrownCount;
    [SerializeField] private int trashThrownCount;
    [SerializeField] private GameObject game;

    private void Start()
    {
        totalTrashThrownCount = trashThrownCount;
        Events.OnGameOver += EventsOnGameOver;
    }

    private void EventsOnGameOver()
    {
        playingEndless = true;
    }

    private void LateUpdate()
    {
        if (totalTrashThrownCount >= 100 && !SceneManager.GetSceneByName("WinPopup").isLoaded && !playingEndless)
        {
            Events.TriggerWin();
        }
    }
    
    public void AddTrashThrownCount()
    {
        trashThrownCount++;
        totalTrashThrownCount++;
        Debug.Log(totalTrashThrownCount);
    }

    public int TrashThrownCount()
    {
        return trashThrownCount;
    }
    
    public int TotalTrashThrownCount()
    {
        return totalTrashThrownCount;
    }

    public void RemoveTrashThrownCount(int amount)
    {
        trashThrownCount = Mathf.Max(0, trashThrownCount - amount);
    }
}
