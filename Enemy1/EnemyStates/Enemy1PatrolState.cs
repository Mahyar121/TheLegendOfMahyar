using UnityEngine;
using System.Collections;
using System;

public class Enemy1PatrolState : IEnemy1State
{
    private Enemy1 enemy;
    private float patrolTimer;
    private float patrolDuration;

    public void Enter(Enemy1 enemy)
    {
        patrolDuration = UnityEngine.Random.Range(1, 10);
        this.enemy = enemy;
    }

    public void Execute()
    {
        Patrol();
        enemy.Move();
        if (enemy.Target != null && enemy.InMeleeRange)
        {
            enemy.ChangeState(new Enemy1MeleeState());
        }
   
        
     
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.Target = Player.Instance.gameObject;
        }
        if (other.tag == "Edge")
        {
            enemy.ChangeDirection();
        }
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new Enemy1PatrolState());
        }
    }
}
