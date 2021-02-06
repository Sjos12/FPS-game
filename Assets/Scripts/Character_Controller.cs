
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
        public Camera fpsCam;
        public RuntimeAnimatorController[] animationControllers;
        public GameObject[] weapons;
        public GameObject[] muzzleFlashes;
        public int[] magazines;
        public int[] magazineCapacities;
        public bool[] magazineEmpty;
        public int stockpile = 200;
        public AudioClip[] audioClips;
        public TextMeshProUGUI ammoDisplay;
        public TextMeshProUGUI stockpileDisplay;
        public bool fullAuto = false;
        float fireRate = 2f;
        float fireTime = 0;
        int activeWeapon;
        public float targetTime = 60.0f;
        //variables for raycast
        public int damage = 10;
        public float range = 100f;
        
        

        // Start is called before the first frame update
        void Start()
        {
            m_Animator = gameObject.GetComponent<Animator>();
            m_shootingSound = GetComponent<AudioSource>();
            //weaponslot for ak47
            magazines[0] = 30;
            magazineCapacities[0] = magazines[0];
            magazineEmpty[0] = false;
            //wepaonslot for glock
            magazines[1] = 17;
            magazineCapacities[1] = magazines[1];
            magazineEmpty[1] = false;
            //weaponslot for m1911
            magazines[2] = 7;
            magazineCapacities[2] = magazines[2];
            magazineEmpty[2] = false;
        }
        

        // Update is called once per frame
        void Update()
        {
        if (Input.GetKeyDown("1"))
            {
                //m_Animator.SetTrigger("SwitchGun");
                weaponSwitch(0, fullAuto = true, damage = 20, range = 200f, fireRate = 8f); 
            }
            if (Input.GetKeyDown("2"))
            {
                //.SetTrigger("SwitchGun");
                weaponSwitch(1, fullAuto = false, damage = 25, range = 200f, fireRate);
            }

            if (Input.GetKeyDown("3"))
            {
                //.SetTrigger("SwitchGun");
                weaponSwitch(2, fullAuto = false, damage = 20, range = 200f, fireRate);
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

            ammoDisplay.text = magazines[activeWeapon].ToString();
            stockpileDisplay.text = stockpile.ToString();
            if (Input.GetButton("Fire1"))
            {
                if (magazines[activeWeapon] <= 0)
                {
                    magazineEmpty[activeWeapon] = true;
                }
                if (fullAuto == true && (magazineEmpty[activeWeapon] == false))
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
                if (magazineEmpty[activeWeapon] == true)
                {
                    Debug.Log("Empty mag");
                }

                else
                {
                    
                    if (magazineEmpty[activeWeapon] == false)
                    {
                        if (fullAuto == false)
                        {
                            Shoot();

                        }
                    }
                    if (magazines[activeWeapon] == 0)
                    {
                    magazineEmpty[activeWeapon] = true;
                    }
                }
            }

            if (Input.GetKeyDown("r"))
            {
                m_Animator.SetTrigger("Reload");
            }
        }
        
        public void weaponSwitch(int weaponSlot, bool fullAuto, int damage, float range, float fireRate)
        {
            //disables all weapons. 
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
            }

            activeWeapon = weaponSlot;
            //enables the weapon which has been selected and it's corresponding animator. 
            weapons[weaponSlot].SetActive(true);
            m_Animator.runtimeAnimatorController = animationControllers[weaponSlot];
            m_shootingSound.clip = audioClips[weaponSlot];
        }
        
        //Gets called after the reload animation
        public void fillMagazine ()
        {
            stockpile = stockpile - magazineCapacities[activeWeapon] + magazines[activeWeapon];
            magazines[activeWeapon] = magazineCapacities[activeWeapon];
            magazineEmpty[activeWeapon] = false;
        }

            
        void Shoot()
            {
            RaycastHit hit;

            //makes sure player can't shoot with empty mag
            if (magazineEmpty[activeWeapon] == false)
            {
                m_Animator.SetTrigger("Fire");
                m_shootingSound.Play();
                StartCoroutine("muzzleFlash");
                magazines[activeWeapon] = magazines[activeWeapon] - 1;
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
    IEnumerator muzzleFlash()
    {
        muzzleFlashes[activeWeapon].SetActive(true);
        yield return new WaitForSeconds(0.01f);
        muzzleFlashes[activeWeapon].SetActive(false);
        muzzleFlashes[activeWeapon].transform.Rotate(34, 0, 0, Space.Self);
    }           
        

        
    }


