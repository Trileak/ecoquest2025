using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class BuyItemWalls : MonoBehaviour
{
    private Player player;
    private TrashTracker trashTracker;
    
    [SerializeField] private Button myButton;
    [SerializeField] private GameObject wallsPrefab;

    void Start()
    {
        trashTracker = FindObjectOfType<TrashTracker>();
        player = GameObject.Find("Player")?.GetComponent<Player>();
        myButton?.onClick.AddListener(HandleClick);
    }

    void HandleClick()
    {
        if (trashTracker.TrashThrownCount() >= 2)
        {
            GameObject walls = Instantiate(wallsPrefab, player.GetGrabPointTransform().position, Quaternion.identity);
            walls.GetComponent<HoldManager>()?.OnBought(player.GetGrabPointTransform());
            player.AddGameObject(walls);
            trashTracker.RemoveTrashThrown(2);
        }
    }
}