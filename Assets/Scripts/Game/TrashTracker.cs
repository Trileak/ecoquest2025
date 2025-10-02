using System;
using System.Collections.Generic;
using UnityEngine;

public class TrashTracker : MonoBehaviour
{
    private List<Transform> trashTransforms = new();
    private List<GameObject> trackerObjects = new();
    private Player player;
    private bool playingEndless = false;
    private GameObject compass;

    [SerializeField] private int totalThrownTrash = 0;
    [SerializeField] private int thrownTrash = 0;

    [SerializeField] private GameObject trackerImagePrefab;
    [SerializeField] private GameObject trackerImageParent;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        compass = GameObject.Find("Compass");
        Events.OnGameStart += EventsOnGameStart;
    }

    private void EventsOnGameStart()
    {
        foreach (var trashTransform in trashTransforms)
        {
            if (trashTransform != null)
            {
                Destroy(trashTransform.gameObject);
            }
        }

        foreach (var trackerObject in trackerObjects)
        {
            if (trackerObject != null)
            {
                Destroy(trackerObject.gameObject);
            }
        }

        trashTransforms.Clear();
        trackerObjects.Clear();
        totalThrownTrash = 0;
        thrownTrash = 0;
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
        RectTransform compassRect = compass.GetComponent<RectTransform>();
        float compassWidth = compassRect.rect.width;
        Vector3 compassPos = compassRect.position;

        for (int i = 0; i < trashTransforms.Count; i++)
        {
            Transform trash = trashTransforms[i];
            GameObject tracker = trackerObjects[i];

            if (trash != null && tracker != null)
            {
                float angle = GetAngleOfTrash(trash);
                float normalizedX = (angle / 180f) * (compassWidth / 2f);

                Vector3 trackerPos = tracker.transform.position;
                trackerPos.x = compassPos.x + normalizedX;
                trackerPos.y = compassPos.y + 20f;

                tracker.transform.position = trackerPos;
            }
        }
    }

    private void FixedUpdate()
    {
        UpdateTrackers();
        if (totalThrownTrash >= 100 && !playingEndless)
        {
            Events.TriggerWin();
            playingEndless = true;
        }
    }

    public void AddTrash(Transform trash)
    {
        if (trash != null || trackerImagePrefab != null || trackerImageParent != null)
        {
            trashTransforms.Add(trash);
            trackerObjects.Add(Instantiate(trackerImagePrefab, trackerImageParent.transform));
        }
    }

    public void DeleteTrash(Transform trash)
    {
        int index = trashTransforms.IndexOf(trash);
        if (!(index < 0 || index >= trackerObjects.Count))
        {
            Destroy(trackerObjects[index]);
            trackerObjects.RemoveAt(index);
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

    public void AddTrashThrown(int trash)
    {
        totalThrownTrash += trash;
        thrownTrash += trash;
    }

    public void RemoveTrashThrown(int trash)
    {
        thrownTrash -= trash;
    }

    public int TrashThrownCount()
    {
        return thrownTrash;
    }
}
