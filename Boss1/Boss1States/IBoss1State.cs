using UnityEngine;
using System.Collections;

public interface IBoss1State 

{

    void Execute();
    void Enter(Boss1 enemy);
    void Exit();
    void OnTriggerEnter(Collider2D other);
}
