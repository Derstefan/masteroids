using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float demage = 3f;
    // Use this for initialization
    void Start()
    {
        // Set the bullet to destroy itself after 1 seconds
        Destroy(gameObject, 8.0f);

        // Push the bullet in the direction it is facing
        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * 400);
    }

    public void Explode()
    {

    }
}
