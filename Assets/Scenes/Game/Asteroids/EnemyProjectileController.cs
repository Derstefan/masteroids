using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    //public float demage = 3f;
    public float lifeTime = 8f;
    public float projectilSpeed = 10f;


    public void Update()
    {
        transform.position += transform.up * Time.deltaTime * projectilSpeed;
    }
    public void goProjectile()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Explode()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Ship"))
        {
            Explode();
        }
    }
}
