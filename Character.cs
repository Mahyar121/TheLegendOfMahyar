using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour
{

    [SerializeField]
    private List<string> damageSources;
    [SerializeField]
    protected Stat healthStat;
    [SerializeField]
    protected float movementSpeed;
    [SerializeField]
    private EdgeCollider2D swordCollider;

    public abstract void Death();
    public abstract IEnumerator TakeDamage(); // abstract means that the classes that inherit this, need to implement their own function
    public abstract bool IsDead { get; }
    public bool Attack { get; set; }
    public Animator MyAnimator { get; set; }
    public bool TakingDamage { get; set; }

    public EdgeCollider2D SwordCollider
    {
        get
        {
            return swordCollider;
        }

    }

    protected bool facingRight;

	// Use this for initialization
	public virtual void Start ()
    {

        facingRight = true;
        MyAnimator = GetComponent<Animator>();
        healthStat.Initialize();
	}

    public virtual void ChangeDirection()
    {
        facingRight = !facingRight;
        
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        
        
    }

    public void MeleeAttack()
    {
        SwordCollider.enabled = true;
    }


	public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }
}
