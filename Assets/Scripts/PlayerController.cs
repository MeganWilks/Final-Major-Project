using UnityEngine;

/// <summary>
/// This is the Player Controllers Class where it controls the player movement
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Player")]

    [SerializeField] private GameObject player;
    [SerializeField] private CapsuleCollider capsuleColliders;
    public CharacterController CharController; // Character Controller 

    [Header("Player Speeds")]

    [SerializeField] private float speedSprinting;
    [SerializeField] private float speedWalking;
    [SerializeField] private float speedCrouching;
    [SerializeField] private float speed;

    [Header ("Player Sizing")]

    [SerializeField] private Vector3 crouchingSize;
    [SerializeField] private Vector3 playerSize;

    [Header ("Player Stats")]

    [SerializeField] private int attackPower;


    [Header("Camera")]

    [SerializeField] private Camera camera_;

    [Header("IsPlayer Conditions")]

    [SerializeField] private bool isJumping;
    [SerializeField] private bool isCrouching;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isSprinting;
    [SerializeField] private bool isMoving;

    [Header("Position")]
    [SerializeField] private float xPos;
    [SerializeField] private float yPos;

    [Header ("Health_ Variables")]
    [SerializeField] private int healthValue = 3;
 

    void Start()
    {
        SetCursor();

        speed = speedWalking;

        healthValue = 5;
    }

    private void SetCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region playerControllerInput

        PlayerMovement();

        KeyPressedMovement();

        isMoving = CharController.velocity.sqrMagnitude > 0 ? true : false; // If value is greater than 0 then is True, if less then false ternary conditional operator
        #endregion


    }

    private void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward * vertical;
        Vector3 right = transform.right * horizontal;
        CharController.SimpleMove((forward + right) * speed);

    }

    private void KeyPressedMovement()
    {
        #region sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = speedSprinting;
            isSprinting = true;
        }
        #endregion // Sprint


        #region crouch
        else if (Input.GetKey(KeyCode.E))
        {
            isCrouching = true;
            isSprinting = false;
            speed = speedCrouching;
            player.transform.localScale = crouchingSize;
        }

        #endregion // Crouch

        #region no state
        else
        {
            isSprinting = false;
            isCrouching = false;
            speed = speedWalking;
            player.transform.localScale = playerSize;
        }
        #endregion // No State
    }



  
}
