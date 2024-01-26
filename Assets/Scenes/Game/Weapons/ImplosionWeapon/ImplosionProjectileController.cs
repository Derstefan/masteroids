using System.Collections;
using UnityEngine;

public class ImplosionProjectileController : ProjectileController
{
    public AudioClip explosionSound;
    public Color explosionColor = new Color(1f, 0.74f, 0.2f); // Orange color
    public float explosionDuration = 0.15f;

    public float explosionForce;
    [HideInInspector]
    public float explosionRadius;
    [HideInInspector]
    public float explosionDamage;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public float friction = 0.3f; // Set your desired friction value
    private float startTime;
    private bool explosionProcessStarted = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    private void Update()
    {

        rb.velocity *= 1 - friction * Time.deltaTime;
        if (!explosionProcessStarted)
        {
            return;
        }
        // Calculate the progress of the explosion effect
        float progress = (Time.time - startTime) / explosionDuration;


        if (progress < 1f)
        {
            // Scale up the circle
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * explosionRadius * 2.5f, progress);


            // Change the color smoothly
            spriteRenderer.color = Color.Lerp(Color.white, explosionColor, progress);

            // Make it more transparent
            Color tempColor = spriteRenderer.color;
            tempColor.a = Mathf.Lerp(1f, 0f, progress);
            spriteRenderer.color = tempColor;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void goProjectile()
    {

        //explode after lifetime
        StartCoroutine(ExplodeAfterTime(lifeTime));
        //Destroy(gameObject, lifeTime + 0.1f);
        GetComponent<Rigidbody2D>()
          .AddForce(transform.up * force);
        //give also rotation force
        GetComponent<Rigidbody2D>()
          .AddTorque(2f);

    }


    public IEnumerator ExplodeAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("ExplodeAfterTime " + delay);

        Explode();
    }

    public void StartExplosionEffect()
    {
        explosionProcessStarted = true;
        startTime = Time.time;
    }

    public override void Explode()
    {
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
        StartExplosionEffect();
        Vector3 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, explosionRadius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D hitRb = hit.GetComponent<Rigidbody2D>();
            float explForce = explosionForce;
            if (hit.gameObject.CompareTag("Ship") || hit.gameObject.CompareTag("PlayerProjectile"))
            {
                explForce *= 0.05f; //force reduce for ship
            }

            if (hitRb != null)
            {
                Utils.AddImplosionForce(hitRb, explForce, transform.position, explosionRadius, 0.0F, ForceMode2D.Impulse);
            }

            if (hit.gameObject.CompareTag("Enemy") || hit.gameObject.CompareTag("Asteroid"))
            {
                hit.gameObject.GetComponent<AsteroidController>().doDemage(explosionDamage);
            }
        }


    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Asteroid"))
        {
            // Explode();
        }
        else if (c.gameObject.CompareTag("Enemy"))
        {
            // Explode();
        }
        else if (c.gameObject.CompareTag("EnemyProjectile"))
        {
            // Explode();
        }
    }



}
