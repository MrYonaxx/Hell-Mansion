using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FSM stateMachine;
    public D_temporayEntity entityData;
    
    public int facingDirection { get; private set; } //is useless for now but maybe useful
    public Rigidbody rb { get; private set; }
    public RaycastHit rayHit;
    public Animator anim { get; private set; }
    public GameObject aliveGameObject { get; private set; } //la un truc spécifique au tuto à enlever après peut être, parce que juste un game object qui référence un objet dans la scène en dessous du monstre
    public FieldOfView minFieldOfView { get; private set; } //maybe two of them for min and max agro range
    public FieldOfView maxFieldOfView { get; private set; } //maybe two of them for min and max agro range

    private Vector3 velocityWorkSpace;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;
    public string MonsterName;
    public int maxHealth;
    public int power; //Coef de difficulté de l'ennemie
    public virtual void Start()
    {
        facingDirection = 1;

        aliveGameObject = transform.Find("Alive").gameObject;
        rb = aliveGameObject.GetComponent<Rigidbody>();
        anim = aliveGameObject.GetComponent<Animator>();
        stateMachine = new FSM();
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
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.physicsUpdate();
    }

    public virtual void setVelocity(float velocity)
    {
        //TODO: à revoir pour adapter à notre jeu
        velocityWorkSpace = rb.transform.forward*velocity;
        rb.velocity = velocityWorkSpace;
    }

    public virtual bool checkWall()
    {
        //TODO: à revoir pour adapter à notre jeu
        return Physics.Raycast(wallCheck.position, aliveGameObject.transform.forward, entityData.wallCheckDistance, entityData.whatisground);
    }

    public virtual bool checkLedge()
    {
        //TODO: à revoir pour adapter à notre jeu
        return Physics2D.Raycast(ledgeCheck.position,Vector2.down, entityData.ledgeCheckDistance, entityData.whatisground);

    }

    public virtual bool checkPlayerInMinRangeAgro()
    {
        //TODO: à revoir pour adapter la détéction à notre jeu peut être pas un raycast d'ailleurs
        this.minFieldOfView.FindVisibleTargets();
        Debug.Log("minFieldOfView");
        Debug.Log(minFieldOfView.visibleTargets.Count);
        return minFieldOfView.visibleTargets.Count > 0;
        //return Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool checkPlayerInMaxRangeAgro()
    {
        this.maxFieldOfView.FindVisibleTargets();
        Debug.Log("maxFieldOfView");
        Debug.Log(maxFieldOfView.visibleTargets.Count);
        return maxFieldOfView.visibleTargets.Count > 0;
        //return Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.forward, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }
    public virtual void flip()
    {
        //TODO: à revoir pour adapter à notre jeu et le facing
        //facingDirection *= -1;
        aliveGameObject.transform.Rotate(0f, 180f, 0f);
    }
    public virtual void Rotate(int angle)
    {
        //facingDirection = (facingDirection + angle) % 360; // Applique la rotation
        aliveGameObject.transform.Rotate(0f, 0f, angle); // Je suppose qu'on va tourner par rapport à l'axe z   
    }

    public virtual void RotateToFacePlayer()
    {
        if(minFieldOfView.visibleTargets.Count>0 || this.maxFieldOfView.visibleTargets.Count>0)
        {
            rb.transform.LookAt(maxFieldOfView.visibleTargets[0]);
        }
    }
}

