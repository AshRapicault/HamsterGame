using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.SceneManagement;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public GameObject enemyEndPoint;
    public GameObject enemyStartPoint;
    private Rigidbody2D body;
    private Animator anim;
    private Transform currentPoint;
    private bool movingRight = true;

    void Start()
    {
        // Get references for your components
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = enemyStartPoint.transform;
        anim.SetBool("isWalking", true);

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == enemyStartPoint.transform)
        {
            body.velocity = new Vector2(speed, 0);
        }
        else
        {
            body.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == enemyStartPoint.transform)
        {
            flip();
            currentPoint = enemyEndPoint.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == enemyEndPoint.transform)
        {
            flip();
            currentPoint = enemyStartPoint.transform;
        }
    }
    private void flip()
    {
        if (movingRight && currentPoint == enemyEndPoint.transform || !movingRight && currentPoint == enemyStartPoint.transform)
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;

            movingRight = !movingRight;  
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(enemyStartPoint.transform.position, 0.5f);
        Gizmos.DrawWireSphere(enemyEndPoint.transform.position, 0.5f);
        Gizmos.DrawLine(enemyStartPoint.transform.position,enemyEndPoint.transform.position);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
