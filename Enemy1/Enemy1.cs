using UnityEngine;
using System.Collections;
using System;

public class Enemy1 : Character

{
    private IEnemy1State currentState;
    private Canvas healthCanvas;
    private Vector2 startPos;

    
    [SerializeField]
    private Transform leftEdge;
    [SerializeField]
    private Transform rightEdge;
     
    [SerializeField]
    private float meleeRange;

    public GameObject Target { get; set; }
    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }
    public override bool IsDead
    {
        get
        {
            return healthStat.CurrentVal <= 0;
        }
    }


    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        startPos = transform.position;
        Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
        ChangeState(new Enemy1IdleState());
        healthCanvas = transform.GetComponentInChildren<Canvas>();
	
	}

    void Update()
    {
        if(!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }
            LookAtTarget();
        }
    }
    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new Enemy1PatrolState());
    }

    private void LookAtTarget() // ENEMY SIGHT
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;
            if (xDir > 0 && facingRight || xDir < 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }
    public void ChangeState (IEnemy1State newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter(this);
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public void Move()
    {
        if (!Attack)
        {
            if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                MyAnimator.SetFloat("speed", 1);
                transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
            }
            else if (currentState is Enemy1PatrolState)
            {
                ChangeDirection();
            }
            else if (currentState is Enemy1MeleeState)
            {
                Target = null;
                ChangeState(new Enemy1IdleState());
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }

    public override IEnumerator TakeDamage()
    {
        if(!healthCanvas.isActiveAndEnabled)
        {
            healthCanvas.enabled = true;
        }
        healthStat.CurrentVal -= 10;

        if(!IsDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {

            MyAnimator.SetTrigger("death");
            yield return new WaitForSeconds(3);
            Death();
            yield return null;   
        }
    }

    public override void Death()
    {
        healthCanvas.enabled = false;
        Destroy(gameObject);
    }

    public override void ChangeDirection()
    {
        Transform tmp = transform.FindChild("Enemy1Canvas").transform;
        Vector3 post = tmp.position;
        tmp.SetParent(null);
        base.ChangeDirection();
        tmp.SetParent(transform);
        tmp.position = post;
    }

    


}
