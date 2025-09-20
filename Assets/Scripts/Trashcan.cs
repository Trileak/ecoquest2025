using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    private int trashThrownCount;
    private int totalTrashThrownCount;
    
    [SerializeField] private GameObject game;

    public void AddTrashThrownCount()
    {
        trashThrownCount++;
    }

    public int TrashThrownCount()
    {
        return trashThrownCount;
    }
    
    public int TotalTrashThrownCount()
    {
        return totalTrashThrownCount;
    }
}
