using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public class playerTakeDamage : MonoBehaviour
{
    public GameObject zombie;
    public GameObject barricade;
    
    private int damage = 20;

    private void Start()
    {
        damage = zombie.GetComponent<AIZombieController>().attackDamage;
    }
    public void hitRegister()
    {
        //zombie = GameObject.FindGameObjectWithTag("Zombie");
        if (zombie.GetComponent<AIZombieController>().distance < zombie.GetComponent<AIZombieController>().minimumAttackRange)
        {
            zombie.GetComponent<AIZombieController>().attackPlayer();
        }
        
    }
    public void objectAttack()
    {
        if (barricade != null)
        {
            barricade.GetComponent<ObjectTakeDamage>().objectTakeDamage(damage);
        }   
    }
}
