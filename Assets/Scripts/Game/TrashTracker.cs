using System;
using System.Collections.Generic;
using UnityEngine;

public class TrashTracker : MonoBehaviour
{
    private readonly List<Transform> trashTransforms = new();
    private readonly List<GameObject> trackerObjects = new();
    private Player player;

    [SerializeField] private GameObject trackerImagePrefab;
    [SerializeField] private GameObject trackerImageParent;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        Events.OnGameStart += EventsOnGameStart;
    }

    private void EventsOnGameStart()
    {
        foreach (var trashTransform in trashTransforms)
        {
            Destroy(trashTransform.gameObject);
        }

        foreach (var trackerObject in trackerObjects)
        {
            Destroy(trackerObject.gameObject);
        }
        trashTransforms.Clear();
        trackerObjects.Clear();
    }

    private float GetAngleOfTrash(Transform trashTransform)
    {
        if (trashTransform == null || player == null) return 0f;

        Vector3 toTrash = trashTransform.position - player.transform.position;
        toTrash.y = 0;

        Vector3 playerForward = player.transform.forward;
        playerForward.y = 0;

        return Vector3.SignedAngle(playerForward, toTrash, Vector3.up);
    }

    private void UpdateTrackers()
    {
        for (int i = 0; i < trashTransforms.Count; i++)
        {
            Transform trash = trashTransforms[i];
            GameObject tracker = trackerObjects[i];

            if (trash == null || tracker == null) continue;

            float angle = GetAngleOfTrash(trash) * 2;
            Vector3 trackerPos = tracker.transform.position;
            trackerPos.x = angle + Screen.width / 2;
            tracker.transform.position = trackerPos;
        }
    }

    private void FixedUpdate()
    {
        UpdateTrackers();
    }

    public void AddTrash(Transform trash)
    {
        if (trash == null || trackerImagePrefab == null || trackerImageParent == null) return;

        trashTransforms.Add(trash);
        trackerObjects.Add(Instantiate(trackerImagePrefab, trackerImageParent.transform));
    }

    public void DeleteTrash(Transform trash)
    {
        int index = trashTransforms.IndexOf(trash);
        if (index < 0 || index >= trackerObjects.Count) return;

        Destroy(trackerObjects[index]);
        trackerObjects.RemoveAt(index);
        trashTransforms.RemoveAt(index);
    }

    public int GetTrashCount() => trashTransforms.Count;

    public List<Transform> GetTrashTransforms() => new(trashTransforms);

    public void HoldTrashTransform(Transform trashTransform)
    {
        int index = trashTransforms.IndexOf(trashTransform);
        if (index != -1 && trackerObjects[index] != null)
        {
            trackerObjects[index].transform.localScale = new Vector3(1, 0.5f, 1);
        }
    }

    public void DropTrashTransform(Transform trashTransform)
    {
        int index = trashTransforms.IndexOf(trashTransform);
        if (index != -1 && trackerObjects[index] != null)
        {
            trackerObjects[index].transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
