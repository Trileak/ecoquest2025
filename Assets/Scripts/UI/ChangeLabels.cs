using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeLabels : MonoBehaviour
{
    private TrashTracker trashTracker;
    private int totalTrash;
    
    [SerializeField] private TextMeshProUGUI trashCounterLabel;

    private void Awake()
    {
        trashTracker = FindObjectOfType<TrashTracker>();
    }
    
    void FixedUpdate()
    {
        totalTrash = trashTracker.TrashThrownCount();
        trashCounterLabel.SetText($"Trash: {totalTrash}");
    }
}
