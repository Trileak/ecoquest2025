using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private List<Transform> followables = new List<Transform>();
    private Transform currentTarget;
    private Rigidbody rigidbody;
    private bool isGrounded = false;
    private bool canMove = true;
    private Vector3 wanderTarget;
    private bool isWandering = false;
    private float idleTimer = 0f;
    private bool misplaced = false;


    [SerializeField] private float wanderRadius  = 4f;
    [SerializeField] private float idleDuration  = 3f;
    [SerializeField] private float moveForce     = 1f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float jumpForce     = 2f;
    [SerializeField] private float jumpCooldown  = 2f;
    [SerializeField] private float pauseTime     = 5f;
    private float jumpTimer = 0f;
    private float pauseTimer = 0f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        Events.OnMisplace += EventsOnMisplace;
    }

    private void EventsOnMisplace()
    {
        misplaced = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }

        if (other.gameObject.name.Contains("Wall"))
        {
            Destroy(other.gameObject);
            canMove = false;
            pauseTimer = pauseTime;
            Debug.Log("Argh!");
        }

        if (other.gameObject.CompareTag("Player") && misplaced)
        {
            misplaced = false;
            Debug.Log("Gotcha!");
            other.gameObject.GetComponent<Player>().LoseLife();
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
        if (misplaced)
        {
            AttackFollowables();
        }
        else
        {
            WanderRandomly();
        }
    }
    
    private void WanderRandomly()
    {
        if (!isWandering)
        {
            Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
            wanderTarget = new Vector3(
                transform.position.x + randomCircle.x,
                transform.position.y,
                transform.position.z + randomCircle.y
            );
            isWandering = true;
        }

        Vector3 direction = wanderTarget - transform.position;
        direction.y = 0f;

        if (direction.magnitude > 1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rigidbody.AddRelativeForce(Vector3.forward * moveForce);
        }
        else
        {
            idleTimer += Time.fixedDeltaTime;
            if (idleTimer >= idleDuration)
            {
                idleTimer = 0f;
                isWandering = false;
            }
        }
    }
    
    private void AttackFollowables()
    {
        if (!canMove)
        {
            pauseTimer -= Time.fixedDeltaTime;
            if (pauseTimer <= 0f)
            {
                canMove = true;
            }
            return;
        }

        followables.Clear();
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Followable");
        foreach (GameObject target in targets)
        {
            followables.Add(target.transform);
        }

        Player player = FindFirstObjectByType<Player>();
        if (player != null)
        {
            followables.Add(player.transform);
        }

        if (followables.Count > 0)
        {
            currentTarget = followables[0];
        }
        else
        {
            currentTarget = null;
        }

        jumpTimer += Time.fixedDeltaTime;

        Vector3 direction = currentTarget.position - transform.position;
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
