using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody playerRigidBody;
    private PlayerInputActions playerInputActions;
    private TrashTracker trashTracker;
    private List<Trash> itemsHeld;
    private List<GameObject> items;

    private bool floorCollision;
    private int jumpCount;
    private Vector2 mouseMovement;

    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private int grabAmount = 1;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        trashTracker = FindObjectOfType<TrashTracker>();
        itemsHeld = new List<Trash>();
        items = new List<GameObject>();

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerInputActions.Player.Disable();
        Events.OnGameStart += EventsOnGameStart;
        Events.OnWin += EventsOnWin;
    }

    private void EventsOnGameStart()
    {
        Debug.Log("Game started — enabling input!");
        playerInputActions.Player.Enable();
        playerInputActions.Player.Look.performed += LookPerformed;
    }

    private void EventsOnWin()
    {
        playerInputActions.Player.Look.performed -= LookPerformed;
        playerInputActions.Player.Jump.performed -= JumpPerformed;
        playerInputActions.Player.Hold.performed -= HoldPerformed;
        playerInputActions.Player.Craft.performed -= CraftPerformed;
        playerInputActions.Player.Place.performed -= PlacePerformed;

        playerInputActions.Player.Disable();

        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Vector2 input2D = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Debug.Log($"Movement input: {input2D}");

        Vector3 moveDirection = transform.TransformDirection(new Vector3(input2D.x, 0, input2D.y));
        playerRigidBody.AddForce(moveDirection * speed, ForceMode.Force);
        playerRigidBody.linearDamping = 0f;
        playerRigidBody.angularDamping = 0.05f;
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += JumpPerformed;
        playerInputActions.Player.Hold.performed += HoldPerformed;
        playerInputActions.Player.Craft.performed += CraftPerformed;
        playerInputActions.Player.Place.performed += PlacePerformed;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Jump.performed -= JumpPerformed;
        playerInputActions.Player.Hold.performed -= HoldPerformed;
        playerInputActions.Player.Craft.performed -= CraftPerformed;
        playerInputActions.Player.Place.performed -= PlacePerformed;
        playerInputActions.Player.Look.performed -= LookPerformed;
        playerInputActions.Player.Disable();
    }

    private void LookPerformed(InputAction.CallbackContext context)
    {
        mouseMovement = context.ReadValue<Vector2>();
        Debug.Log(mouseMovement);
        transform.Rotate(0f, mouseMovement.x * mouseSensitivity * Time.deltaTime, 0f);
    }

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        if ((floorCollision || jumpCount <= 1) && context.performed)
        {
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }

    private void HoldPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Hold performed");
        if (context.ReadValue<float>() == 1)
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, interactDistance, pickUpLayerMask))
            {
                if (hit.transform.TryGetComponent(out Trash trash) && itemsHeld.Count < grabAmount)
                {
                    trash.Grab(objectGrabPointTransform);
                    itemsHeld.Add(trash);
                    trashTracker.HoldTrashTransform(trash.transform);
                }
            }
        }
        else if (itemsHeld.Count > 0)
        {
            itemsHeld[0].Drop();
            trashTracker.DropTrashTransform(itemsHeld[0].transform);
            itemsHeld.RemoveAt(0);
        }
    }

    private void CraftPerformed(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
        Time.timeScale = 0f;
        playerInputActions.Player.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void PlacePerformed(InputAction.CallbackContext obj)
    {
        if (items.Count > 0 && transform.position.y < 7)
        {
            if (items[0].TryGetComponent(out HoldManager holdManager))
            {
                holdManager.Drop();
                items.RemoveAt(0);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            floorCollision = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            floorCollision = false;
        }
    }

    public Vector2 GetMouseMovement()
    {
        return mouseMovement;
    }
    
    public PlayerInputActions GetInputActions()
    {
        return playerInputActions;
    }
    
    public Transform GetGrabPointTransform()
    {
        return objectGrabPointTransform;
    }
    
    public void AddGameObject(GameObject obj)
    {
        items.Add(obj);
    }
}
