using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    private Player player;
    private TrashTracker trashTracker;

    [SerializeField] private Button myButton;
    [SerializeField] private GameObject minionPrefab;

    void Start()
    {
        myButton?.onClick.AddListener(HandleClick);
        trashTracker = FindObjectOfType<TrashTracker>();
        player   = FindObjectOfType<Player>();
    }

    void HandleClick()
    {
        if (trashTracker?.TrashThrownCount() >= 10)
        {
            trashTracker?.RemoveTrashThrown(10);
            GameObject minion = Instantiate(minionPrefab, transform.position, Quaternion.identity);
            minion.GetComponent<HoldManager>()?.OnBought(player.GetGrabPointTransform());
            player.AddGameObject(minion);
        }
    }
}
