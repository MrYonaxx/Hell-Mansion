using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualEnnemies : MonoBehaviour
{
    
    public D_VirtualEnnemies virtualEnneiesData; 
    public FSM stateMachine;
    public Rigidbody2D rb {get; private set; }
    public Animator anim {get; private set; }
    public GameObject aliveGO {get; private set; }
    
    public int facingDirection {get; private set; } //Orientation de l'ennemie
    private Vector2 velocityWorkSpace;
    public string name;
    
    [SerializeField]
    private Transform wallCheck;
    
    public int maxHealth;
    public int power; //Coef de difficulté de l'ennemie
    
    
    
    public virtual void Start()
    {
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
    }

    public virtual void Update()
    {
        stateMachine.currentState.logicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.physicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(Mathf.Cos(facingDirection) * velocity, Mathf.Sin(facingDirection) * velocity); // A voir comment ça réagit
        rb.velocity = velocityWorkSpace;
    }

    public virtual bool checkWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, virtualEnneiesData.wallCheckDistance, virtualEnneiesData.whatisground);
    }

    public virtual void Rotate(int angle)
    {  
        facingDirection = (facingDirection + angle) % 360; // Applique la rotation
        aliveGO.transform.Rotate(0f, 0f, angle); // Je suppose qu'on va tourner par rapport à l'axe z   
    }
}
