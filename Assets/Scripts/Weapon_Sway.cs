using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Sway : MonoBehaviour
{

    public float amount;
    public float maxamount;
    public float smooth;
    private Quaternion def;
    private bool Paused = false;

    void Start()
    {
        def = transform.localRotation;
    }

    void Update()
    {
        float factorX = (Input.GetAxis("Mouse Y")) * amount;
        float factorY = -(Input.GetAxis("Mouse X")) * amount;
        //float factorZ = -Input.GetAxis("Vertical") * amount;
        float factorZ = 0 * amount;

        if (!Paused)
        {
            if (factorX > maxamount)
                factorX = maxamount;

            if (factorX < -maxamount)
                factorX = -maxamount;

            if (factorY > maxamount)
                factorY = maxamount;

            if (factorY < -maxamount)
                factorY = -maxamount;

            if (factorZ > maxamount)
                factorZ = maxamount;

            if (factorZ < -maxamount)
                factorZ = -maxamount;

            Quaternion Final = Quaternion.Euler(def.x + factorX, def.y + factorY, def.z + factorZ);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Final, (Time.time * smooth));
        }
    }
}
