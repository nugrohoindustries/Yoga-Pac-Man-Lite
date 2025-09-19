using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearIdleState : EnemyState
{
	private float idleTime = 2f;
    private float timer;
	
    public NearIdleState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
		timer = 0f;
        enemy.Agent.isStopped = true;
    //    enemy.Animator.SetTrigger("Idle");

        
    //    if (enemy.gemover != null)
   //     {
    //        enemy.gemover.SetActive(true);
    //    }
		
        enemy.Agent.ResetPath(); 
        enemy.animator.SetTrigger("NearIdle"); 
    }

    public override void UpdateLogic()
    {
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (enemy.playerHasPowerUp)
        {
            enemy.ChangeState(new RetreatState(enemy));
        }
        else if (distanceToPlayer > enemy.tangkapDistance) 
        {
            enemy.ChangeState(new ChaseState(enemy));
        }
		
		timer += Time.deltaTime;

        
        if (Random.value < 0.3f)
        {
            enemy.ChangeState(new AttackState(enemy));
            return;
        }

        if (timer >= idleTime)
        {
            enemy.ChangeState(new PatrolState(enemy));
        }
    }

    public override void Exit()
    {
        enemy.Agent.isStopped = false;

        if (enemy.gemover != null)
        {
            enemy.gemover.SetActive(false);
        }
    }
}
