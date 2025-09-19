using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PatrolState : EnemyState
{
    private enum PatrolSubState { Idle, Moving }
    private PatrolSubState subState;

    private float idleTimer;
    private float idleDuration = 2f; // nunggu player
	
	private float footstepTimer = 0f;

    public PatrolState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        SetSubState(PatrolSubState.Idle);
    }

    public override void UpdateLogic()
    {
        switch (subState)
        {
            case PatrolSubState.Idle:
                IdleBehaviour();
                break;

            case PatrolSubState.Moving:
                MoveBehaviour();
                break;
        }

       
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (enemy.playerHasPowerUp)
        {
            enemy.ChangeState(new RetreatState(enemy));
        }
        else if (distanceToPlayer <= enemy.chaseDistance)
        {
            enemy.ChangeState(new ChaseState(enemy));
        }
    }

    public override void Exit()
    {
        // keluar gem
    }

    private void SetSubState(PatrolSubState newSubState)
    {
        subState = newSubState;

        switch (subState)
        {
            case PatrolSubState.Idle:
                idleTimer = 0f;
                enemy.animator.SetTrigger("Idle");
				enemy.animator.SetBool("IsMoving", false);
                enemy.Agent.ResetPath();
                break;

            case PatrolSubState.Moving:
                ChooseNewWaypoint();
				enemy.animator.SetBool("IsMoving", true);
                enemy.animator.SetTrigger("Walk");
                break;
        }
    }

    private void IdleBehaviour()
    {
        idleTimer += Time.deltaTime;
	//	enemy.Agent.ResetPath();
        enemy.animator.SetBool("IsMoving", false);
        if (idleTimer >= idleDuration)
        {
            SetSubState(PatrolSubState.Moving);
        }
    }

    private void MoveBehaviour()
    {
	//	ChooseNewWaypoint();
		footstepTimer += Time.deltaTime;
		if (footstepTimer >= enemy.footstepInterval)
		{
			PlayFootstep();
			footstepTimer = 0f;
		}
        enemy.animator.SetBool("IsMoving", true); 
        if (!enemy.Agent.pathPending && enemy.Agent.remainingDistance < enemy.waypointTolerance)
        {
            SetSubState(PatrolSubState.Idle);
        }
    }
	
	private void PlayFootstep()
	{
    if (enemy.audioSource != null && enemy.footstepClips.Count > 0)
		{
			int index = Random.Range(0, enemy.footstepClips.Count);
			enemy.audioSource.PlayOneShot(enemy.footstepClips[index]);
		}
	}

    private void ChooseNewWaypoint()
    {
        if (enemy.waypoints.Count == 0) return;

        int newIndex = Random.Range(0, enemy.waypoints.Count);
        if (newIndex == enemy.currentWaypointIndex)
            newIndex = (newIndex + 1) % enemy.waypoints.Count;

        enemy.currentWaypointIndex = newIndex;
        enemy.Agent.destination = enemy.waypoints[enemy.currentWaypointIndex].position;
    }
}
