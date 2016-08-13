using UnityEngine;
using System.Collections;

public class Boss1PatrolState : IBoss1State {

    private Boss1 boss;
    private float patrolTimer;
    private float patrolDuration;

    public void Enter(Boss1 boss)
    {
        
        patrolDuration = 10;
        this.boss = boss;
    }

    public void Execute()
    {
        Patrol();
        boss.Move();
        if (boss.Target != null && boss.InMeleeRange)
        {
            boss.ChangeState(new Boss1MeleeState());
        }



    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Player")
        {
            boss.Target = Player.Instance.gameObject;
        }
        if (other.tag == "Edge")
        {
            boss.ChangeDirection();
        }
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolDuration)
        {
            boss.ChangeState(new Boss1PatrolState());
        }
    }
}
