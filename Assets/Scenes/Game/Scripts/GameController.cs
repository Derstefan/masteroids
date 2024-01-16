using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject asteroid;

    private int score;
    private int hiscore;
    private int asteroidsRemaining;
    private int lives;
    private int wave;
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

    void BeginGame()
    {

        score = 0;
        lives = 3;
        wave = 1;

        // Prepare the HUD
        //  scoreText.text = "SCORE:" + score;
        //  hiscoreText.text = "HISCORE: " + hiscore;
        // livesText.text = "LIVES: " + lives;
        //  waveText.text = "WAVE: " + wave;

        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {

        for (int i = 0; i < wave * increaseEachWave; i++)
        {

            // Spawn an asteroid
            Instantiate(asteroid,
                new Vector3(Random.Range(MapConfig.XMin, MapConfig.XMax),
                    Random.Range(MapConfig.YMin, MapConfig.YMax), 0),
                Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));

        }

        // waveText.text = "WAVE: " + wave;
    }

    public void DecrementLives()
    {
        lives--;
        //  livesText.text = "LIVES: " + lives;

        if (lives < 1)
        {
            BeginGame();
        }
    }

}
