using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Player player;      // The player
    private float cameraY = 0f; // Tracks vertical rotation

    private void Start()
    {
        player = FindObjectOfType<Player>(); // Finds player
    }

    private void Update()
    {
        Vector2 mouseMovement = player.GetMouseMovement();
        float mouseY = mouseMovement.y;
        cameraY -= mouseY / 40f;
        cameraY = Mathf.Clamp(cameraY, -90f, 90f);

        transform.localEulerAngles = new Vector3(cameraY, 0f, 0f);
    }
}