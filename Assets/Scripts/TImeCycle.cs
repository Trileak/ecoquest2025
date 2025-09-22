using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    private GameObject sun;

    private const float realDayLength = 180f;
    private const float fullRotation = 360f;

    private float rotationSpeed;

    private void Awake()
    {
        sun = GameObject.Find("Sun");
        rotationSpeed = fullRotation / realDayLength;
    }

    private void FixedUpdate()
    {
        sun.transform.Rotate(Vector3.right * rotationSpeed * Time.fixedDeltaTime);
    }
}