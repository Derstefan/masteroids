using System.Collections;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    public float spawnStartTimer = 0f;
    public float spawnIntervall = 10f;
    public int spawnAmount = 30;
    public int spawnAmountIncrease = 10;


    public AudioClip destroySound;
    public GameObject splitterObjects;
    public int splitterAmount = 3;
    public float maxLife = 100f;

    public bool spawnExp = true;
    private float currentLife;



    GameController GC;

    void Start()
    {

        GC = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        currentLife = maxLife;
        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * Random.Range(-50.0f, 150.0f));
        GetComponent<Rigidbody2D>()
            .angularVelocity = Random.Range(-0.0f, 90.0f);

    }


    void OnCollisionEnter2D(Collision2D c)
    {

        if (c.gameObject.CompareTag("PlayerProjectile"))
        {

            float demage = c.gameObject.GetComponent<ProjectileController>().demage;
            StartCoroutine(SmoothBlink());
            currentLife -= demage;

            if (currentLife < 0)
            {
                if (gameObject == null) return;
                GC.destroyAsteroid(gameObject);
                if (splitterObjects != null)
                {
                    spawnSmallerAsteroids(splitterAmount);
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

            GameObject splitterAsteroid = GC.spawnAsteroid(
                splitterObjects,
                transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * 0.5f,
                Quaternion.Euler(0, 0, angle));

            splitterAsteroid.GetComponent<Rigidbody2D>().velocity = velocity;
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



