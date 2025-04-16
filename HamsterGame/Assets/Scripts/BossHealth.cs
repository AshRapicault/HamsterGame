using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;
    public Slider healthBar;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    void Start()
    {
        CurrentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = CurrentHealth;
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
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    void Die()
    {
        FindObjectOfType<SeedSpawner>().StopSpawning();
        Destroy(gameObject);
    }

    public void RestoreHealth()
    {
        CurrentHealth = maxHealth;
        healthBar.value = CurrentHealth;
    }
}