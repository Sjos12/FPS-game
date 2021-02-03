using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTakeDamage : MonoBehaviour
{
    public int health = 200;

    public void objectTakeDamage(int damage)
    {
        health = health - damage;
        GetComponent<Healthbar>().SetHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0 )
        {
            Destroy(gameObject);
        }
        
    }
}
