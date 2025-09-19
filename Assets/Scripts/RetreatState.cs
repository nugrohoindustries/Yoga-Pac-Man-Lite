using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : EnemyState
{
    public RetreatState(EnemyAI enemy) : base(enemy) { }
	
	private float footstepTimer = 0f;

    public override void Enter()
    {
        enemy.animator.SetTrigger("Retreat");
        enemy.Agent.speed *= enemy.retreatSpeedMultiplier;
    }

    public override void UpdateLogic()
    {
        Vector3 directionAway = (enemy.transform.position - enemy.player.position).normalized;
        Vector3 retreatTarget = enemy.transform.position + directionAway * enemy.retreatDistance;

        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(retreatTarget, out hit, enemy.retreatDistance, UnityEngine.AI.NavMesh.AllAreas))
        {
            enemy.Agent.destination = hit.position;
        }

        if (!enemy.playerHasPowerUp)
        {
            enemy.Agent.speed /= enemy.retreatSpeedMultiplier;
            enemy.ChangeState(new PatrolState(enemy));
        }
		
		footstepTimer += Time.deltaTime;
    if (footstepTimer >= enemy.footstepInterval)
    {
        PlayFootstep();
        footstepTimer = 0f;
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

    public override void Exit()
    {
        
        if (enemy.Agent.speed > 0)
            enemy.Agent.speed /= enemy.retreatSpeedMultiplier;
    }
}
