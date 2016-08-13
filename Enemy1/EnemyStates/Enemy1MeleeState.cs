using UnityEngine;
using System.Collections;
using System;

public class Enemy1MeleeState : IEnemy1State

{
    private Enemy1 enemy;
    private float attackTimer;
    private float attackCoolDown = 2;
    private bool canAttack = true;

    public void Enter(Enemy1 enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Attack();
        if (enemy.Target == null)
        {
            enemy.ChangeState(new Enemy1IdleState());
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
            enemy.MyAnimator.SetTrigger("attack");
        }
    }
}
