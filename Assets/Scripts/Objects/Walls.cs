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
            // Smoothly move toward the grab point
            Vector3 targetPosition = objectHoldTransform.position;
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, 0.2f);
            rigidbody.MovePosition(newPosition);

            // Rotate to face the player
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
            targetRotation *= Quaternion.Euler(0, -90f, 0); // Adjust based on wall's orientation
            rigidbody.MoveRotation(Quaternion.Slerp(rigidbody.rotation, targetRotation, 0.2f));
        }
    }
}
