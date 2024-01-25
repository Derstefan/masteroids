using UnityEngine;

public class BiggerShotProjectileController : ProjectileController
{
    public override void goProjectile()
    {
        Destroy(gameObject, lifeTime);
        GetComponent<Rigidbody2D>()
          .AddForce(transform.up * force);
    }

    public override void Explode()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Asteroid"))
        {
            Explode();
        }
        else if (c.gameObject.CompareTag("Enemy"))
        {
            Explode();
        }
        else if (c.gameObject.CompareTag("EnemyProjectile"))
        {
            Explode();
        }
    }



}
