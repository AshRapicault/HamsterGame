using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSit : StateMachineBehaviour
{
    CatBoss cat;
    gameOverScript gameOver;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cat = animator.GetComponent<CatBoss>();
        gameOver = FindObjectOfType<gameOverScript>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("TooFar", cat.TooFar());
        if (gameOver.gameOverActive == true)
        {
            Debug.Log("player ded now cat should sit");
            animator.SetBool("playerDead", true);
        }
    }
}
