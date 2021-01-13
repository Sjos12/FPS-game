using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDownSights : MonoBehaviour
{
  
    public Vector3 hipPos;
    public Vector3 zoomPos;

    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, zoomPos, Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, hipPos, Time.deltaTime);
        }
    }
}

