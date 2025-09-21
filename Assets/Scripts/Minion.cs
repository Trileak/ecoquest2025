using System;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    private List<Transform> trashTransforms;
    private TrashTracker trashTracker;
    private Rigidbody rigidbody;
    private bool isColliding   = false;
    private bool pickedUpTrash = false;
    private Trash trash;
    private Player player;
    private bool isHeld;
    private Vector3 trashCan = new Vector3(2, 1, 10);
    
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float moveForce     = 1f;
    [SerializeField] private float dodgeForce    = 1f;
    [SerializeField] private Transform objectGrabPoint;

    private void Awake()
    {
        trashTracker    = FindObjectOfType<TrashTracker>();
        trashTransforms = trashTracker.GetTrashTransforms();
        rigidbody       = GetComponent<Rigidbody>();
        player          = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Trash"))
        {
            other.gameObject.transform.TryGetComponent(out trash);
            trash.Grab(objectGrabPoint);
            trashTracker.HoldTrashTransform(trash.transform);
            pickedUpTrash = true;
        }

        if (other.gameObject.name == "Trash Can")
        {
            if (trash != null)
            {
                trash.Drop();
                trashTracker.DropTrashTransform(trash.transform);
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
        TryGetComponent<HoldManager>(out HoldManager holdManager);
        isHeld =  holdManager.IsHeld();
        if (!isHeld) 
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
                Move(trashCan);
            }
        }
        else
        {
            transform.position = player.GetGrabPointTransform().position;
        }
    }

    private Transform GetClosestTrash()
    {
        Transform closestTrash = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform trashTransform in trashTracker.GetTrashTransforms())
        {
            if (trashTransform == null) continue;

            Trash trashComponent = trashTransform.GetComponent<Trash>();
            if (trashComponent == null || trashComponent.IsHeld()) continue;

            float distance = Vector3.Distance(transform.position, trashTransform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTrash = trashTransform;
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
        if (rigidbody.linearVelocity.magnitude > maxSpeed)
        {
            rigidbody.linearVelocity = Vector3.ClampMagnitude(rigidbody.linearVelocity, maxSpeed);
        }

        if (isColliding)
        {
            Vector3 dodgeDirection = -transform.right;
            rigidbody.AddForce(dodgeDirection * dodgeForce, ForceMode.Acceleration);
        }
    }
}
