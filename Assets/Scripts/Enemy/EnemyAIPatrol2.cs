using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyAIPatrol2 : MonoBehaviour
{

    [Header("NavMeshAgent")]
    [SerializeField] public NavMeshAgent navMeshAgent;

    [Header("Enemy MovementTime")]
    [SerializeField] public float startWaitTime = 4;
    [SerializeField] public float rotateTime = 2;

    [Header("Enemy Speeds")]
    [SerializeField] public float walkSpeed = 6;
    [SerializeField] public float runSpeed = 9;

    [Header("Enemy View")]
    [SerializeField] public float radiusView = 15;
    [SerializeField] public float angleView;

    [Header("LayerMasks")]
    [SerializeField] public LayerMask playerMask;
    [SerializeField] public LayerMask obstaclMask;
    
    [Header("LayerMask Settings")]
    [SerializeField] float meshResolution = 1f;
    [SerializeField] public int edgeIterations = 4;
    [SerializeField] public float edgeDistance = 0.5f;

    [Header("Waypoints")]
    [SerializeField] public Transform[] waypoints;
    [SerializeField] public int currentWaypointIndex;

    [Header("Player Position")]
    [SerializeField] public Vector3 playerLastPos = Vector3.zero;
    [SerializeField] public Vector3 playerPos;

    [Header("Conditions + Other")]

    [SerializeField] public bool m_playerInRange;
    [SerializeField] public bool m_playerIsNear;
    [SerializeField] public bool m_isPatrol;
    [SerializeField] public bool m_playerIsCaught;

    [SerializeField] public float m_WaitTime;
    [SerializeField] public float m_TimeToRotate;




    void Start()
    {
        playerPos = Vector3.zero;
        m_isPatrol = true;
        m_playerIsCaught = false;
        m_playerInRange = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = rotateTime;

        currentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = walkSpeed;
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);

    }

    void Update()
    {
        EnvironmentView();
        if(!m_isPatrol)
        {
            Chasing();
        }
        else
        {
            Patrolling();
        }
    }


    public void Chasing()
    {
        m_playerIsNear = false;
        playerLastPos = Vector3.zero;

        if(!m_playerIsCaught)
        {
            Move(runSpeed);
            navMeshAgent.SetDestination(playerPos);
        }

        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if(m_WaitTime <= 0 && !m_playerIsCaught && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position)>=6f)
            { 
                m_isPatrol = true;
                m_playerIsNear = false;
                Move(walkSpeed);
                m_TimeToRotate = rotateTime;
                m_WaitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);

            }
            else
            {
                if(Vector3.Distance(transform.position,GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
            
            
    }

    public void Patrolling()
    {
        if(m_playerIsNear)
        {
            if(m_TimeToRotate <= 0)
            {
                Move(walkSpeed);
                LookingPlayer(playerLastPos);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_playerIsNear = false;
            playerLastPos = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
            if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if(m_WaitTime <= 0)
                {
                    NextPos();
                    Move(walkSpeed);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
        
    }

    public void playerCaught()
    {
        m_playerIsCaught = true;

    }

    public void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    public void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0; 
    }

    public void NextPos()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
    }


    public void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) < +0.3)
        {
            if (m_WaitTime <= 0)
            {
                m_playerIsNear = false;
                Move(walkSpeed);
                navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = rotateTime;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        } 
    }

   public void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, radiusView, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToPlayer) < angleView / 2)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstaclMask))
                {
                    m_playerInRange = true;
                    m_isPatrol = false;
                }
                else
                {
                    m_playerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > radiusView)
            {
                m_playerInRange = false;
            }


            if (m_playerInRange)
            {
                playerPos = player.transform.position;
            }
        }
    }


}
