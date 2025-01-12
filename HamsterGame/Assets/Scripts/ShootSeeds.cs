
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ShootSeeds : MonoBehaviour
{
    [SerializeField] private GameObject seedPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float seedSpeed = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootSeed();
        }
    }

    private void ShootSeed()
    {
        GameObject newSeed = Instantiate(seedPrefab, shootPoint.position, Quaternion.identity);

        Rigidbody2D rb = newSeed.GetComponent<Rigidbody2D>();

        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        rb.velocity = direction * seedSpeed;

        StartCoroutine(DestroySeedAfterTime(newSeed, 3f));
    }

    private IEnumerator DestroySeedAfterTime(GameObject seed, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(seed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
