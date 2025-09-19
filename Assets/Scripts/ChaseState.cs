using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(EnemyAI enemy) : base(enemy) { }
	
	private float footstepTimer = 0f;

    public override void Enter()
    {
        enemy.animator.SetTrigger("Chase");
    }

    public override void UpdateLogic()
{
    enemy.Agent.destination = enemy.player.position;

    float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
	
	footstepTimer += Time.deltaTime;
    if (footstepTimer >= enemy.footstepInterval)
    {
        PlayFootstep();
        footstepTimer = 0f;
    }

    if (enemy.playerHasPowerUp)
    {
        enemy.ChangeState(new RetreatState(enemy));
    }
    else if (distanceToPlayer <= enemy.tangkapDistance) //baru
    {
        enemy.ChangeState(new NearIdleState(enemy));
		enemy.gemover.SetActive (true);
    }
    else if (distanceToPlayer > enemy.chaseDistance * 1f)
    {
        enemy.ChangeState(new PatrolState(enemy));
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
        // Nothing special yet
    }
}
