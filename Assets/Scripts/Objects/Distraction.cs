using UnityEngine;

public class Distraction : MonoBehaviour
{
    private HoldManager holdManager;
    private Transform monsterTransform;

    private void Awake()
    {
        holdManager      = FindObjectOfType<HoldManager>();        // Hold Manager
        monsterTransform = FindObjectOfType<Monster>().transform;  // Monster transform
    }

    private void FixedUpdate()
    {
        if ((this.transform.position - monsterTransform.position).magnitude < 2f) // If the monster is close to the object
        {
            Destroy(this.gameObject); // Destroy it
        }
    }
}
