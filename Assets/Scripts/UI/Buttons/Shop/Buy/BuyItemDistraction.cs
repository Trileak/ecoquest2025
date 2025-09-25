using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class BuyItemDistraction : MonoBehaviour
{
    private Player player;
    
    [SerializeField] private Button myButton;
    [SerializeField] private Trashcan trashcan;
    [SerializeField] private GameObject distractionPrefab;

    private void Start()
    {
        trashcan = FindObjectOfType<Trashcan>();
        player = GameObject.Find("Player")?.GetComponent<Player>();
        myButton?.onClick.AddListener(OnBuyDistraction);
    }

    private void OnBuyDistraction()
    {
        if (trashcan == null || player == null)
        {
            Debug.LogWarning("Missing reference to Trashcan or Player.");
            return;
        }

        if (trashcan.TrashThrownCount() >= 2)
        {
            GameObject distraction = Instantiate(distractionPrefab, player.GetGrabPointTransform().position, Quaternion.identity);
            distraction.GetComponent<HoldManager>()?.OnBought(player.GetGrabPointTransform());
            player.AddGameObject(distraction);
            trashcan.RemoveTrashThrownCount(5);
        }
    }
}