using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashCounter : MonoBehaviour
{
    private Trashcan trashcan;
    
    [SerializeField] private TextMeshProUGUI text;
    
    private void Awake()
    {
        trashcan = FindObjectOfType<Trashcan>();
    }
    
    private void Update()
    {
        Debug.Log(text);
        Debug.Log(trashcan);
        text.text = $"Trash: {trashcan.TrashThrownCount()}";
    }
}
