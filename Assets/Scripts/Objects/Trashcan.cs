using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    [SerializeField] private int trashThrownCount;
    [SerializeField] private int totalTrashThrownCount;
    [SerializeField] private GameObject game;

    public void AddTrashThrownCount()
    {
        trashThrownCount++;
        totalTrashThrownCount++;

        if (totalTrashThrownCount >= 100)
        {
            Events.TriggerWin();
        }
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
