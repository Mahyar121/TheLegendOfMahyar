using UnityEngine;
using System.Collections;

public interface IEnemy1State
{
    void Execute();
    void Enter(Enemy1 enemy);
    void Exit();
    void OnTriggerEnter(Collider2D other);



}
	