using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class BuyItemDistraction : MonoBehaviour
{
    private Player player;
    private TrashTracker trashTracker;
    
    [SerializeField] private Button myButton;
    [SerializeField] private GameObject distractionPrefab;

    void Start()
    {
        player       = GameObject.Find("Player")?.GetComponent<Player>();
        trashTracker = GameObject.Find("TrashTracker")?.GetComponent<TrashTracker>();
        myButton?.onClick.AddListener(HandleClick);
    }

    void HandleClick()
    {
        if (trashTracker.TrashThrownCount() >= 5)
        {
            trashTracker.RemoveTrashThrown(5);
            GameObject distraction = Instantiate(distractionPrefab, player.GetGrabPointTransform().position, Quaternion.identity);
            distraction.GetComponent<HoldManager>()?.OnBought(player.GetGrabPointTransform());
            player.AddGameObject(distraction);
        }
    }
}