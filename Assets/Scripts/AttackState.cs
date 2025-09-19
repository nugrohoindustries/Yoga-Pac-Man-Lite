using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    private float attackDuration = 1.5f; 
    private float timer;

    public AttackState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        timer = 0f;
        enemy.Agent.isStopped = true; 
        enemy.animator.SetTrigger("Attack");
	//	PlayAttack();
        enemy.Invoke(nameof(enemy.EnableAttackHitbox), 0.5f);
    }

    public override void UpdateLogic()
    {
        timer += Time.deltaTime;

        if (timer >= attackDuration)
        {
            
            enemy.ChangeState(new NearIdleState(enemy));
        }
    }
	
	public void DealDamage()
	{
    if (enemy.player != null)
    {
		
        Karakter ph = enemy.player.GetComponent<Karakter>();
        if (ph != null)
        {
            ph.TakeDamage(1);
        }
    }
	}
	
	public void PlayAttack()
    {
        if (enemy.attackClip != null && enemy.audioSource != null)
            enemy.audioSource.PlayOneShot(enemy.attackClip);
    }

    public override void Exit()
    {
        enemy.Agent.isStopped = false;
    }
}