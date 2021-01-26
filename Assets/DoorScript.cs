using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator m_animator;
     GameObject player;
    public bool doorIsOpen = false;
    public bool doorIsClosed = true;
    public float distance;
    public float minDistance = 1.5f; 
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        player = GameObject.Find("FPSController_Low_Poly");
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (distance < minDistance )
        {
            if (Input.GetKeyDown("e") && doorIsClosed)
            {
                m_animator.SetBool("DoorOpen", true);
                m_animator.SetBool("DoorClosed", false);
                doorIsOpen = true;
                doorIsClosed = false;
            }

            else if (Input.GetKeyDown ("e") && doorIsOpen)
            {
                m_animator.SetBool("DoorClosed", true);
                m_animator.SetBool("DoorOpen", false);
                doorIsOpen = false;
                doorIsClosed = true;
            }
         }
    }
}
