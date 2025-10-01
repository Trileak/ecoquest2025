using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private TrashTracker trashTracker;
    
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private float spawnRange    = 400f;
    [SerializeField] private GameObject tetrapak;
    [SerializeField] private GameObject can;
    [SerializeField] private GameObject bottle;

    private void Start()
    {
        InvokeRepeating("SpawnRandom", 0f, spawnInterval);
        trashTracker = FindObjectOfType<TrashTracker>();
    }

    private void SpawnRandom()
    {
        Vector3 randomPosition = new Vector3(
            UnityEngine.Random.Range(-spawnRange, spawnRange),
            1f,
            UnityEngine.Random.Range(-spawnRange, spawnRange)
        );

        int choice = UnityEngine.Random.Range(1, 4);
        if (choice == 1)
        {
            trashTracker?.AddTrash(Instantiate(tetrapak, randomPosition, Quaternion.identity).transform);
        }
        else if (choice == 2)
        {
            trashTracker?.AddTrash(Instantiate(can, randomPosition, Quaternion.identity).transform);
        }
        else if (choice == 3)
        {
            trashTracker?.AddTrash(Instantiate(bottle, randomPosition, Quaternion.identity).transform);
        }
    }
}
