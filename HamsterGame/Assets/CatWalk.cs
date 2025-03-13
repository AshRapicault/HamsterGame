using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatWalk : StateMachineBehaviour
{
    public float speed = 2.5f;
    Rigidbody2D rb;
    CatBoss cat;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        cat = animator.GetComponent<CatBoss>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cat.LookAtPlayer();

        animator.SetBool("TooFar", cat.TooFar());
        animator.SetBool("ShouldAttack", cat.ShouldAttack());

        if (!cat.TooFar())
        {
            Vector2 targetPosition = new Vector2(cat.player.position.x, rb.position.y);
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }
    }
}