using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAttack : StateMachineBehaviour
{
    CatBoss cat;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cat = animator.GetComponent<CatBoss>();
        cat.LookAtPlayer();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("ShouldAttack", cat.ShouldAttack());
    }
}