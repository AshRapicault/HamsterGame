using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    [SerializeField] private CollectiblesManager cm;
    [SerializeField] private gameOverScript gameOver;


    private float normalSpeed;
    private bool speedBoostActive = false;
    private float speedBoostDuration = 10f;
    private float speedBoostTimer = 0f;


    private void Awake()
    {
        normalSpeed = speed;
        // Get references for your components
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (speedBoostActive)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0f)
            {
                speed = normalSpeed;
                speedBoostActive = false;
            }
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // The way the hamster is facing when moving left/right
        if (horizontalInput > 0.1f)
        {
            transform.localScale = new Vector2(1, 1);  // Facing right
        }
        else if (horizontalInput < -0.1f)
        {
            transform.localScale = new Vector2(-1, 1); // Facing left
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }

        // Set walking animation parameter
        anim.SetBool("walking", horizontalInput != 0);

        // Set jumping animation parameter
        anim.SetBool("jumping", !grounded); // Player is jumping if not grounded
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
        anim.SetBool("grounded", grounded);
        anim.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            anim.SetBool("grounded", grounded);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            grounded = true;
            anim.SetBool("grounded", grounded);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        bool isLevel2 = SceneManager.GetActiveScene().name == "Level2";

        if (other.gameObject.CompareTag("PointCollectible"))
        {
            Destroy(other.gameObject);
            cm.countPoints++;
        }
        else if (other.gameObject.CompareTag("AttackCollectible"))
        {
            if (isLevel2 || cm.countAttackSeeds < 3)
            {
                Destroy(other.gameObject);
                cm.countAttackSeeds++;
            }
        }

        if (other.gameObject.CompareTag("DeadlyObstacle"))
        {
            Destroy(gameObject);
            gameOver.GameOver();
        }
    }

    private void ActivateSpeedBoost()
    {
        speed += 5;
        speedBoostActive = true;
        speedBoostTimer = speedBoostDuration;
    }
}