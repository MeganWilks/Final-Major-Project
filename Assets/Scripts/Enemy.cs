using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    [Header("Nav Mesh Agent")]
    [SerializeField] public NavMeshAgent navMeshAgent;
    [SerializeField] public NavMeshSurface navMeshSurface;
    [SerializeField] public Transform player;
    [SerializeField] public LayerMask isGround, isPlayer;

    [Header("NPC Patrolling")]
    [SerializeField] public Vector3 walkingPoint;
    [SerializeField] public bool isWalkingPointSet;
    [SerializeField] public float walkingPointRange;

    [Header("NPC Attacking")]
    [SerializeField] public float timeBetweenAttack;
    [SerializeField] public bool isAttacking;
    [SerializeField] public int attackPower;
    [SerializeField] private GameObject currentAttack;

    [Header("NPC States")]
    [SerializeField] public float NPCVisionRange, NPCAttackRange = 0;
    [SerializeField] public bool isPlayerInVisionRange, isPlayerInAttackRange;

    [SerializeField] public GameObject projectile;
    [SerializeField] public GameObject NPCWeapon;

    [Header("NPC Health")]
    [SerializeField] public int NPChealth;



    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshSurface = GetComponent<NavMeshSurface>();
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

        if (isPlayerInVisionRange)
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
            return;
        }

        FindingWalkPoint();
        

    }

    private void NPCChasePlayer()
    {

        navMeshAgent.SetDestination(player.position);

    }
    private void NPCAttackingPlayer()
    {
        // Make Sure Enemy doesnt move when attacking
        //transform.LookAt(player);
        if (!isAttacking)
        {
            currentAttack = Instantiate(NPCWeapon,transform.position,transform.rotation);

            //currentAttack.transform.parent = transform;
 



            //ADD ATTACK CODE
            //Rigidbody rigidbody = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //var direction = new Vector3(player.transform.position.x - rigidbody.position.x, 1, player.transform.position.z - rigidbody.position.z);
            //rigidbody.gameObject.transform.forward = direction;

           // rigidbody.AddForce(transform.forward * attackPower, ForceMode.Impulse);
           // rigidbody.AddForce(transform.up * 2f, ForceMode.Impulse);

            isAttacking = true;
            // Invoke(nameof(ResetAttacking), timeBetweenAttack);
            StartCoroutine(AttackAnimation(currentAttack));
        }
    }


    IEnumerator AttackAnimation (GameObject currentAttack)
    {
        float timer = 0f;
        while(timer < timeBetweenAttack)
        {
            currentAttack.transform.position = transform.forward + transform.position;
            currentAttack.transform.rotation = Quaternion.Euler(0, 0, -90) * transform.rotation;
            currentAttack.transform.position += transform.up;
            timer += Time.deltaTime;
            yield return null;
        }
       
        Destroy(currentAttack);
        isAttacking = false;

    }

    private void ResetAttacking()
    {
        isAttacking = false;

    }

    public void Damage(int damage)
    {

        NPChealth -= damage;
        //NPChealth(Health.Damage(damage));

        if (NPChealth <= 0) Invoke(nameof(DestroyEnemy),0.5f);





    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
        if (currentAttack != null)
        {
            Destroy(currentAttack);
        }

        GameManager.instance.Rooms[GameManager.instance.RoomIndex].enemiesInRoom --;
        
    }
}

