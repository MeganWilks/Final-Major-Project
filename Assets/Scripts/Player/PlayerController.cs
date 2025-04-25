using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// This is the Player Controllers Class where it controls the player movement
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Player")]

    public static PlayerController instance;
    [SerializeField] private GameObject player;
    [SerializeField] private CapsuleCollider capsuleColliders;
    public CharacterController CharController; // Character Controller 
    private Vector3 movementDirection = Vector3.zero;

    [Header("Player Speeds")]

    [SerializeField] private float speedSprinting;
    [SerializeField] private float speedWalking;
    [SerializeField] private float speedCrouching;
    [SerializeField] private float speed;

    [Header("Player Sizing")]

    [SerializeField] private Vector3 crouchingSize;
    [SerializeField] private Vector3 playerSize;

    [Header("Player Stats")]

    [SerializeField] private int attackPower;

    [Header("Camera")]

    [SerializeField] private Camera camera_;

    [Header("IsPlayer Conditions")]

    [SerializeField] private bool isJumping;
    [SerializeField] private bool isCrouching;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isSprinting;
    [SerializeField] private bool isMoving;

    [SerializeField] private bool MovementEnabled = true;

    [Header("Position")]
    [SerializeField] private float xPos;
    [SerializeField] private float yPos;

    [Header("Health_ Variables")]
    [SerializeField] private int healthValue = 3;

    [Header("Player Animation")]
    [SerializeField] private Animator playerAnimator;

    [Header("Rotation Variables")]
    [SerializeField] private float rotationSpeed = 90f;

    [Header("Inventory Variables")]
    
    [SerializeField] public InventoryManager inventoryManager;

    [Header("Enemy")]
    [SerializeField] public BoxCollider attackCollider;


    void Start()
    {
        instance = this;

        SetCursor();
        

        speed = speedWalking;

        healthValue = 5;
    }

    public void SetAnimator(string state, bool condition)
    {
        playerAnimator.SetBool(state, condition);

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
        PlayerRotation();

        KeyPressedMovement();

        isMoving = movementDirection.sqrMagnitude > 0 ? true : false; // If value is greater than 0 then is True, if less then false ternary conditional operator
        playerAnimator.SetBool("IsWalking", isMoving); // if IsMoving is true then set animator IsWalking to true
        #endregion
    }

    private void PlayerMovement()
    {
        if (!MovementEnabled) return;
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // Normalized means that the Direction is the same but it stays like this a little longer
        movementDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (movementDirection.sqrMagnitude > 0.01f)
        {
            CharController.SimpleMove(movementDirection * speed);
        }
    }

    private void PlayerRotation()
    {
        if (movementDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void AttackEnemy()
    {
        if(isAttacking) return;

        StartCoroutine(AttackingCoroutine());
        
    
    }

    IEnumerator AttackingCoroutine()
    {
        MovementEnabled = false;

        isAttacking = true;
        playerAnimator.SetTrigger("IsAttacking");
        
        yield return new WaitForSeconds(0.2f);

        DealDamage(attackPower);
        yield return new WaitForSeconds(1f);

        isAttacking = false;
        

        MovementEnabled = true;
    }

    public void DealDamage(int damage)
    {
        List<RaycastHit> hits = new List<RaycastHit>();
        hits = Physics.BoxCastAll(transform.position + attackCollider.center, attackCollider.size / 2,transform.forward, Quaternion.identity, 1).ToList();
        foreach(RaycastHit hit in hits)
        {
            if(hit.collider == null) continue;
            if(hit.collider.GetComponent<Enemy>() == null) continue;
            hit.collider.GetComponent<Enemy>().Damage(damage);
        }

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
        else if (Input.GetKey(KeyCode.C))
        {
            isCrouching = true;
            isSprinting = false;
            speed = speedCrouching;
            player.transform.localScale = crouchingSize;
            
        }
        #endregion

        #region Use Item
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //Use Item
            if(inventoryManager.selectedItem != null)
            {
                inventoryManager.selectedItem.Use(this);
            }

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
