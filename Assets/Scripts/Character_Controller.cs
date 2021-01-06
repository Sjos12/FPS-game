using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    Animator m_Animator;
    AudioSource m_shootingSound;
    public int magazine = 7;
    public bool magazineEmpty = false; 

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_shootingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetButtonDown("Fire1"))
        {
            

            if (magazine == 0)
            {
                magazineEmpty = true; 
            }

            else
            {
                magazine = magazine - 1;
                magazineEmpty = false;
            }

            if (magazineEmpty == true)
            {
                Debug.Log("Empty mag");
            }

            else
            {
                m_Animator.SetTrigger("Fire");
                m_shootingSound.Play();
                Debug.Log("Fire Animation");
            }
            
        }

        if (Input.GetKeyDown("r"))
        {

            m_Animator.SetTrigger("Reload");
            magazine = 7;
            Debug.Log("Reload Animation");
        }
    }
}
