using UnityEngine;
using System.Collections;

public class Boss1MeleeState : IBoss1State {

    private Boss1 boss;
    private float attackTimer;
    private float attackCoolDown = 2;
    private bool canAttack = true;

    public void Enter(Boss1 boss)
    {
        this.boss = boss;
    }

    public void Execute()
    {
        Attack();
        if (boss.Target == null)
        {
            boss.ChangeState(new Boss1IdleState());
        }

    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCoolDown)
        {
            canAttack = true;
            attackTimer = 0;
        }
        if (canAttack)
        {
            canAttack = false;
            boss.MyAnimator.SetTrigger("attack");
        }
    }
}
