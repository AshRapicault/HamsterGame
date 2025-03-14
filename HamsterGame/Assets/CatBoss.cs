using System.Collections;
using UnityEngine;

public class CatBoss : MonoBehaviour
{
    public Transform player;
    public float attackRange = 7f;
    public float followRange = 15f;

    private SpriteRenderer spriteRenderer;
    private Vector2 startPosition;

    private float attackTimer = 0f;
    private bool playerInAttackRange = false;

    [Header("Battle Field Limits")]
    public Vector2 fieldMin;
    public Vector2 fieldMax;

    [Header("Game Over Script")]
    public gameOverScript gameOver;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = new Vector2(165f, -15f);
        transform.position = startPosition;
        gameOver = FindObjectOfType<gameOverScript>();
    }

    void Update()
    {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, fieldMin.x, fieldMax.x),
            Mathf.Clamp(transform.position.y, fieldMin.y, fieldMax.y)
        );

        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if (!playerInAttackRange)
            {
                playerInAttackRange = true;
                StartCoroutine(StartAttackTimer());
            }
        }
        else
        {
            playerInAttackRange = false;
            StopCoroutine(StartAttackTimer());
        }
    }

    public void LookAtPlayer()
    {
        spriteRenderer.flipX = transform.position.x < player.position.x;
    }

    public bool TooFar()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        return distance > followRange;
    }

    public bool ShouldAttack()
    {
        return Vector2.Distance(transform.position, player.position) <= attackRange;
    }

    public void ReturnToStart()
    {
        transform.position = startPosition;
    }

    private IEnumerator StartAttackTimer()
    {
        attackTimer = 0f;
        while (attackTimer < 3f)
        {
            if (!playerInAttackRange) yield break;
            attackTimer += Time.deltaTime;
            yield return null;
        }

        if (playerInAttackRange)
        {
            gameOver.GameOver();
        }
    }
}