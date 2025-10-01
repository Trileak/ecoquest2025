using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashCounter : MonoBehaviour
{
    private ChangeLabels changeLabels;
    
    [SerializeField] private TextMeshProUGUI text;
    
    private void Awake()
    {
        changeLabels = FindAnyObjectByType<ChangeLabels>();
    }
    
    private void Update()
    {
        Debug.Log(text);
        Debug.Log(changeLabels);
        text.text = $"Trash: {changeLabels.GetTotalTrash()}";
    }
}
