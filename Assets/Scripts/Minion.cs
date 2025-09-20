using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Minion : MonoBehaviour
{
    private List<Transform> trashTransforms;
    private TrashTracker trashTracker;
    private Rigidbody rigidbody = new Rigidbody();
    private bool isColliding = false;
    private bool pickedUpTrash = false;
    private Trash trash;

    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float moveForce = 1f;
    [SerializeField] private float dodgeForce = 1f;
    [SerializeField] private Transform objectGrabPoint;
    [SerializeField] private Transform trashCan;

    private void Awake()
    {
        trashTracker = FindObjectOfType<TrashTracker>();
        trashTransforms = trashTracker.GetTrashTransforms();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Trash"))
        {
            other.gameObject.transform.TryGetComponent(out trash);
            trash.Grab(objectGrabPoint);
            pickedUpTrash = true;
        }

        if (other.gameObject.name == "Trash Can")
        {
            if (trash != null)
            {
                trash.Drop();
                pickedUpTrash = false;
            }
        }

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
        if (!pickedUpTrash)
        {
            Transform closest = GetClosestTrash();
            if (closest != null)
            {
                Move(closest.position);
            }
        }
        else
        {
            Move(trashCan.position);
        }
    }

    private Transform GetClosestTrash()
    {
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
        return closestTrash;
    }

    private void Move(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime * 100f);
        }

        Vector3 moveDirection = direction.normalized;
        float distance = direction.magnitude;

        if (distance > 0.5f)
        {
            rigidbody.AddForce(moveDirection * moveForce, ForceMode.Acceleration);
        }

        float maxSpeed = 2.5f;
        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }

        if (isColliding)
        {
            Vector3 dodgeDirection = -transform.right;
            rigidbody.AddForce(dodgeDirection * dodgeForce, ForceMode.Acceleration);
        }
    }
}
