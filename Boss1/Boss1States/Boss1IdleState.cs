using UnityEngine;
using System.Collections;

public class Boss1IdleState : IBoss1State {

    private Boss1 boss;
    private float idleTimer;
    private float idleDuration;

    public void Enter(Boss1 boss)
    {
        if (GameObject.Find("DRAGONBOSS"))
        {
            idleDuration = 4;
            this.boss = boss;
        }
        else
        {
            idleDuration = 10;
            this.boss = boss;
        }
    }

    public void Execute()
    {
        Idle();
        if (boss.Target != null)
        {
            boss.ChangeState(new Boss1PatrolState());
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
        boss.MyAnimator.SetFloat("speed", 0);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            boss.ChangeState(new Boss1PatrolState());
        }
    }
}
