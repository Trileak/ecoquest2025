using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable InconsistentNaming

public class TrashTracker : MonoBehaviour
{
    private List<Transform> trashTransforms;
    private List<GameObject> trackerObjects;
    private Player player;

    [SerializeField] private GameObject trackerImagePrefab;
    [SerializeField] private GameObject trackerImageParent;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        trashTransforms = new List<Transform>();
        trackerObjects = new List<GameObject>();
    }

    private float GetAngleOfTrash(Transform trashTransform)
    {
        Vector3 toTrash = trashTransform.position - player.transform.position;
        toTrash.y = 0;
        Vector3 playerForward = player.transform.forward;
        playerForward.y = 0;
        float angle = Vector3.SignedAngle(playerForward, toTrash, Vector3.up);
        return angle;
    }
    
    private void UpdateTrackers()
    {
        for (int i = 0; i < trashTransforms?.Count; i++)
        {
            float angle = GetAngleOfTrash(trashTransforms[i]) * 2; // Range: -180 to 180 * 2 for both

            Vector3 trackerPos = trackerObjects[i].transform.position;
            trackerPos.x = angle + Screen.width / 2; // Offset to middle of the screen
            trackerObjects[i].transform.position = trackerPos; 
        }
    }


    private void FixedUpdate()
    {
        UpdateTrackers();
    }

    private float NormalizeAngle(float angle)
    {
        return Mathf.DeltaAngle(0f, angle);
    }
    
    public void AddTrash(Transform trash)
    {
        trashTransforms?.Add(trash);
        trackerObjects?.Add(Instantiate(trackerImagePrefab, trackerImageParent.transform));
    }

    public void DeleteTrash(Transform trash)
    {
        int index = trashTransforms.IndexOf(trash);

        if (index >= 0 && index < trackerObjects.Count)
        {
            // Remove tracker GameObject
            Destroy(trackerObjects[index]);
            trackerObjects.RemoveAt(index);

            // Remove trash reference
            trashTransforms.RemoveAt(index);
        }
    }

    public int GetTrashCount()
    {
        return trashTransforms.Count;
    }

    public List<Transform> GetTrashTransforms()
    {
        return trashTransforms;
    }
    
    public void HoldTrashTransform(Transform trashTransform)
    {
        if (trashTransforms.IndexOf(trashTransform) != -1)
        {
            trackerObjects[trashTransforms.IndexOf(trashTransform)].transform.localScale = new Vector3(1, 0.5f, 1);
        }
    }

    public void DropTrashTransform(Transform trashTransform)
    {
        if (trashTransforms.IndexOf(trashTransform) != -1)
        {
            trackerObjects[trashTransforms.IndexOf(trashTransform)].transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
