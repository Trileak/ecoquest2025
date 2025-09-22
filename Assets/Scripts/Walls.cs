using UnityEngine;

public class Walls : MonoBehaviour
{
    private HoldManager holdManager;
    private Transform objectHoldTransform;
    private Player player;
    private Rigidbody rigidbody;
        
    private void Awake()
    {
        holdManager         = GetComponent<HoldManager>();
        player              = FindObjectOfType<Player>();
        objectHoldTransform = player.GetGrabPointTransform();
        rigidbody           = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (holdManager.IsHeld())
        {
            objectHoldTransform = player.GetGrabPointTransform();
            rigidbody.MovePosition(objectHoldTransform.position);
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
            targetRotation *= Quaternion.Euler(0, -90f, 0);
            rigidbody.MoveRotation(targetRotation);
        }
    }

}
