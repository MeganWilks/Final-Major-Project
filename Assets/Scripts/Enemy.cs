using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    [Header("Nav Mesh Agent")]
    [SerializeField] public NavMeshAgent navMeshAgent;
    [SerializeField] public Transform player;
    [SerializeField] public LayerMask isGround, isPlayer;

    [Header("NPC Patrolling")]
    [SerializeField] public Vector3 walkingPoint;
    [SerializeField] public bool isWalkingPointSet;
    [SerializeField] public float walkingPointRange;

    [Header("NPC Attacking")]
    [SerializeField] public float timeBetweenAttack;
    [SerializeField] public bool isAttacking;

    [Header("NPC States")]
    [SerializeField] public float NPCVisionRange, NPCAttackRange = 0;
    [SerializeField] public bool isPlayerInVisionRange, isPlayerInAttackRange;

    [SerializeField] public GameObject projectile;

    [Header("NPC Health")]
    [SerializeField] public int NPChealth;


    private void Awake()
    {
        player = GetComponent<GameObject>().transform;
        //player = GameObject.Find("PlayerObj").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Checking Sight and Attack Range
        isPlayerInVisionRange = Physics.CheckSphere(transform.position, NPCVisionRange, isPlayer);
        isPlayerInAttackRange = Physics.CheckSphere(transform.position, NPCAttackRange, isPlayer);

        if (!isPlayerInVisionRange && !isPlayerInAttackRange)
        {
            NPCPatrolling();
        }

        if (isPlayerInVisionRange && !isPlayerInAttackRange)
        {
            NPCChasePlayer();
        }

        if (isPlayerInVisionRange && isPlayerInAttackRange)
        {
            NPCAttackingPlayer();
        }

    }

    private void NPCPatrolling()
    {
        if (!isWalkingPointSet)
        {
            FindingWalkPoint();
        }

        if (isWalkingPointSet)
        {
            navMeshAgent.SetDestination(walkingPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkingPoint;

        //When walkingPoint is reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            isWalkingPointSet = false;
        }
    }

    private void FindingWalkPoint()
    {
        // Calculate random point in range#
        float randomZAxis = Random.Range(-walkingPointRange, walkingPointRange);
        float randomXAxis = Random.Range(-walkingPointRange, walkingPointRange);

        walkingPoint = new Vector3(transform.position.x + randomXAxis, transform.position.y, transform.position.z + randomZAxis);

        if (Physics.Raycast(walkingPoint, -transform.up, 2f, isGround))
        {
            isWalkingPointSet = true;
        }

        else
        {
            FindingWalkPoint();
        }

    }

    private void NPCChasePlayer()
    {

        navMeshAgent.SetDestination(player.position);

    }
    private void NPCAttackingPlayer()
    {
        // Make Sure Enemy doesnt move when attacking
        navMeshAgent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!isAttacking)
        {
            //ADD ATTACK CODE
            Rigidbody rigidbody = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rigidbody.AddForce(transform.forward * 5f, ForceMode.Impulse);
           


            isAttacking = true;
            Invoke(nameof(ResetAttacking), timeBetweenAttack);
        }
    }

    private void ResetAttacking()
    {
        isAttacking = false;

    }

    public void Damage(int damage)
    {
        NPChealth -= damage;

        if (NPChealth <= 0) Invoke(nameof(DestroyEnemy),0.5f);





    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}

