using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    private Player player;
    
    [SerializeField] private Button myButton;
    [SerializeField] private Trashcan trashcan;
    [SerializeField] private GameObject minionPrefab;

    private void Start()
    {
        myButton?.onClick.AddListener(OnBuyMinion);
        trashcan = FindObjectOfType<Trashcan>();
        player   = FindObjectOfType<Player>();
    }

    private void OnBuyMinion()
    {
        if (trashcan == null || player == null)
        {
            Debug.LogWarning("Missing reference to Trashcan or Player.");
            return;
        }
        
        if (trashcan?.TrashThrownCount() >= 10)
        {
            trashcan?.RemoveTrashThrownCount(10);
            GameObject minion = Instantiate(minionPrefab, transform.position, Quaternion.identity);
            minion.GetComponent<HoldManager>()?.OnBought(player.GetGrabPointTransform());
            player.AddGameObject(minion);
        }
    }
}
