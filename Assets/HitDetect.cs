using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetect : MonoBehaviour
{
    public Target target;
    public void sendDamage(int damage)
    {
        Debug.Log("headshot");
        target.TakeDamage(damage);
    }
}
