using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public FSM stateMachine;
    public D_temporayEntity entityData;
    
    public int facingDirection { get; private set; } //is useless for now but maybe useful
    public Rigidbody rb { get; private set; }
    public RaycastHit rayHit;
    public HealthGUI healthBar;
    public Animator anim { get; private set; }
    public GameObject aliveGameObject { get; private set; } //la un truc spécifique au tuto à enlever après peut être, parce que juste un game object qui référence un objet dans la scène en dessous du monstre
    public FieldOfView minFieldOfView { get; private set; } //maybe two of them for min and max agro range
    public FieldOfView maxFieldOfView { get; private set; } //maybe two of them for min and max agro range
    public animationToStateMachine atsm { get; private set; }
    private Vector3 velocityWorkSpace;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;
    protected bool isStunned;
    protected bool isDead;
    public int lastDamageDirection { get; private set; }
    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;

    public delegate void ActionEntity(Entity e);
    public event ActionEntity OnHit;
    public event ActionEntity OnDead;

    CharacterController characterController;

    public virtual void Start()
    {
        facingDirection = 1;
        currentHealth = entityData.maxHealth;
        healthBar.setMaxHealthPoints(entityData.maxHealth);
        isStunned = false;
        aliveGameObject = transform.Find("Alive").gameObject;
        characterController = aliveGameObject.GetComponent<CharacterController>();
        rb = aliveGameObject.GetComponent<Rigidbody>();
        anim = aliveGameObject.GetComponent<Animator>();
        atsm = aliveGameObject.GetComponent<animationToStateMachine>();
        stateMachine = new FSM();
        currentStunResistance = entityData.stunResistance;
        FieldOfView[] fieldOfViews = aliveGameObject.GetComponents<FieldOfView>();
        foreach(FieldOfView fieldOfView in fieldOfViews)
        {
            if(fieldOfView.fieldOfViewMinOrMax==FieldOfViewMinOrMax.isMinFieldOfView)
            {
                this.minFieldOfView = fieldOfView;
            } else if (fieldOfView.fieldOfViewMinOrMax == FieldOfViewMinOrMax.isMaxFieldOfView)
            {
                this.maxFieldOfView = fieldOfView;
            }
        }

    }

    public virtual void Update()
    {
        stateMachine.currentState.logicUpdate();

        if(Time.time>=lastDamageTime+entityData.stunRecoveryTime)
        {
            resetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.physicsUpdate();
    }

    public virtual void setVelocity(float velocity)
    {
        velocityWorkSpace = rb.transform.forward*velocity;
        rb.velocity = velocityWorkSpace;

        //characterController.Move(velocityWorkSpace * Time.deltaTime);
    }

    public virtual bool checkWall()
    {
        return Physics.Raycast(wallCheck.position, aliveGameObject.transform.forward, entityData.wallCheckDistance, entityData.whatisground);
    }

    public virtual bool checkLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position,Vector2.down, entityData.ledgeCheckDistance, entityData.whatisground);

    }

    public virtual bool checkPlayerInMinRangeAgro()
    {
        this.minFieldOfView.FindVisibleTargets();
        //Debug.Log("minFieldOfView");
        //Debug.Log(minFieldOfView.visibleTargets.Count);
        return minFieldOfView.visibleTargets.Count > 0;
    }

    public virtual bool checkPlayerInMaxRangeAgro()
    {
        this.maxFieldOfView.FindVisibleTargets();
       //Debug.Log("maxFieldOfView");
        //Debug.Log(maxFieldOfView.visibleTargets.Count);
        return maxFieldOfView.visibleTargets.Count > 0;
    }

    public virtual bool checkPlayerInCloseRangeAction() //check juste devant lui
    {
        //TODO : à transformer en field of view ici
        return Physics.Raycast(playerCheck.position, aliveGameObject.transform.forward, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }
    public virtual bool checkGround()
    {
        return Physics.OverlapSphere(groundCheck.position, entityData.groundCheckRadius, entityData.whatisground).Length>0;
    }
    public virtual void flip()
    {
        aliveGameObject.transform.Rotate(0f, 180f, 0f);
    }
    public virtual void Rotate(int angle)
    {
        aliveGameObject.transform.Rotate(0f, 0f, angle); // Je suppose qu'on va tourner par rapport à l'axe z   
    }

    public virtual void RotateToFacePlayer()
    {
        if(minFieldOfView.visibleTargets.Count>0 || this.maxFieldOfView.visibleTargets.Count>0)
        {
            rb.transform.LookAt(maxFieldOfView.visibleTargets[0]);
        }
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkSpace.Set(0, velocity, 0);
        //velocityWorkSpace = -rb.transform.forward * velocity; 
        rb.velocity = velocityWorkSpace;
        //alors la faudrait juste qu'il saute un peu et c'est tout.
    }

    public virtual void resetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;

        currentHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;
       // DamageHop(entityData.damageHopSpeed);
        healthBar.setHealth(currentHealth);
        //TODO : voir pour partiules quand il est endommagé
        //Instantiate(entityData.hitParticule, aliveGameObject.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0, 360)));

        if(currentStunResistance<=0)
        {
            isStunned = true;
        }
        if (currentHealth <= 0)
        {
            isDead = true;
            OnDead?.Invoke(this);
        }
        else
            OnHit?.Invoke(this);
    }

    public void Revive()
    {
        isDead = false;
    }
    public virtual void SetVelocity(float velocity,Vector2 angle,int direction)
    {
        angle.Normalize();
        velocityWorkSpace = direction*rb.transform.forward * velocity;
        rb.velocity = velocityWorkSpace;
    }
}

