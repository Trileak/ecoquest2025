using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeLabels : MonoBehaviour
{
    [SerializeField] Trashcan trashcanTetrapak;
    [SerializeField] Trashcan trashcanSoda;
    [SerializeField] Trashcan trashcanBottle;
    [SerializeField] private TextMeshProUGUI trashCounterLabel;

    void FixedUpdate()
    {
        int totalTrash = trashcanTetrapak.TrashThrownCount() + trashcanSoda.TrashThrownCount() + trashcanBottle.TrashThrownCount();
        trashCounterLabel.SetText($"Trash: {totalTrash}");
    }

    public int GetTotalTrash()
    {
        return trashcanTetrapak.TrashThrownCount() + trashcanSoda.TrashThrownCount() + trashcanBottle.TrashThrownCount();
    }
}
