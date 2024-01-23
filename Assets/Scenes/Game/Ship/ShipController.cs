using System.Linq;
using UnityEngine;

public class ShipController : MonoBehaviour
{


    //    public AudioClip crash;


    public GameObject[] allWeaponPrefabs;
    [HideInInspector]
    public GameObject[] allWeapons;
    [HideInInspector]
    public GameObject[] weapons;

    private int currentWeaponIndex = 0;

    private float timeSinceLastShot = 0f;

    private GameController gameController;


    public int lvl = 1;

    private SkillManager skillManager;
    [HideInInspector]
    public ShipStats shipStats;

    void Start()
    {
        shipStats = new ShipStats();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        init();

    }

    void init()
    {
        allWeapons = new GameObject[allWeaponPrefabs.Length];
        for (int i = 0; i < allWeaponPrefabs.Length; i++)
        {
            allWeapons[i] = Instantiate(allWeaponPrefabs[i]);
            allWeapons[i].transform.parent = transform;
        }
        skillManager = new SkillManager(this);
        skillManager.addStatSkills(shipStats.getSkills());
        foreach (GameObject weapon in allWeapons)
        {
            skillManager.addWeaponSkills(weapon.GetComponent<WeaponController>().getWeaponSkills());
        }
        // startSkills
        learn("Simple Weapon");

    }

    void checkUnlockedWeapons()
    {
        foreach (GameObject weapon in allWeapons)
        {
            if (weapon.GetComponent<WeaponController>().activated == true && !weapons.Contains(weapon))
            {
                weapons = weapons.Concat(new GameObject[] { weapon }).ToArray();
            }
        }
    }

    public void learn(string skillName)
    {

        skillManager.learnSkill(skillName);
        checkUnlockedWeapons();
        ResumeGame();
    }

    public Skill[] getRandomLearnableSkills(int amount)
    {
        Skill[] skills = new Skill[amount];
        for (int i = 0; i < amount; i++)
        {
            skills[i] = skillManager.getLearnableSkill();
        }
        return skills;

    }


    void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        direction.Normalize();
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float currentAngle = transform.rotation.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, shipStats.rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);

        Vector2 forceDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Apply force with a maximum speed constraint
        float currentSpeed = rb.velocity.magnitude;
        if (currentSpeed < shipStats.maxSpeed)
        {
            rb.AddForce(forceDirection * shipStats.speed);
        }

        // Adjust camera orthographic size based on velocity
        float velocityMagnitude = rb.velocity.magnitude;

        float zoomFactor = Mathf.Clamp(velocityMagnitude * 0.1f, 0f, 1f); // Adjust this factor based on your preference

        Camera.main.orthographicSize = Mathf.Lerp(Config.minZoom, Config.maxZoom, zoomFactor);
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

        if (weapons.Length == 0) throw new System.Exception("No weapons available");
        float attackRate = 1f / (weapons[currentWeaponIndex].GetComponent<WeaponController>().attackSpeed + shipStats.attackSpeed);
        if (Input.GetMouseButton(0) && timeSinceLastShot >= attackRate)
        {
            shoot();
            timeSinceLastShot = 0f;
        }
    }

    void CycleWeapon(int direction)
    {
        if (weapons.Length == 0) throw new System.Exception("No weapons available");
        // Change the current weapon index based on the direction of the scroll
        currentWeaponIndex = (currentWeaponIndex + direction + weapons.Length) % weapons.Length;
    }

    private void shoot()
    {
        float halfHeight = GetComponent<Renderer>().bounds.extents.y;

        Vector3 shootingPosition = transform.position + (transform.up * halfHeight * 1.3f);

        weapons[currentWeaponIndex].GetComponent<WeaponController>().shoot(shootingPosition, transform.rotation);
    }

    public void incrementExp()
    {
        shipStats.exp++;
        gameController.RaiseHighscore();

        if (shipStats.exp >= shipStats.expToNextLvl)
        {
            lvl++;
            shipStats.exp = 0;
            shipStats.expToNextLvl = (int)(shipStats.expToNextLvl * 1.5);

            Debug.Log("Level up! next " + shipStats.expToNextLvl);

            Skill[] skills = getRandomLearnableSkills(3);
            PauseGame();
            //Debug.Log("Learned skills: " + skills[0].name + " " + skills[1].name + " " + skills[2].name);
            learn(skills[0].name);
            //Debug.Log("Learnable skills: " + skillManager.getAllLearnableSkills());
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
    }


}
