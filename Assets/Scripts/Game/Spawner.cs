using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private TrashTracker trashTracker;
    
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private float spawnRange    = 400f;
    [SerializeField] private GameObject trash;

    private void Start()
    {
        InvokeRepeating("SpawnRandom", 0f, spawnInterval);
        trashTracker = FindObjectOfType<TrashTracker>();
    }

    private void SpawnRandom()
    {
        Vector3 randomPosition = new Vector3(
            UnityEngine.Random.Range(-spawnRange, spawnRange),
            1f, // fixed height, adjust as needed
            UnityEngine.Random.Range(-spawnRange, spawnRange)
        );
        
        trashTracker?.AddTrash(Instantiate(trash, randomPosition, Quaternion.identity).transform);
    }
}
