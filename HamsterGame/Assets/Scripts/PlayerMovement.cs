using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

}
