
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
        public RuntimeAnimatorController[] animationControllers;
        public GameObject[] weapons;
        public AudioClip[] audioClips;
        //variables for magazine logic
        public int magazine = 7;
        public int magazineCapacity = 7;
        public TextMeshProUGUI ammoDisplay;
        public bool magazineEmpty = false;
        public bool fullAuto = false;
        float fireRate = 2f;
        float fireTime = 0;

        //variables for raycast
    public int damage = 10;
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
            if (Input.GetKeyDown("1"))
            {
                //m_Animator.SetTrigger("SwitchGun");
                weaponSwitch(0, fullAuto = false, damage = 20, range = 200f, fireRate, magazineCapacity = 7);
            }
            if (Input.GetKeyDown("2"))
            {
                //.SetTrigger("SwitchGun");
                weaponSwitch(1, fullAuto = true, damage = 20, range = 200f, fireRate = 8f, magazineCapacity = 30);
            }
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
            if (Input.GetButton("Fire1"))
            {
                if (magazine <= 0)
                {
                    magazineEmpty = true;
                }
                if (fullAuto == true && (magazineEmpty == false))
                    {
                        if (Time.time - fireTime > 1 / fireRate)
                        {
                            fireTime = Time.time;
                            Shoot();
                        
                        }
                }
            }
            if (Input.GetButtonDown("Fire1"))
            {
                if (magazineEmpty == true)
                {
                    Debug.Log("Empty mag");
                }

                else
                {
                    
                    if (magazineEmpty == false)
                    {
                        if (fullAuto == false)
                        {
                            Shoot();

                        }
                    }
                    

                    if (magazine == 0)
                    {
                        magazineEmpty = true;
                    }

                    
                    //Debug.Log("Fire Animation");
                }
            }

            if (Input.GetKeyDown("r"))
            {
                m_Animator.SetTrigger("Reload");
            }
        }
        
        public void weaponSwitch(int weaponSlot, bool fullAuto, int damage, float range, float fireRate, int magazineCapacity)
        {
            //disables all weapons. 
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
            }

            //enables the weapon which has been selected and it's corresponding animator. 
            weapons[weaponSlot].SetActive(true);
            m_Animator.runtimeAnimatorController = animationControllers[weaponSlot];
            m_shootingSound.clip = audioClips[weaponSlot];
        }
        
        public void fillMagazine ()
        {
            magazine = magazineCapacity;
            magazineEmpty = false;
        }

            
        void Shoot()
            {
            RaycastHit hit;

            //makes sure player can't shoot with empty mag
            if (magazineEmpty == false)
            {
            m_Animator.SetTrigger("Fire");
            m_shootingSound.Play();
            magazine = magazine - 1;
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
                    else
                    {
                        Debug.Log(hit);
                    }
                }
            }

        }

        
    }


