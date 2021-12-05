using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AttackDetails attackDetails;
    private float speed;
    private Rigidbody rb;
    private float travelDistance;
    public float damageRadius;
    private Vector3 startPosition;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private Transform damagePosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        startPosition = transform.position;
    }

    private void Update()
    {
        attackDetails.position = transform.position;
    }

    private void FixedUpdate()
    {

        Collider[] detectedObjects = Physics.OverlapSphere(damagePosition.position, damageRadius, whatIsPlayer);
        if(detectedObjects.Length>0)
        {
            foreach (Collider collider in detectedObjects)
            {
                Debug.Log(attackDetails.damageAmount);

                collider.transform.SendMessage("TakeDamage", attackDetails.damageAmount);
            }
            Destroy(gameObject);
        }
        else if ((transform.position - startPosition).magnitude >= travelDistance)
        {
            Destroy(gameObject);
        }
    }

    public void FireProjectile(float speed,float travelDistance,float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
    }
}