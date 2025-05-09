using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SeedSpawner : MonoBehaviour
{
    [SerializeField] private GameObject seedPrefab; // Het zaadje prefab
    [SerializeField] private float spawnInterval = 2f; // Interval tussen spawns
    [SerializeField] private float spawnHeight = 3f; // Maximale hoogte waarop zaadjes kunnen verschijnen
    [SerializeField] private float spawnWidth = 3f; // Maximale breedte waarop zaadjes kunnen verschijnen
    [SerializeField] private bool isBossField = false;
    [SerializeField] private GameObject bossBattleHintPanel;

    private bool isPlayerInField = false;
    private bool isSpawning = false;
    public CollectiblesManager cm;
    public BossHealth bossHealth;
    gameOverScript gameOver;
    public AudioManager audioManager;

    private bool bossBattleMusicPlayed = false;

    void Start()
    {
        cm = CollectiblesManager.instance;
        gameOver = FindObjectOfType<gameOverScript>();
        audioManager = AudioManager.instance;
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
            float randomY = transform.position.y + Random.Range(-spawnHeight, 1);

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
            if (bossBattleHintPanel != null)
            {
                bossBattleHintPanel.SetActive(true);
            }

            isPlayerInField = true;

            if (isBossField && !bossBattleMusicPlayed)
            {
                bossBattleMusicPlayed = true;
                if (audioManager != null)
                {
                    audioManager.PlayMusic(audioManager.bossBattleMusic);
                }
                else
                {
                    Debug.Log("no audiomanager found");
                }
            }

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
                CatBoss catBoss = FindObjectOfType<CatBoss>();
                if (catBoss != null)
                {
                    catBoss.ReturnToStart();
                }
            }

            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayMusic(AudioManager.instance.gameplayMusic);
            }

            bossBattleMusicPlayed = false;
        }
    }

    public void CloseHintPanel()
    {
        if (bossBattleHintPanel != null)
        {
            bossBattleHintPanel.SetActive(false);
        }
    }
}