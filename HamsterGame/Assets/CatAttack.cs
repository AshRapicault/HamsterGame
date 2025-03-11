using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAttack : StateMachineBehaviour
{
    Transform player;
    CatBoss cat;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cat = animator.GetComponent<CatBoss>();

        cat.LookAtPlayer(); 
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!cat.PlayerInAttackRange())
        {
            animator.SetTrigger("TooFar");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("TooFar");
    }
}