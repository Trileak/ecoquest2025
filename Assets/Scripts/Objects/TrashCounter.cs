using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashCounter : MonoBehaviour
{
    private TrashTracker trashTracker;
    
    [SerializeField] private TextMeshProUGUI text;
    
    private void Awake()
    {
        trashTracker = FindAnyObjectByType<TrashTracker>();
    }
    
    private void Update()
    {
        Debug.Log(text);
        Debug.Log(trashTracker);
        text.text = $"Trash: {trashTracker.TrashThrownCount()}";
    }
}
