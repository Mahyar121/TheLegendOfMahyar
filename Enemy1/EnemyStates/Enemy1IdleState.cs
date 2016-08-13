using UnityEngine;
using System.Collections;
using System;

public class Enemy1IdleState : IEnemy1State

{
    private Enemy1 enemy;
    private float idleTimer;
    private float idleDuration;

    public void Enter(Enemy1 enemy)
    {
        idleDuration = UnityEngine.Random.Range(1, 10);
        this.enemy = enemy;
    }

    public void Execute()
    {
        Idle();
        if (enemy.Target != null)
        {
            enemy.ChangeState(new Enemy1PatrolState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
     
    }

    private void Idle()
    {
        enemy.MyAnimator.SetFloat("speed", 0);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new Enemy1PatrolState());
        }
    }

    
}
