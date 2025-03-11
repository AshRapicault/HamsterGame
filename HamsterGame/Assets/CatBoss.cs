using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBoss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    public float attackRange = 3f; 

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void LookAtPlayer()
    {
        if (transform.position.x > player.position.x)
        {
            spriteRenderer.flipX = true; 
            isFlipped = true;
        }
        else if (transform.position.x < player.position.x)
        {
            spriteRenderer.flipX = false; 
            isFlipped = false;
        }
    }

    public bool PlayerInAttackRange()
    {
        return Vector2.Distance(transform.position, player.position) <= attackRange;
    }
}