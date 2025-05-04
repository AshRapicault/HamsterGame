using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f;
    private bool drive = false;

    public void StartDriving()
    {
        drive = true;
    }

    private void Update()
    {
        if (drive)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime); 
        }
    }
}
