using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
//[RequireComponent(typeof (ZombieCharacterScript))]
public class AIZombieController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public ZombieCharacterScript character { get; private set; } // the character we are controlling
    Collider collider;
    Animator target_animator;
    public Transform target;                                    // target to aim for
    public float distance;
    //minimum attack range of zombie
    public float minimumAttackRange = 2f;
    //finds animator on the armature of the zombie
    GameObject armature;
    DamageObject target_script;
    //The player object
    public GameObject player;
    public float climbSpeed = 1f;
    public float defaultAgentSpeed;
    bool climbFinal;
    public bool hasJumped = false;
    //zombie attack strength
    public int attackDamage = 25;

    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<ZombieCharacterScript>();
        collider = GetComponent<Collider>();
        armature = gameObject.transform.GetChild(0).gameObject;
        target_animator = armature.GetComponent<Animator>();
        target_script = armature.GetComponent<DamageObject>();
        agent.updateRotation = false;
        agent.updatePosition = true;
        agent.autoTraverseOffMeshLink = true;
        defaultAgentSpeed = agent.speed;
    }


    private void Update()
    {
        if (agent.isOnOffMeshLink)
        {
            //agent.speed = climbSpeed;
            RaycastHit hit;
            Vector3 shootPosition = transform.position + new Vector3(0, 2.2f, 0);
            Vector3 shootPositionJumpRay = transform.position + new Vector3(0, -3f, 0);
            
            if (Physics.Raycast(shootPosition, transform.forward, out hit, 2.5f))
            {
                agent.speed = climbSpeed;
                target_animator.SetBool("isClimbing", true);
                climbFinal = true;
            }
            else if (climbFinal == true)
            {
               agent.speed = climbSpeed;
               target_animator.SetBool("isClimbing", false);
               target_animator.SetTrigger("climbFinal");
               climbFinal = false;
            }

            if (Physics.Raycast(shootPositionJumpRay, -transform.forward, out hit, 1.5f))
            {
                Debug.Log(Vector3.Angle(agent.transform.forward, hit.normal));
                if (Vector3.Angle(agent.transform.forward, hit.normal) > 60 && (hasJumped == false)) {
                    target_animator.SetTrigger("Jump");
                    agent.speed = climbSpeed + 3f;
                    hasJumped = true;
                }

                else {
                    //agent.speed = climbSpeed;
                }
            }
            else 
            {
                //agent.speed = defaultAgentSpeed;
            }

        }
        else
        {
            hasJumped = false;
            agent.speed = defaultAgentSpeed;
            target_animator.SetBool("isClimbing", false);
        }

        if (target != null)
            agent.SetDestination(target.position);
        //checks distance between zombie and player. 
        distance = Vector3.Distance(gameObject.transform.position, target.transform.position);

        if (agent.remainingDistance > agent.stoppingDistance)
            character.Move(agent.desiredVelocity, false, false);
        else
            character.Move(Vector3.zero, false, false);



        //code for attacking player.
        if (distance < minimumAttackRange) {
            //sets trigger 'attack'
            target_animator.SetTrigger("Attack");
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Barricade")
        {
            armature.GetComponent<playerTakeDamage>().barricade = collision.collider.gameObject;

            //target_animator.SetBool("isAttackingObject", true);
        }
        else
        {
            //target_animator.SetBool("isAttackingObject", false);
            armature.GetComponent<playerTakeDamage>().barricade = null;
        }
    }
    public void attackPlayer()
        {
   
            player = GameObject.FindGameObjectWithTag("Player");

            FirstPersonController playerScript = player.GetComponent<FirstPersonController>();

  
            playerScript.playerTakeDamage(attackDamage);
            
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }

