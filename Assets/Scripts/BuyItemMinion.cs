using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    [SerializeField] private Button myButton;
    [SerializeField] private Trashcan trashcan;
    [SerializeField] private GameObject minionPrefab;

    void Start()
    {
        myButton?.onClick.AddListener(HandleClick);
        trashcan = FindObjectOfType<Trashcan>();
    }

    void HandleClick()
    {
        Debug.Log("18");
        if (trashcan?.TrashThrownCount() >= 10)
        {
            Debug.Log("21");
            trashcan?.RemoveTrashThrownCount(10);
            GameObject minion = Instantiate(minionPrefab, transform.position, Quaternion.identity);
            minion.GetComponent<Minion>()?.OnBought();
        }
    }
}
