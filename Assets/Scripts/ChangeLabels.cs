using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeLabels : MonoBehaviour
{
    private Trashcan trashcan;
    
    [SerializeField] private TextMeshProUGUI trashCounterLabel;
    
    void Start()
    {
        trashcan = FindObjectOfType<Trashcan>();
    }

    void FixedUpdate()
    {
        trashCounterLabel.SetText($"Trash: {trashcan.TrashThrownCount()}");
    }
}
