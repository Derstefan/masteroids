using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public AudioClip destroy;
    public GameObject smallAsteroid;

    private GameController gameController;
    public float maxLife = 10f;
    private float life;

    // Use this for initialization
    void Start()
    {
        life = maxLife;

        GameObject gameControllerObject =
            GameObject.FindWithTag("GameController");

        gameController =
            gameControllerObject.GetComponent<GameController>();

        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * Random.Range(-50.0f, 150.0f));

        GetComponent<Rigidbody2D>()
            .angularVelocity = Random.Range(-0.0f, 90.0f);

    }


    void OnCollisionEnter2D(Collision2D c)
    {

        if (c.gameObject.tag.Equals("Bullet"))
        {

            c.gameObject.GetComponent<BulletController>().Explode();
            float demage = c.gameObject.GetComponent<BulletController>().demage;
            life -= demage;

            Destroy(c.gameObject);
            if (life < 0)
            {
                Destroy(gameObject);
                // If large asteroid spawn new ones
                if (smallAsteroid != null)
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

        void spawSmallerAsteroids()
        {
            Instantiate(smallAsteroid,
                new Vector3(transform.position.x - .5f,
                    transform.position.y - .5f, 0),
                    Quaternion.Euler(0, 0, 90));

            Instantiate(smallAsteroid,
                new Vector3(transform.position.x + .5f,
                    transform.position.y + .0f, 0),
                    Quaternion.Euler(0, 0, 0));

            Instantiate(smallAsteroid,
                new Vector3(transform.position.x + .5f,
                    transform.position.y - .5f, 0),
                    Quaternion.Euler(0, 0, 270));

        }
    }


}
