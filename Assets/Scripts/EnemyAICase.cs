using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAICase : MonoBehaviour
{
    public enum EnemyState { Patrol, Chase, Retreat }

    [Header("Musuh")]
    public Transform player;
    public List<Transform> waypoints;
    public Renderer enemyRenderer;

    [Header("Setingan")]
    public float chaseDistance = 8f;
    public float retreatDistance = 5f;
    public float waypointTolerance = 1f;
    public float retreatSpeedMultiplier = 1.5f;

    [Header("State")]
    public EnemyState currentState = EnemyState.Patrol;

    private NavMeshAgent agent;
    private int currentWaypointIndex;
    private bool playerHasPowerUp = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (enemyRenderer != null)
            enemyRenderer.material.color = Color.red;

        currentState = EnemyState.Patrol;
        ChooseNewWaypoint();
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                Chase();
                break;

            case EnemyState.Retreat:
                Retreat();
                break;
        }
    }

   
    private void Patrol()
    {
        if (waypoints.Count == 0) return;

        if (!agent.pathPending && agent.remainingDistance < waypointTolerance)
        {
            ChooseNewWaypoint();
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (playerHasPowerUp)
        {
            ChangeState(EnemyState.Retreat);
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            ChangeState(EnemyState.Chase);
        }
    }

    private void ChooseNewWaypoint()
    {
        int newIndex = Random.Range(0, waypoints.Count);

        
        if (newIndex == currentWaypointIndex)
            newIndex = (newIndex + 1) % waypoints.Count;

        currentWaypointIndex = newIndex;
        agent.destination = waypoints[currentWaypointIndex].position;
    }

   
    private void Chase()
    {
        agent.destination = player.position;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (playerHasPowerUp)
        {
            ChangeState(EnemyState.Retreat);
        }
        else if (distanceToPlayer > chaseDistance * 2f)
        {
            ChangeState(EnemyState.Patrol);
        }
    }

   
    private void Retreat()
    {
        
        Vector3 directionAway = (transform.position - player.position).normalized;
        Vector3 retreatTarget = transform.position + directionAway * retreatDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(retreatTarget, out hit, retreatDistance, NavMesh.AllAreas))
        {
            agent.speed *= retreatSpeedMultiplier;
            agent.destination = hit.position;
        }

        if (!playerHasPowerUp)
        {
            agent.speed /= retreatSpeedMultiplier;
            ChangeState(EnemyState.Patrol);
        }
    }

    
    private void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

    
    public void SetPlayerPowerUp(bool active)
    {
        playerHasPowerUp = active;
    }
}
