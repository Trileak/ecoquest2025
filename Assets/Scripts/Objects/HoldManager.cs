using System;
using UnityEngine;

public class HoldManager : MonoBehaviour
{
    private Rigidbody rigidbody;
    private bool isHeld;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void OnBought(Transform grabPoint)
    {
        transform.SetParent(grabPoint); // Attach to grab point
        transform.localPosition = Vector3.zero; // Snap to grab point
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
        isHeld = true;
        transform.localScale *= 0.125f;

        Collider component = GetComponentInChildren<Collider>();
        if (component != null) component.enabled = false;
    }


    public void Drop()
    {
        transform.SetParent(null); // Detach from grab point
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        isHeld = false;
        transform.localScale *= 8f;

        Collider component = GetComponentInChildren<Collider>();
        if (component != null) component.enabled = true;
    }


    public bool IsHeld()
    {
        return isHeld;
    }
}
