using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public class playerTakeDamage : MonoBehaviour
{
    public GameObject zombie; 
    public void hitRegister()
    {
        zombie = GameObject.Find("StandardZombieV1");
        zombie.GetComponent<AIZombieController>().attackPlayer();
    }

}
