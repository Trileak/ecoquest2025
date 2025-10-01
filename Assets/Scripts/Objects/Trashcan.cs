using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trashcan : MonoBehaviour
{
    private bool playingEndless = false;
    private TrashTracker trashTracker;
    
    [SerializeField] private int totalTrashThrownCount;
    [SerializeField] private int trashThrownCount;
    [SerializeField] private GameObject game;
    [SerializeField] private string acceptedTrash;

    private void Start()
    {
        totalTrashThrownCount = trashThrownCount;
        Events.OnGameOver    += EventsOnGameOver;
        trashTracker          = game.GetComponent<TrashTracker>();
    }

    private void EventsOnGameOver()
    {
        playingEndless = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains(acceptedTrash))
        {
            trashTracker.DeleteTrash(gameObject.transform);
            AddTrashThrownCount(); // Add 1 to the trash thrown count
        }
        else if (collision.gameObject.GetComponent<Trash>() != null)
        {
            Events.TriggerMisplace();
        }
        else if (!collision.gameObject.name.Contains(acceptedTrash))
        {
            Debug.Log($"{acceptedTrash} != {collision.gameObject}");
        }
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
