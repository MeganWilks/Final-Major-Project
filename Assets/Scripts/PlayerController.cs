using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]

    [SerializeField] private  GameObject player;

    [SerializeField] private float speedSprinting;
    [SerializeField] private float speedWalking;
    [SerializeField] private float speedCrouching;
    [SerializeField] private float speed;

    [SerializeField] private Vector3 crouchingSize;
    [SerializeField] private Vector3 playerSize;
  
    [SerializeField] private int attackPower;
    [SerializeField] private int healthValue = 3;
    [SerializeField] private Camera camera_;

    [Header("IsPlayer...")]

    [SerializeField] private bool isJumping;
    [SerializeField] private bool isCrouching;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isSprinting;
    [SerializeField] private bool isMoving;

    [SerializeField] private float xPos;
    [SerializeField] private float yPos;

    [SerializeField] private CapsuleCollider capsuleColliders;
    public CharacterController cc;
    void Start()
    {
        speed = speedWalking;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        
    }

    // Update is called once per frame
    void Update()
    {
        #region playerControllerInput

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = transform.forward * vertical;
        Vector3 right = transform.right * horizontal;
        cc.SimpleMove ((forward + right) * speed);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = speedSprinting;
            isSprinting = true;
        }

        else if (Input.GetKey(KeyCode.E))
        {
            isCrouching = true;
            isSprinting = false;
            speed = speedCrouching;
            player.transform.localScale = crouchingSize;
        }
        else
        {
            isSprinting = false;
            isCrouching = false;
            speed = speedWalking;
            player.transform.localScale = playerSize;
        }

        isMoving = cc.velocity.sqrMagnitude > 0 ? true : false; // If value is greater than 0 then is True, if less then false ternary conditional operator
        #endregion


    }
}
