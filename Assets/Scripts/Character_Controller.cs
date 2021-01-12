
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Character_Controller : MonoBehaviour
    {
        Animator m_Animator;
        Animator target_animator;
        AudioSource m_shootingSound;

        //variables for magazine logic
        public int magazine = 7;
        public TextMeshProUGUI ammoDisplay;
        public bool magazineEmpty = false;

        //variables for raycast
        public int damage = 10;
        public float range = 100f;

        //variables for healthsystem 
        

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
            if ((Input.GetKey("w")) || (Input.GetKey("s")))
            {
                m_Animator.SetBool("isWalking", true);
            }

            else
            {
                m_Animator.SetBool("isWalking", false);
            }
       
            if (Input.GetButtonDown("Fire2"))
            {
                m_Animator.SetBool("isAiming", true);
            }
            if (Input.GetButtonUp("Fire2"))
            {
                m_Animator.SetBool("isAiming", false);
            }

            ammoDisplay.text = magazine.ToString();
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

        void Shoot()
        {
            RaycastHit hit;

            //makes sure player can't shoot with empty mag
            if (magazineEmpty == false)
            {
                //shoots a ray
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
                {
                    Target target = hit.transform.GetComponent<Target>();

                    if (target != null)
                    {
                        //raycast target takes damage
                        target.TakeDamage(damage);

                        //set trigger 'hit' at child of zombie.
                        GameObject armature = target.transform.GetChild(0).gameObject;

                        target_animator = armature.GetComponent<Animator>();

                        target_animator.SetTrigger("Hit");
                    }
                }
            }

        }

        
    }


