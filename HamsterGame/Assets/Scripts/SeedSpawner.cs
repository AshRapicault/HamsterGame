using System.Collections;
using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    [SerializeField] private GameObject seedPrefab; // Het zaadje prefab
    [SerializeField] private float spawnInterval = 2f; // Interval in seconden tussen spawns
    [SerializeField] private float spawnHeight = 2f; // Max hoogte waarop de zaadjes kunnen verschijnen
    [SerializeField] private float spawnWidth = 5f; // Max breedte waarop de zaadjes kunnen verschijnen

    private bool isPlayerInField = false;
    private bool isSpawning = false;
    public CollectiblesManager cm;
    public BossHealth bossHealth;

    void Start()
    {
        cm = CollectiblesManager.instance;
    }

    void Update()
    {
        if (isPlayerInField && !isSpawning && cm.countAttackSeeds < 30)
        {
            StartCoroutine(SpawnSeeds());
        }
        else if (cm.countAttackSeeds >= 30)
        {
            StopCoroutine(SpawnSeeds());
        }
    }

    private IEnumerator SpawnSeeds()
    {
        isSpawning = true;

        while (isPlayerInField && cm.countAttackSeeds < 30)
        {
            float randomX = transform.position.x + Random.Range(-spawnWidth / 2, spawnWidth / 2);
            float randomY = transform.position.y + Random.Range(-spawnHeight, 0);

            Instantiate(seedPrefab, new Vector2(randomX, randomY), Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInField = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInField = false;

            if (bossHealth != null)
            {
                bossHealth.RestoreHealth();
            }
            StopCoroutine(SpawnSeeds());
        }
    }
}