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

    void OnTriggerEnter2D(Collider2D c)
    {
        // Debug.Log("EnemyProjectileController OnCollisionEnter2D " + c.gameObject.tag);
        if (c.gameObject.CompareTag("Ship"))
        {
            Explode();
        }
    }
}
