using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public Collider2D damageCollider;
    public float interval = 5f;
    public gameOverScript gameOverScript;

    private bool isActive = false;
    private float timer;

    void Start()
    {
        gameOverScript = FindObjectOfType<gameOverScript>();

        if (fireParticles == null)
            fireParticles = GetComponent<ParticleSystem>();

        if (damageCollider == null)
            damageCollider = GetComponent<Collider2D>();

        timer = interval;
        SetState(false);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            isActive = !isActive;
            SetState(isActive);
            timer = interval;
        }
    }

    void SetState(bool state)
    {
        if (state)
            fireParticles.Play();
        else
            fireParticles.Stop();

        damageCollider.enabled = state;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag("Player"))
        {
            gameOverScript.GameOver();
        }
    }
}
