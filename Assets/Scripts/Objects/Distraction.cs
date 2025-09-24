using UnityEngine;

public class Distraction : MonoBehaviour
{
    private HoldManager holdManager;

    private void Awake()
    {
        holdManager = FindObjectOfType<HoldManager>();
    }

    private void FixedUpdate()
    {
        
    }
}
