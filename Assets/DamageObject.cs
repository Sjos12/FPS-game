using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class DamageObject : MonoBehaviour
{
    int damage = 10;
    public GameObject zombie;
    AIZombieController zombiescript;
    // Start is called before the first frame update
    void Start()
    {
        zombiescript = zombie.GetComponent<AIZombieController>();
    }

    public void damageObject(int damage)
    {
        zombiescript.player.GetComponent<FirstPersonController>().playerTakeDamage(damage);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
