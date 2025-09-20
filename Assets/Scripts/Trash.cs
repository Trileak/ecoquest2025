using System;
using System.Collections;
using System.Collections.Generic;
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
        objectRigidbody.useGravity    = false;                    // Stop using gravity 
        objectRigidbody.isKinematic   = true;                     // Stop using the rigidbody
        isHeld                        = true;                     // Become held
        foreach (Transform child in gameObject.transform) // For each child in this parent
        {
            Collider collider = child.GetComponent<Collider>(); // Get the collider
            if (collider != null) // If there is a collider
            {
                collider.enabled = false; // Turn it off
            }
        }

    }

    public void Drop()
    {
        objectRigidbody.isKinematic = false; // Start using the rigidbody
        objectRigidbody.useGravity  = true;  // Start using gravity
        isHeld                      = false; // Start being held
        foreach (Transform child in gameObject.transform) // Same as line 22 but turns *off* the collider
        {
            Collider collider = child.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (objectRigidbody && isHeld) // If object rigidbody isn't null and is being held
        {
            objectRigidbody.MovePosition(objectGrabPointTransform.position); // Go to the transform position
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Trash Can")
        {
            trashTracker.DeleteTrash(gameObject.transform);
            if (isThrown == false)
            {
                trashcan.AddTrashThrownCount(); // Add 1 to the trash thrown count
                isThrown = !isThrown;           // Same as isThrown = true 
            }
            Destroy(this.gameObject); // Destroy this game object
        }
    }
}
