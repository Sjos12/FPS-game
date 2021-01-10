using System;
using UnityEngine;

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
        public float minimumAttackRange = 2f;
        public GameObject player;
        public int attackDamage = 10;

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
            Debug.Log("Attack");
            Animator target_animator = armature.GetComponent<Animator>();
            
            //sets trigger 'attack'
            target_animator.SetTrigger("Attack");

            player = GameObject.Find("FPSController_Low_Poly");

            UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerScript = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();

            playerScript.playerTakeDamage(attackDamage);


        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
