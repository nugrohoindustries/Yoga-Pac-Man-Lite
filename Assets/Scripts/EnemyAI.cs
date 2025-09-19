using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public List<Transform> waypoints;
    public Renderer enemyRenderer;
    public Animator animator;
	public GameObject gemover;
	
	[Header("Audio")]
	public AudioSource audioSource;
	public List<AudioClip> footstepClips;
	public float footstepInterval = 0.5f; // time between steps

    [Header("Settings")]
    public float chaseDistance = 8f;
    public float retreatDistance = 5f;
	public float tangkapDistance = 1f;
    public float waypointTolerance = 1f;
    public float retreatSpeedMultiplier = 1.5f;

    private UnityEngine.AI.NavMeshAgent agent;
    private EnemyState currentState;

    [HideInInspector] public int currentWaypointIndex;
    [HideInInspector] public bool playerHasPowerUp = false;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (enemyRenderer != null)
            enemyRenderer.material.color = Color.red;

        
        ChangeState(new PatrolState(this));
    }

    private void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;

        if (currentState != null)
            currentState.Enter();
    }

    public void SetPlayerPowerUp(bool active)
    {
        playerHasPowerUp = active;
    }

    public UnityEngine.AI.NavMeshAgent Agent => agent;
}