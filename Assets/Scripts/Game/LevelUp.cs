using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private TrashTracker trashTracker;
    private int level;

    private void Awake()
    {
        trashTracker = GetComponent<TrashTracker>();
        if (trashTracker == null)
        {
            Debug.LogError("TrashTracker not found!");
        }
    }

    private void Update()
    {
        if (trashTracker == null) return;

        int totalTrashThrown = trashTracker.TotalThrownTrashCount();
        Debug.Log("Trash Thrown: " + totalTrashThrown);

        level = 0;

        if (totalTrashThrown <= 3)
        {
            level = 1; // 60
            trashTracker.SetTrashDespawnTimes(60);
        }
        else if (totalTrashThrown <= 15)
        {
            level = 2; // 45
            trashTracker.SetTrashDespawnTimes(45);
        }
        else if (totalTrashThrown <= 28)
        {
            level = 3; // 30
            trashTracker.SetTrashDespawnTimes(30);
        }
        else if (totalTrashThrown <= 41)
        {
            level = 4; // 15
            trashTracker.SetTrashDespawnTimes(15);
        }
        else if (totalTrashThrown <= 49)
        {
            level = 5; // 7.5
            trashTracker.SetTrashDespawnTimes(7.5f);
        }

        Debug.Log("Current Level: " + level);
    }

    public int GetLevel()
    {
        Debug.Log("GetLevel called. Returning level: " + level);
        return level;
    }
}