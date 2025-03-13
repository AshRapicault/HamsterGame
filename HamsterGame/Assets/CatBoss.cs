using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBoss : MonoBehaviour
{
    public Transform player;
    public float attackRange = 7f;
    public float followRange = 15f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
}