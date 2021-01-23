using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class despawnZombie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void destroyZombie()
    {
        gameObject.GetComponentInParent<Target>().Die();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
