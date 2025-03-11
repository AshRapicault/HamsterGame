using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatWalk : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float followRange = 10f;

    Transform player;
    Rigidbody2D rb;
    CatBoss cat;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        cat = animator.GetComponent<CatBoss>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector2.Distance(player.position, rb.position);

        if (distance > followRange)
        {
            animator.SetTrigger("TooFar");
            return;
        }

        cat.LookAtPlayer(); // Kijk altijd naar de speler

        Vector2 targetPosition = new Vector2(player.position.x, rb.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (cat.PlayerInAttackRange())
        {
            animator.SetTrigger("Attack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("TooFar");
    }
}