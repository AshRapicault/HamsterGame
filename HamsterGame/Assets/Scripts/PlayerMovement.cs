using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //the way the hamster is facing when moving left/right
        if (horizontalInput > 0.1f)
        {
            transform.localScale = new Vector2(2,2);
        }
        else if(horizontalInput < 0.0f)
        {
            transform.localScale = new Vector2(-2,2);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.velocity = new Vector2(body.velocity.x, speed);
        }
    }
}
