using System;
using UnityEngine;

public class HoldManager : MonoBehaviour
{
    private Rigidbody rigidbody;
    private bool isHeld;

    private void Awake()
    {
        rigidbody     = GetComponent<Rigidbody>();
    }

    public void OnBought()
    {
        rigidbody.useGravity  = false;                     // Stop using gravity 
        rigidbody.isKinematic = true;                      // Stop using the rigidbody
        isHeld                = true;                      // Become held
        transform.localScale *= 0.5f;                      // Make self smaller
        var component = GetComponent<Collider>();          // Checks if collider is null
        if (component != null) component.enabled = false;
    }

    public void Drop()
    {
        rigidbody.isKinematic = false;                     // Start using the rigidbody
        rigidbody.useGravity  = true;                      // Start using gravity
        isHeld                = false;                     // Start being held
        var component = GetComponent<Collider>();
        if (component != null) component.enabled = true;
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}
