using System;
using System.Collections;
using System.Collections.Generic;
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
            AddTrashThrownCount();
            trashTracker.AddTrashThrown(1);
        }
        else if (collision.gameObject.GetComponent<Trash>() != null)
        {
            Events.TriggerMisplace();
        }
    }
    
    public void AddTrashThrownCount()
    {
        trashThrownCount++;
        totalTrashThrownCount++;
    }
}
