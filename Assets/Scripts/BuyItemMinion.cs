using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    private Player player;
    
    [SerializeField] private Button myButton;
    [SerializeField] private Trashcan trashcan;
    [SerializeField] private GameObject minionPrefab;

    void Start()
    {
        myButton?.onClick.AddListener(HandleClick);
        trashcan = FindObjectOfType<Trashcan>();
        player   = FindObjectOfType<Player>();
    }

    void HandleClick()
    {
        if (trashcan?.TrashThrownCount() >= 10)
        {
            trashcan?.RemoveTrashThrownCount(10);
            GameObject minion = Instantiate(minionPrefab, transform.position, Quaternion.identity);
            minion.GetComponent<HoldManager>()?.OnBought();
            player.AddGameObject(minion);
        }
    }
}
