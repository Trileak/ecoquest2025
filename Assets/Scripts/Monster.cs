using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Transform player;
    private Rigidbody rigidbody;
    private bool isGrounded = false;

    [SerializeField] private float moveForce     = 1f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float jumpForce     = 2f;
    [SerializeField] private float jumpCooldown  = 2f;
    private float jumpTimer = 0f;

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
        jumpTimer += Time.fixedDeltaTime;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        rigidbody.AddRelativeForce(Vector3.forward * moveForce);

        if (isGrounded && jumpTimer >= jumpCooldown && direction.magnitude > 0.2f)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpTimer = 0f;
        }
    }
}