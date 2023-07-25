using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : StateMachineBehaviour
{
    public class EnemyChaseState : StateMachineBehaviour
    {
        private Transform player; // Reference to the player's transform

        [SerializeField] private float chaseRange = 4; // The range within which the enemy should chase the player

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Find the player object using the "Player" tag and get its transform
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Make the enemy look at the player
            animator.transform.LookAt(player);

            // Calculate the distance between the enemy and the player
            float distance = Vector3.Distance(player.position, animator.transform.position);

            // If the distance is greater than the chase range, stop chasing and set isAttacking to false
            if (distance > chaseRange)
            {
                animator.SetBool("isAttacking", false);
            }
        }
    
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // This method is empty as there is no behavior to perform when exiting this state
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
