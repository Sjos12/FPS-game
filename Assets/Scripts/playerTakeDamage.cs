using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public class playerTakeDamage : MonoBehaviour
{
    public GameObject zombie;
    public GameObject barricade;
    private int damage;
    public void hitRegister()
    {
        //zombie = GameObject.FindGameObjectWithTag("Zombie");
        zombie.GetComponent<AIZombieController>().attackPlayer();
        damage = zombie.GetComponent<AIZombieController>().attackDamage;
}
    public void objectAttack()
    {
        barricade.GetComponent<ObjectTakeDamage>().objectTakeDamage(damage);
    }
}
