using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Sway : MonoBehaviour
{

    public float amount;
    public float smooth;

    public bool paused = false; 

    private Vector3 def;



    void Start()
    {
        def = transform.localPosition;
    }

    void Update()
    {
        if (!paused)
        {
            float factorX = -Input.GetAxis("Mouse X") * amount;
            float factorY = -Input.GetAxis("Mouse Y") * amount;

            Vector3 finalPosition = new Vector3(factorX, factorY, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + def, Time.deltaTime * smooth);
        }
        
    }
}
