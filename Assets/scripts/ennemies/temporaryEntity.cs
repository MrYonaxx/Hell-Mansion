using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporaryEntity : MonoBehaviour
{
    public FSM stateMachine;
    public D_temporayEntity entityData;
    
    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject entityGameObject { get; private set; } //la un truc spécifique au tuto à enlever après peut être, parce que juste un game object qui référence un objet dans la scène en dessous du monstre
    private Vector2 velocityWorkSpace;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    public virtual void Start()
    {
        facingDirection = 1;

        entityGameObject = transform.Find("temporaryEntity").gameObject;
        rb = entityGameObject.GetComponent <Rigidbody2D>();
        anim = entityGameObject.GetComponent<Animator>();

        stateMachine = new FSM();
    }

    public virtual void update()
    {
        stateMachine.currentState.logicUpdate();
    }

    public virtual void fixedUpdate()
    {
        stateMachine.currentState.physicsUpdate();
    }

    public virtual void setVelocity(float velocity)
    {
        //TODO: à revoir pour adapter à notre jeu
        velocityWorkSpace.Set(facingDirection * velocity,rb.velocity.y);
        rb.velocity = velocityWorkSpace;
    }

    public virtual bool checkWall()
    {
        //TODO: à revoir pour adapter à notre jeu
        return Physics2D.Raycast(wallCheck.position, entityGameObject.transform.right, entityData.wallCheckDistance, entityData.whatisground);
    }

    public virtual bool checkLedge()
    {
        //TODO: à revoir pour adapter à notre jeu
        return Physics2D.Raycast(ledgeCheck.position,Vector2.down, entityData.ledgeCheckDistance, entityData.whatisground);

    }

    public virtual void flip()
    {
        //TODO: à revoir pour adapter à notre jeu et le facing
        facingDirection *= -1;
        entityGameObject.transform.Rotate(0f, 180f, 0f);
    }
}
