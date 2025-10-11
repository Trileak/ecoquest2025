using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    private GameObject sun;

    private const float realDayLength = 180f; // Duration of a full day in seconds
    private const float fullRotation = 360f;

    private int days;
    private float rotationSpeed;
    private float elapsedTime = 0f; // Time counter

    private void Awake()
    {
        sun = GameObject.Find("Sun");
        rotationSpeed = fullRotation / realDayLength;
    }

    private void FixedUpdate()
    {
        sun.transform.Rotate(Vector3.right * rotationSpeed * Time.fixedDeltaTime);
        elapsedTime += Time.fixedDeltaTime;
        if (elapsedTime >= realDayLength)
        {
            elapsedTime = 0f;
            days++;
        }
    }

    public int GetDays()
    {
        return days;
    }
}