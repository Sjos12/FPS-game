using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTakeDamage : MonoBehaviour
{
    int health = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void objectTakeDamage(int damage)
    {
        health = -damage;
        GetComponent<Healthbar>().SetHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
