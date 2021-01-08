using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    Animator m_Animator;
    AudioSource m_shootingSound;

    //variables for magazine logic
    public int magazine = 7;
    public bool magazineEmpty = false;

    //variables for raycast
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam; 

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_shootingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetButtonDown("Fire2"))
        {
            m_Animator.SetBool("isAiming", true);
        }
      if (Input.GetButtonUp("Fire2"))
        {
            m_Animator.SetBool("isAiming", false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            //raycast shooting logic function
            Shoot();

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

    void Shoot ()
    {
        RaycastHit hit; 
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
