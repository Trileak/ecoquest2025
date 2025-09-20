using System;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    private List<Transform> trashTransforms;
    private TrashTracker trashTracker;
    private Rigidbody rigidbody;
    private bool isColliding = false;

    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float moveForce = 1f;
    [SerializeField] private float dodgeForce = 1f;

    private void Awake()
    {
        trashTracker    = FindObjectOfType<TrashTracker>();
        trashTransforms = trashTracker.GetTrashTransforms();
        rigidbody       = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Floor"))
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.gameObject.CompareTag("Floor"))
        {
            isColliding = false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction;
        Transform closestTrash = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform trash in trashTransforms)
        {
            float distance = Vector3.Distance(transform.position, trash.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTrash = trash;
            }
        }

        if (closestTrash != null)
        {
            direction = closestTrash.position - transform.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
        if (rigidbody.velocity.x < 10f)
        {
            // Move forward
            rigidbody.AddRelativeForce(Vector3.forward * moveForce, ForceMode.Impulse);

            Debug.Log(rigidbody.velocity);

            // Dodge if colliding
            if (isColliding)
            {
                rigidbody.AddRelativeForce(Vector3.left * dodgeForce, ForceMode.Impulse);
            }
        }
        //if ()
    }
}
