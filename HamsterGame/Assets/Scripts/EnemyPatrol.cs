using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    public GameObject enemyStartPoint;
    public GameObject enemyEndPoint;
    private Rigidbody2D body;
    private Animator anim;
    private Transform currentTarget;
    private bool movingRight = true;

    public float switchDistance = 0.1f;

    public gameOverScript gameOver;
    public CollectiblesManager collectiblesManager;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentTarget = enemyEndPoint.transform;
        anim.SetBool("isWalking", true);

        gameOver = GameObject.FindGameObjectWithTag("gameOver").GetComponent<gameOverScript>();
        collectiblesManager = CollectiblesManager.instance;
    }

    void Update()
    {
        // Beweeg met constante snelheid naar het doel
        float direction = Mathf.Sign(currentTarget.position.x - transform.position.x);
        body.velocity = new Vector2(direction * speed, body.velocity.y);

        // Controleer of we dicht genoeg zijn om van richting te wisselen
        if (Mathf.Abs(transform.position.x - currentTarget.position.x) < switchDistance)
        {
            if (currentTarget == enemyEndPoint.transform)
            {
                currentTarget = enemyStartPoint.transform;
            }
            else
            {
                currentTarget = enemyEndPoint.transform;
            }
            // Nieuw: kijkrichting bijwerken
            UpdateDirection(direction);
        }
    }

    void UpdateDirection(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (direction > 0 ? 1 : -1);
        transform.localScale = scale;
    }

    void OnDrawGizmos()
    {
        if (enemyStartPoint != null && enemyEndPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(enemyStartPoint.transform.position, 0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(enemyEndPoint.transform.position, 0.2f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(enemyStartPoint.transform.position, enemyEndPoint.transform.position);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            gameOver.GameOver();
        }

        if (collision.gameObject.CompareTag("AttackSeed"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            collectiblesManager.countPoints += 5;
        }
    }
}