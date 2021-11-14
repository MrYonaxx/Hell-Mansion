using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateEnnemy : MonoBehaviour
{
    [SerializeField]
    int damage = 1;
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    void OnTriggerEnter(Collider other)
    {
        var healthComponent = other.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage);
        };

    }
}
