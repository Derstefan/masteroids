using UnityEngine;

public class ShipController : MonoBehaviour
{
    private ShipStats stats;

    //    public AudioClip crash;


    public GameObject[] allWeapons;

    public GameObject[] weapons;

    private int currentWeaponIndex = 0;

    private float timeSinceLastShot = 0f;

    private GameController gameController;

    void Start()
    {
        stats = new ShipStats();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();


        // copy weapons from allWeapons to weapons
        weapons = new GameObject[allWeapons.Length];
        for (int i = 0; i < allWeapons.Length; i++)
        {
            weapons[i] = allWeapons[i];
        }
    }


    void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        direction.Normalize();
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float currentAngle = transform.rotation.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, stats.rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
        Vector2 forceDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        GetComponent<Rigidbody2D>().AddForce(forceDirection * stats.thrustForce);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Asteroid") || c.gameObject.CompareTag("EnemyProjectile"))
        {
            Debug.Log("Ship hit");
            // AudioSource.PlayClipAtPoint(crash, Camera.main.transform.position);
            transform.position = new Vector3(0, 0, 0);
            GetComponent<Rigidbody2D>().velocity = Vector3.zero; // Remove all velocity from the ship
            gameController.DecrementLives();
        }
    }




    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        // Change weapon with mouse scroll wheel
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel > 0f)
        {
            CycleWeapon(1); // Scroll up
        }
        else if (scrollWheel < 0f)
        {
            CycleWeapon(-1); // Scroll down
        }


        float attackRate = 1f / (weapons[currentWeaponIndex].GetComponent<WeaponController>().attackSpeed + stats.attackSpeed);
        if (Input.GetMouseButton(0) && timeSinceLastShot >= attackRate)
        {
            shoot();
            timeSinceLastShot = 0f;
        }
    }

    void CycleWeapon(int direction)
    {
        // Change the current weapon index based on the direction of the scroll
        currentWeaponIndex = (currentWeaponIndex + direction + weapons.Length) % weapons.Length;
    }

    private void shoot()
    {
        weapons[currentWeaponIndex].GetComponent<WeaponController>().shoot(new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    }
}
