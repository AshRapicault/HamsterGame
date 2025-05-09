using System.Collections;
using UnityEngine;

public class ShootSeeds : MonoBehaviour
{
    [SerializeField] private GameObject seedPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float seedSpeed = 10f;

    public CollectiblesManager collectiblesManager;

    private void Start()
    {
        collectiblesManager = CollectiblesManager.instance;
    }

    void Update()
    {
        if (PauseMenu.instance != null && PauseMenu.instance.isPaused)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (collectiblesManager.countAttackSeeds > 0)
            {
                ShootSeed();
                collectiblesManager.countAttackSeeds--;
            }
        }
    }

    private void ShootSeed()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float direction = mouseWorldPosition.x < transform.position.x ? -1f : 1f;

        Vector3 spawnPos = shootPoint.position + new Vector3(direction * 0.5f, 0, 0);

        GameObject newSeed = Instantiate(seedPrefab, spawnPos, Quaternion.identity);
        Rigidbody2D rb = newSeed.GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(direction * seedSpeed, 0f);

        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D seedCollider = newSeed.GetComponent<Collider2D>();
        if (playerCollider != null && seedCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, seedCollider);
        }

        StartCoroutine(DestroySeedAfterTime(newSeed, 3f));
    }

    private IEnumerator DestroySeedAfterTime(GameObject seed, float time)
    {
        yield return new WaitForSeconds(time);
        if (seed != null)
            Destroy(seed);
    }
}