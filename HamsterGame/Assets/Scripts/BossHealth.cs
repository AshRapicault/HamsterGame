using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar;
    public int damageAmount = 1;

    [SerializeField] GameObject nextLevel;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    void Start()
    {
        CurrentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = CurrentHealth;

        nextLevel.active = false;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        healthBar.value = CurrentHealth;

        // Als de gezondheid 0 of lager is, sterft de kat
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AttackSeed"))
        {
            TakeDamage(damageAmount);
            Destroy(collision.gameObject);
        }
    }

    void Die()
    {
        FindObjectOfType<SeedSpawner>().StopSpawning();
        Destroy(gameObject);

        if (CollectiblesManager.instance != null)
        {
            CollectiblesManager.instance.countPoints += 50;
            CollectiblesManager.instance.countAttackSeeds = 0;
        }

        nextLevel.active = true;
    }

    public void RestoreHealth()
    {
        CurrentHealth = maxHealth;
        healthBar.value = CurrentHealth;
    }
}