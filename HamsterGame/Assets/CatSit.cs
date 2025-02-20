using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSit : StateMachineBehaviour
{

    public float speed = 2.5f;
    public float followRange = 10f;

    Transform player;
    Rigidbody2D rb;
    CatBoss cat;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        cat = animator.GetComponent<CatBoss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(player.position, rb.position) <= followRange)
        {
            animator.SetTrigger("PlayerIsNear");
        }
        else
        {
            animator.ResetTrigger("PlayerIsNear");
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
