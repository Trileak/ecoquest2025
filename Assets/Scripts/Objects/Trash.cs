using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private Rigidbody objectRigidbody;          // The rigidbody of the object
    private Transform objectGrabPointTransform; // The transform of the grab point of the object
    private TrashTracker trashTracker;          // A trash tracker
    private Trashcan trashcan;                  // The trashcan
    private bool isHeld = false;                // Check if object is held
    private bool isThrown = false;              // Check if the object is thrown (so doesn't give 3 points for 1 trash)
    
    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();        // Get the rigidbody component
        trashTracker    = FindObjectOfType<TrashTracker>(); // Get the trashtracker object
        trashcan        = FindObjectOfType<Trashcan>();     // Get the trashcan object
    }
    
    public void Grab(Transform objectGrabPointTransform) // When grabbed
    {
        this.objectGrabPointTransform = objectGrabPointTransform; // Get the object's transform
        transform.SetParent(objectGrabPointTransform);
        objectRigidbody.useGravity                             = false; // Stop using gravity 
        objectRigidbody.isKinematic                            = true;  // Stop using the rigidbody
        isHeld                                                 = true;  // Become held
        gameObject.transform.GetComponent<Collider>().enabled  = false; // Turn off parent collider
        Collider playerCollider = FindObjectOfType<Player>().GetComponentInChildren<Collider>();
        Physics.IgnoreCollision(transform.GetComponent<Collider>(), playerCollider, true);
    }

    public void Drop()
    {
        if (transform != null)
        {
            transform.SetParent(null);
            objectRigidbody.isKinematic = false; // Start using the rigidbody
            objectRigidbody.useGravity = true; // Start using gravity
            isHeld = false; // Start being held
            gameObject.transform.GetComponent<Collider>().enabled = true; // Turn on parent collider
            Collider playerCollider = FindObjectOfType<Player>().GetComponentInChildren<Collider>();
            Physics.IgnoreCollision(transform.GetComponent<Collider>(), playerCollider, true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("TrashCan"))
        {
            Destroy(this.gameObject); // Destroy this game object
        }
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}
