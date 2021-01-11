using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ZombieCharacterScript))]
    public class AIZombieController : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ZombieCharacterScript character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        public float distance;
        //minimum attack range of zombie
        public float minimumAttackRange = 2f;

        //The player object
        public GameObject player;
        
        //zombie attack strength
        public int attackDamage = 10;

        //variables for delay 
        public float lastAttackTime = 0;
        public float attackCooldown = 1;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ZombieCharacterScript>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
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
                attackPlayer(target);
            }
        }

        public void attackPlayer(Transform target)
        {
            //finds animator on the armature of the zombie
            GameObject armature = gameObject.transform.GetChild(0).gameObject;
            Animator target_animator = armature.GetComponent<Animator>();
            
            //sets trigger 'attack'
            target_animator.SetTrigger("Attack");

            player = GameObject.Find("FPSController_Low_Poly");

            FirstPersonController playerScript = player.GetComponent<FirstPersonController>();

            //if statements sets delay so the zombie doesn't attack 60 times a second
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;
                playerScript.playerTakeDamage(attackDamage);
            }         
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
