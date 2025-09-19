using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearIdleState : EnemyState
{
    public NearIdleState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
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
    }

    public override void Exit()
    {
        // mbuh
    }
}
