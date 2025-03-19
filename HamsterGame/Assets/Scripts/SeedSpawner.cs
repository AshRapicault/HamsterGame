using System.Collections;
using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    [SerializeField] private GameObject seedPrefab; // Het zaadje prefab
    [SerializeField] private float spawnInterval = 2f; // Interval tussen spawns
    [SerializeField] private float spawnHeight = 2f; // Maximale hoogte waarop zaadjes kunnen verschijnen
    [SerializeField] private float spawnWidth = 5f; // Maximale breedte waarop zaadjes kunnen verschijnen

    private bool isPlayerInField = false;
    private bool isSpawning = false;
    public CollectiblesManager cm;
    public BossHealth bossHealth;
    gameOverScript gameOver;

    private bool bossBattleMusicPlayed = false;

    void Start()
    {
        cm = CollectiblesManager.instance;
        gameOver = FindObjectOfType<gameOverScript>();
    }

    void Update()
    {
        if (isPlayerInField && !isSpawning && cm.countAttackSeeds < 30)
        {
            StartCoroutine(SpawnSeeds());
        }
    }

    private IEnumerator SpawnSeeds()
    {
        isSpawning = true;

        while (isPlayerInField && cm.countAttackSeeds < 30 && bossHealth.CurrentHealth > 0)
        {
            float randomX = transform.position.x + Random.Range(-spawnWidth / 2, spawnWidth / 2);
            float randomY = transform.position.y + Random.Range(-spawnHeight, 0);

            Instantiate(seedPrefab, new Vector2(randomX, randomY), Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
        DestroyAllSeeds();
    }

    public void DestroyAllSeeds()
    {
        GameObject[] seeds = GameObject.FindGameObjectsWithTag("AttackCollectible");
        foreach (GameObject seed in seeds)
        {
            Destroy(seed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInField = true;
        }

        if (!bossBattleMusicPlayed)
        {
            bossBattleMusicPlayed = true;
            AudioManager.instance.PlayMusic(AudioManager.instance.bossBattleMusic);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInField = false;
            StopSpawning();

            if (bossHealth != null)
            {
                bossHealth.RestoreHealth();
            }

            if (!gameOver.gameOverActive)
            {
                FindObjectOfType<CatBoss>().ReturnToStart();
            }

            AudioManager.instance.PlayMusic(AudioManager.instance.gameplayMusic);
            bossBattleMusicPlayed = false;
        }
    }
}