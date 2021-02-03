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
        defaultAgentSpeed = agent.speed;
    }


    private void Update()
    {
        

        //when the agent reach the end point you should tell it, and the agent will "exit" the link and work normally after that
        

        if (agent.isOnOffMeshLink)
        {
            RaycastHit hit;
            if (Physics.Raycast(agent.transform.position, agent.transform.forward, out hit, 3f)) {
                if (hit.collider.gameObject.tag != "Environment")
                {
                    target_animator.SetTrigger("climbFinal");
                    Debug.Log("climbFinal");
                }
            }
        }
        else
        {
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

            target_animator.SetBool("isAttackingObject", true);
        }
        else
        {
            target_animator.SetBool("isAttackingObject", false);
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

