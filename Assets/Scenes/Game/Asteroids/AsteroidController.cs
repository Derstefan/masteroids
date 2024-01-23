using System.Collections;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public bool activated = true;

    public float spawnStartTimer = 0f;
    public float spawnIntervall = 10f;
    public int spawnAmount = 30;
    public int spawnAmountIncrease = 10;


    public AudioClip destroySound;
    public GameObject splitterObjects;
    public int splitterAmountMin = 3;
    public int splitterAmountMax = 7;
    public float splitterforceMultiplier = 1.5f;

    public float maxLife = 100f;
    public int destructionScore = 5;
    public int singleHitScore = 1;

    public int spawnExp = 1;
    private float currentLife;




    GameController GC;

    void Start()
    {
        GC = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        currentLife = maxLife;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (!activated)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
            rb.isKinematic = true;

        }

        rb.AddForce(transform.up * Random.Range(-50.0f, 150.0f));
        rb.angularVelocity = Random.Range(-0.0f, 90.0f);

    }

    public void activate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        activated = true;
        rb.isKinematic = false;
    }

    private bool isSpawning = false;


    void OnCollisionEnter2D(Collision2D c)
    {

        if (c.gameObject.CompareTag("PlayerProjectile"))
        {

            float demage = c.gameObject.GetComponent<ProjectileController>().demage;
            StartCoroutine(SmoothBlink());
            currentLife -= demage;
            GC.RaiseHighscore(singleHitScore);

            if (currentLife < 0)
            {

                if (gameObject == null) return;
                GC.destroyAsteroid(gameObject);
                GC.RaiseHighscore(destructionScore);
                if (splitterObjects != null && isSpawning == false)
                {
                    isSpawning = true;
                    spawnSmallerAsteroids(Random.Range(splitterAmountMin, splitterAmountMax));
                    isSpawning = false;

                }

                // Play a sound
                //            AudioSource.PlayClipAtPoint(
                //               destroy, Camera.main.transform.position);
                // Add to the score
                // gameController.IncrementScore();

            }
        }
    }

    void spawnSmallerAsteroids(int numberOfAsteroids)
    {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;

        for (int i = 0; i < numberOfAsteroids; i++)
        {
            float angle = i * 360f / numberOfAsteroids;

            // Calculate a vector pointing away from the center
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);

            GameObject splitterAsteroid = GC.spawnAsteroid(
                splitterObjects,
                transform.position + direction * 0.5f,
                Quaternion.Euler(0, 0, angle));

            splitterAsteroid.GetComponent<Rigidbody2D>().velocity = velocity;

            // Calculate a force direction away from the center
            Vector2 forceDirection = direction;

            splitterAsteroid.GetComponent<Rigidbody2D>()
                .AddForce(forceDirection * Random.Range(150.0f * splitterforceMultiplier, 1650.0f * splitterforceMultiplier));
        }
    }



    private IEnumerator SmoothBlink()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float elapsedTime = 0f;
        float startAlpha = spriteRenderer.color.a;
        Color startColor = Color.white; // Start color is white
        Color targetColor = Color.red; // Target color is red

        while (elapsedTime < Config.blinkDuration)
        {
            // Interpolate between start and target alpha values
            float alpha = Mathf.Lerp(startAlpha, currentLife / maxLife, elapsedTime / Config.blinkDuration);

            // Interpolate between start and target colors based on life and maxLife
            Color lerpedColor = Color.Lerp(targetColor, startColor, currentLife / maxLife);

            // Set the sprite's alpha and color values
            Color newColor = spriteRenderer.color;
            newColor.a = alpha;
            newColor = lerpedColor; // Set the lerped color
            spriteRenderer.color = newColor;

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the sprite is fully visible at the end
        Color finalColor = spriteRenderer.color;
        finalColor.a = startAlpha;
        spriteRenderer.color = finalColor;
    }

}



