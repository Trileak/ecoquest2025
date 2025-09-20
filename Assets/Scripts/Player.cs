using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    private Rigidbody playerRigidBody;               // Player rigid body (adds physics)
    private PlayerInputActions playerInputActions;   // Player input actions
    private bool floorCollision;                     // Floor check
    private int jumpCount;                           // Double jump check
    private Vector2 mouseMovement;                   // Movement of mouse (to add camera movement)
    private Trash itemHeld;                // The script of the item held

    [SerializeField] private float mouseSensitivity;         // Sensitivity of the mouse
    [SerializeField] private float jumpForce        = 5f;    // The jump force
    [SerializeField] private float speed            = 10f;   // Movement speed
    [SerializeField] private float interactDistance = 2f;    // Interact distance
    [SerializeField] private Transform cameraTransform;      // The camera transform (for interacting stuff)
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    private void Awake()
    {
        playerRigidBody    = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        Vector2 input2D    = playerInputActions.Player.Movement.ReadValue<Vector2>();
        playerRigidBody.AddRelativeForce(new Vector3(input2D.x, 0, input2D.y) * speed, ForceMode.Force);   // Adds force
        playerRigidBody.drag        = 0f;                                             // Linear drag
        playerRigidBody.angularDrag = 0.05f;                                          // Rotational drag
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();                                // Enables the player
        playerInputActions.Player.Jump.performed     += JumpPerformed;     // }
        playerInputActions.Player.Movement.performed += MovementPerformed; // } Add subscribers to the events
        playerInputActions.Player.Look.performed     += LookPerformed;     // }
        playerInputActions.Player.Hold.performed     += HoldPerformed;     // }
        playerInputActions.Player.Craft.performed    += CraftPerformed;    // }
        playerInputActions.Player.Place.performed    += PlacePerformed;    // }
    }

    private void PlacePerformed(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void CraftPerformed(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
        Time.timeScale = 0f;
        playerInputActions.Player.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    private void ExitPerformed(InputAction.CallbackContext obj)
    {
        SceneManager.UnloadSceneAsync("Shop");
        Time.timeScale = 1f;
        playerInputActions.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Jump.performed     -= JumpPerformed;     // } 
        playerInputActions.Player.Movement.performed -= MovementPerformed; // } Removes subscribers to the events
        playerInputActions.Player.Look.performed     -= LookPerformed;     // }
        playerInputActions.Player.Hold.performed     -= HoldPerformed;     // }
        playerInputActions.Player.Disable();                               // Disables the player
    }

    private void HoldPerformed(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 1) // If E is pressed (or left mouse button)
        { // If something is in front of the camera vvv
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit raycastHit, interactDistance, pickUpLayerMask)) {
                if (raycastHit.transform.TryGetComponent(out Trash trash))
                { // And has the component objectGrabbable ^^^
                    trash.Grab(objectGrabPointTransform); // Call the grab function with the grab point
                    itemHeld = trash; // And put it as the item held
                }
            }
        }
        else // If Q is pressed (or right mouse button)
        {
            if (itemHeld)
            {
                itemHeld.Drop();
            }
        }
    }


    private void MovementPerformed(InputAction.CallbackContext context)
    {
        Vector2 inputVec2D = context.ReadValue<Vector2>();
        Vector3 localInput = new Vector3(inputVec2D.x, 0f, inputVec2D.y);
        Vector3 worldDir   = transform.TransformDirection(localInput);
        playerRigidBody.AddForce(worldDir * speed, ForceMode.Force);
    }

    private void LookPerformed(InputAction.CallbackContext context)
    {
        mouseMovement = context.ReadValue<Vector2>();
        transform.Rotate(0f, mouseMovement.x / 50f, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            floorCollision = true;
            jumpCount      = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            floorCollision = false;
    }

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        if ((floorCollision || jumpCount <= 1) && context.performed)
        {
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++; // ++ is the same thing as += 1
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
}
