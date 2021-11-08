using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FSM stateMachine;
    public D_temporayEntity entityData;
    
    public int facingDirection { get; private set; }
    public Rigidbody rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGameObject { get; private set; } //la un truc sp�cifique au tuto � enlever apr�s peut �tre, parce que juste un game object qui r�f�rence un objet dans la sc�ne en dessous du monstre
    private Vector3 velocityWorkSpace;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    public string MonsterName;
    public int maxHealth;
    public int power; //Coef de difficult� de l'ennemie
    public virtual void Start()
    {
        facingDirection = 1;

        aliveGameObject = transform.Find("Alive").gameObject;
        rb = aliveGameObject.GetComponent<Rigidbody>();
        anim = aliveGameObject.GetComponent<Animator>();

        stateMachine = new FSM();

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
        //TODO: � revoir pour adapter � notre jeu
        velocityWorkSpace.Set(0,0,-facingDirection * velocity);
        rb.velocity = velocityWorkSpace;
    }

    public virtual bool checkWall()
    {
        //TODO: � revoir pour adapter � notre jeu
        return Physics2D.Raycast(wallCheck.position, aliveGameObject.transform.right, entityData.wallCheckDistance, entityData.whatisground);
    }

    public virtual bool checkLedge()
    {
        //TODO: � revoir pour adapter � notre jeu
        return Physics2D.Raycast(ledgeCheck.position,Vector2.down, entityData.ledgeCheckDistance, entityData.whatisground);

    }

    public virtual void flip()
    {
        //TODO: � revoir pour adapter � notre jeu et le facing
        facingDirection *= -1;
        aliveGameObject.transform.Rotate(0f, 180f, 0f);
    }
    public virtual void Rotate(int angle)
    {
        facingDirection = (facingDirection + angle) % 360; // Applique la rotation
        aliveGameObject.transform.Rotate(0f, 0f, angle); // Je suppose qu'on va tourner par rapport � l'axe z   
    }
}

