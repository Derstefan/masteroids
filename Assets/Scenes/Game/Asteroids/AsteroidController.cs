using System.Collections;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    public float spawnStartTimer = 0f;
    public float spawnIntervall = 10f;
    public int spawnAmount = 30;
    public int spawnAmountIncrease = 10;

    public float lastSpawnTime = 0f;


    public AudioClip destroySound;
    public GameObject splitterObjects;
    public float maxLife = 10f;
    private float currentLife;

    private float blinkDuration = 0.05f;

    void Start()
    {
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
                Destroy(gameObject);
                if (splitterObjects != null)
                {
                    spawSmallerAsteroids();
                }

                // Play a sound
                //            AudioSource.PlayClipAtPoint(
                //               destroy, Camera.main.transform.position);
                // Add to the score
                // gameController.IncrementScore();

            }

        }
    }

    void spawSmallerAsteroids()
    {

        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;

        GameObject o1 = Instantiate(splitterObjects,
            new Vector3(transform.position.x - .5f,
                transform.position.y - .5f, 0),
                Quaternion.Euler(0, 0, 90));
        o1.GetComponent<Rigidbody2D>().velocity = velocity;

        GameObject o2 = Instantiate(splitterObjects,
            new Vector3(transform.position.x + .5f,
                transform.position.y + .0f, 0),
                Quaternion.Euler(0, 0, 0));
        o2.GetComponent<Rigidbody2D>().velocity = velocity;


        GameObject o3 = Instantiate(splitterObjects,
            new Vector3(transform.position.x + .5f,
                transform.position.y - .5f, 0),
                Quaternion.Euler(0, 0, 270));
        o3.GetComponent<Rigidbody2D>().velocity = velocity;


    }



    private IEnumerator SmoothBlink()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float elapsedTime = 0f;
        float startAlpha = spriteRenderer.color.a;
        Color startColor = Color.white; // Start color is white
        Color targetColor = Color.red; // Target color is red

        while (elapsedTime < blinkDuration)
        {
            // Interpolate between start and target alpha values
            float alpha = Mathf.Lerp(startAlpha, currentLife / maxLife, elapsedTime / blinkDuration);

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



