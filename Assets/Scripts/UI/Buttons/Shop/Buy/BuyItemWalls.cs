using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class BuyItemWalls : MonoBehaviour
{
    private Player player;
    
    [SerializeField] private Button myButton;
    [SerializeField] private Trashcan trashcan;
    [SerializeField] private GameObject wallsPrefab;

    private void Start()
    {
        trashcan = FindObjectOfType<Trashcan>();
        player = GameObject.Find("Player")?.GetComponent<Player>();
        myButton?.onClick.AddListener(OnBuyWall);
    }

    private void OnBuyWall()
    {
        if (trashcan == null || player == null)
        {
            Debug.LogWarning("Missing reference to Trashcan or Player.");
            return;
        }

        if (trashcan.TrashThrownCount() >= 2)
        {
            GameObject walls = Instantiate(wallsPrefab, player.GetGrabPointTransform().position, Quaternion.identity);
            walls.GetComponent<HoldManager>()?.OnBought(player.GetGrabPointTransform());
            player.AddGameObject(walls);
            trashcan.RemoveTrashThrownCount(2);
        }
    }
}