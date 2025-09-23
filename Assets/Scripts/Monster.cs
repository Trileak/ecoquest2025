using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Transform player;       // Reference to the player
    private Rigidbody rigidbody;    // Monster's rigidbody
    private bool isGrounded = false;
    private bool canMove = true;    // Movement toggle

    [SerializeField] private float moveForce     = 1f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float jumpForce     = 2f;
    [SerializeField] private float jumpCooldown  = 2f;
    [SerializeField] private float pauseTime     = 5f;
    private float jumpTimer = 0f;
    private float pauseTimer = 0f;  // Separate timer for pause logic

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>().transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }

        if (other.gameObject.name.Contains("Wall"))
        {
            Destroy(other.gameObject); // Destroy wall
            canMove = false;           // Pause movement
            pauseTimer = pauseTime;    // Start pause timer
            Debug.Log("Argh!");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        // Handle pause timer
        if (!canMove)
        {
            pauseTimer -= Time.fixedDeltaTime;
            if (pauseTimer <= 0f)
            {
                canMove = true;
            }
            return; // Skip movement while paused
        }

        jumpTimer += Time.fixedDeltaTime;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        // Rotate toward player
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // Apply forward movement
        rigidbody.AddRelativeForce(Vector3.forward * moveForce);

        // Jump if grounded and cooldown met
        if (isGrounded && jumpTimer >= jumpCooldown && direction.magnitude > 0.2f)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpTimer = 0f;
        }
    }
}
