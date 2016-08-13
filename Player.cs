using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public delegate void DeadEventHandler();

public class Player : Character
{
    public event DeadEventHandler Dead;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private float immortalTime;
    [SerializeField]
    private float climbSpeed;

    private GameObject[] pauseObjects;
    private Vector2 startPos;
    private SpriteRenderer spriteRenderer;
    private bool immortal = false;
    private static Player instance;
    private bool isDead;
    private IUseable useable;
    private Transform currentLocation;

    public bool OnLadder { get; set; }
    public bool OnHeart { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }
    public Rigidbody2D MyRigidbody { get; set; }
    

    public static Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    public override bool IsDead
    {
        get
        {
            if (healthStat.CurrentVal <= 0)
            {
                OnDead();
            }
            return healthStat.CurrentVal <= 0;
        }


    }
   
    private void Awake()
    {
        healthStat.Initialize();
    }
   
    public override void Start ()
    {
        base.Start();
        OnLadder = false;
        OnHeart = false;
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        MyRigidbody = GetComponent<Rigidbody2D>();
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();

	}
    

    
	void Update ()
    {
       

        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -40f)
            {
                Death();
            }
        }
        HandleInput();
	}

    void FixedUpdate()
    {
        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            OnGround = IsGrounded();
            HandleMovement(horizontal, vertical);
            Flip(horizontal);
           
        }
    }

    private void HandleMovement(float horizontal, float vertical)
    {
        if(MyRigidbody.velocity.y < 0)
        {
            MyAnimator.SetBool("land", true);
        }
        if(!Attack)
        {
      
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }
        if (Jump && MyRigidbody.velocity.y == 0 && !OnLadder)
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        if (OnLadder)
        {
            MyAnimator.speed = vertical != 0 ? Mathf.Abs(vertical) : Mathf.Abs(horizontal);
            MyRigidbody.velocity = new Vector2(horizontal * climbSpeed, vertical * climbSpeed);
        }
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !OnLadder)
        {
            MyAnimator.SetTrigger("jump");
           
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            MyAnimator.SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
          
            Use();
        }
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                hidePaused();
            }
             
        }
        
       
    }

    private void showPaused()
    {
        foreach(GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    private void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }
   
    private void Flip (float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }
    private bool IsGrounded()
    {
        if(MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for(int i=0; i< colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator IndicateImmortal()
    {
        while(immortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }

    
    public override IEnumerator TakeDamage()
    {
        if(!immortal)
        {
            healthStat.CurrentVal -= 10;
            if(!IsDead)
            {
                MyAnimator.SetTrigger("damage");
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);

                immortal = false;
            }
            else
            {
                
                MyAnimator.SetTrigger("dead");
                Death();
                

            }
        }
    }
    
    public override void Death()
    {
        MyRigidbody.velocity = Vector2.zero;
        MyAnimator.SetTrigger("idle");
        healthStat.CurrentVal = healthStat.MaxVal;
        transform.position = startPos;
    }

   

    private void OnDead()
    {
        if(Dead != null)
        {

            Dead();   
        }
    }


    private void Use()
    {
        if (useable != null)
        {
            useable.Use();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.tag == "Heart")
        {
            healthStat.CurrentVal += 100;
            startPos = other.gameObject.transform.position;
            
        }

       
        if (other.tag == "EvilPencil")
        {
            healthStat.CurrentVal -= 100;
            Death();
        }
         
      
        if (other.tag == "Useable")
        {
            useable = other.GetComponent<IUseable>();
        }
        if (other.tag == "BossRoom")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level2BossRoom");
        }
        if (other.tag == "MovingPlatform")
        {
            currentLocation = transform.parent;
            transform.parent = other.transform;
        }
       

        base.OnTriggerEnter2D(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Useable")
        {
            useable = null;
        }
        if (other.tag == "MovingPlatform")
        {
            transform.parent = currentLocation;
        }
    }

   



}
