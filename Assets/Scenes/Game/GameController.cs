using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] asteroids;

    private int score;
    private int hiscore;
    private int lives;
    private int increaseEachWave = 50;

    void Start()
    {

        hiscore = PlayerPrefs.GetInt("hiscore", 0);
        BeginGame();
    }

    void Update()
    {

        if (Input.GetKey("escape"))
            Application.Quit();


    }

    void checkForNewSpawn()
    {
        foreach (GameObject asteroid in asteroids)
        {
            if (asteroid == null) return;
            AsteroidController asteroidController = asteroid.GetComponent<AsteroidController>();
            if (asteroidController == null) return;
            if (Time.time < asteroidController.spawnStartTimer) return;
            if (Time.time < asteroidController.lastSpawnTime + asteroidController.spawnIntervall) return;



        }
    }

    void BeginGame()
    {

        score = 0;
        lives = 3;

        // Prepare the HUD
        //  scoreText.text = "SCORE:" + score;
        //  hiscoreText.text = "HISCORE: " + hiscore;
        // livesText.text = "LIVES: " + lives;
        //  waveText.text = "WAVE: " + wave;
        for (int i = 0; i < increaseEachWave; i++)
        {
            SpawnAsteroids(asteroids[0]);
        }
    }

    void SpawnAsteroids(GameObject asteroid)
    {

        Vector3 pos = new Vector3(Random.Range(MapConfig.XMin, MapConfig.XMax),
                    Random.Range(MapConfig.YMin, MapConfig.YMax), 0);


        Instantiate(asteroid, pos,
                Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));
    }

    public void DecrementLives()
    {
        lives--;
        if (lives < 1)
        {
            BeginGame();
        }
    }

}
