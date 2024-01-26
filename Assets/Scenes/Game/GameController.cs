using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] asteroids;

    [HideInInspector]
    public float[] lastSpawnTimes;
    public GameObject ship;

    public GameObject expPrefab;
    public GameObject healPrefab;

    [Header("Events")]
    public GameEvent OnScoreChanged;

    // private int score;
    private int _highscore;
    public int highscore
    {
        get { return _highscore; }
    }
    private int lives;

    public static int endscore = 0;

    private int asteroidCount = 0;

    private float gameStartedAt;

    void Start()
    {
        gameStartedAt = Time.time;
        lastSpawnTimes = new float[asteroids.Length];
        for (int i = 0; i < asteroids.Length; i++)
        {
            lastSpawnTimes[i] = -99999f;
        }
        //SetHighscore(PlayerPrefs.GetInt("hiscore", 0));
    }

    void Update()
    {

        if (Input.GetKey("escape"))
            Application.Quit();

    }

    private void FixedUpdate()
    {
        checkForNewSpawn();
    }

    void checkForNewSpawn()
    {

        float time = Time.time - gameStartedAt;

        for (int i = 0; i < asteroids.Length; i++)
        {
            if (asteroids[i] == null) continue;

            AsteroidController asteroidController = asteroids[i].GetComponent<AsteroidController>();
            if (asteroidController == null) continue;
            if (time < asteroidController.spawnStartTimer) continue;

            if (time > asteroidController.spawnEndTimer) continue;

            if (time < lastSpawnTimes[i] + asteroidController.spawnIntervall) continue;

            int numberOfSpawns = (int)((time / asteroidController.spawnIntervall) + asteroidController.spawnStartTimer);
            SpawnAsteroids(asteroids[i], asteroidController.spawnAmount + numberOfSpawns * asteroidController.spawnAmountIncrease);


            lastSpawnTimes[i] = (time);
        }
    }


    void SpawnAsteroids(GameObject asteroid, int amount)
    {
        float count = 0;

        for (int i = 0; i < amount; i++)
        {
            Vector3 pos;
            int maxAttempts = 10; // Maximum attempts to find a suitable spawn position
            int attempts = 0;

            do
            {
                pos = new Vector3(Random.Range(MapConfig.XMin, MapConfig.XMax),
                                  Random.Range(MapConfig.YMin, MapConfig.YMax), 0);
                attempts++;

            } while ((IsPositionNearShip(pos) || HasOverlap(pos)) && attempts < maxAttempts);

            if (attempts < maxAttempts)
            {
                count++;
                GameObject o = spawnAsteroid(asteroid, pos, Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));
                Debug.Log("Spawned " + o.name);
                o.GetComponent<Rigidbody2D>().AddForce(o.transform.up * Random.Range(-50.0f, 6250.0f));


            }
            else
            {
                Debug.Log("Could not spawn asteroid");
            }
        }
        //Debug.Log("Spawn " + count + " asteroids");
    }

    public GameObject spawnAsteroid(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        asteroidCount++;
        GameObject asteroid = Instantiate(prefab, pos, rot);
        asteroid.GetComponent<AsteroidController>().activate();
        return asteroid;
    }

    public void destroyAsteroid(GameObject asteroid)
    {
        int num = asteroid.GetComponent<AsteroidController>().spawnExp;
        float maxOffset = 0.2f;

        if (Random.Range(0, 100) < 2)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-maxOffset, maxOffset), Random.Range(-maxOffset, maxOffset), 0f);
            Instantiate(healPrefab, asteroid.transform.position + randomOffset, Quaternion.identity);
        }

        if (Random.Range(0, 100) < 1)
        {
            maxOffset += 0.1f;
            num += 7;
        }
        for (int i = 0; i < num; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-maxOffset, maxOffset), Random.Range(-maxOffset, maxOffset), 0f);
            Instantiate(expPrefab, asteroid.transform.position + randomOffset, Quaternion.identity);
        }



        // if its skiping asteroid
        SpikeController sc = asteroid.GetComponent<SpikeController>();
        if (asteroid.GetComponent<SpikeController>() != null)
        {
            sc.CreateAndRotateSpikes();
        }

        //destroy asteroid
        //asteroidCount--;
        //Destroy(asteroid);
        asteroid.GetComponent<AsteroidController>().isDestroyed = true;
        StartCoroutine(FadeOutAndDestroy(asteroid, 0.2f, 0.2f));


    }

    private IEnumerator FadeOutAndDestroy(GameObject asteroid, float fadeDuration, float delayBeforeDestroy)
    {
        SpriteRenderer asteroidRenderer = asteroid.GetComponent<SpriteRenderer>();

        // Gradually fade out the asteroid
        float elapsedTime = 0f;
        Color initialColor = asteroidRenderer.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            asteroidRenderer.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the final color to ensure it reaches the target color
        asteroidRenderer.color = targetColor;

        // Delay before destroying the asteroid
        yield return new WaitForSeconds(delayBeforeDestroy);

        // Additional logic after fading, if needed

        // Destroy the asteroid
        asteroidCount--;
        Destroy(asteroid);
    }


    bool HasOverlap(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, Config.minOverlapDistance);

        // Check if there are too many overlaps
        return colliders.Length > Config.maxOverlapCount;
    }

    bool IsPositionNearShip(Vector3 position)
    {

        float distance = Vector3.Distance(position, ship.transform.position);
        return distance < Config.minDistance;
    }


    public void SetHighscore(int value)
    {
        _highscore = value;
        OnScoreChanged.Raise(this, _highscore);
    }

    public void RaiseHighscore(int value = 1)
    {
        _highscore += value;
        OnScoreChanged.Raise(this, _highscore);
    }

    public void SetEndscore()
    {
        endscore = _highscore;
        Debug.Log("Endscore " + endscore);
    }
}
