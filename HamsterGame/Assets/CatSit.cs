using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSit : StateMachineBehaviour
{
    CatBoss cat;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cat = animator.GetComponent<CatBoss>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("TooFar", cat.TooFar());
    }
}
