using UnityEngine;

public class BounceProjectileController : ProjectileController
{
    public override void goProjectile()
    {
        Destroy(gameObject, lifeTime);
        GetComponent<Rigidbody2D>()
          .AddForce(transform.up * force);

        //give also rotation force
        GetComponent<Rigidbody2D>()
          .AddTorque(20f);
    }

    public override void Explode()
    {
        //Destroy(gameObject);
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
