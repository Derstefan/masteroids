using System.Collections;
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

    [Header("Events")]
    public GameEvent OnProgressChanged;
    public GameEvent OnLevelUp;

    [Header("Skill selection sprites")]
    public Sprite rotationSpeed_sprite;
    public Sprite speed_sprite;
    public Sprite health_sprite;
    public Sprite attackSpeed_sprite;


    public int lvl = 1;

    private SkillManager skillManager;
    [HideInInspector]
    public ShipStats shipStats;

    void Awake()
    {
        shipStats = new ShipStats(rotationSpeed_sprite, speed_sprite, health_sprite, attackSpeed_sprite);
        init();
    }
    void Start()
    {       
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();      
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
        learn(this, "Simple Weapon");

        /*
        foreach(Skill skill in skillManager.allSkills)
        {
            Debug.Log(skill.name);
        }
        */
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

    public void learn(Component sender, object data)
    {
        if(data is string)
        {
            skillManager.learnSkill((string) data);
            Debug.Log("Learn skill: " + data);
            checkUnlockedWeapons();
            ResumeGame();
        }        
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



    private Coroutine controlCoroutine;
    private Coroutine timeSlowCoroutine;
    private float timeSlowFactor = 0.5f;
    private float timeSlowDuration = 2f;
    private float smoothTime = 0.2f; // 70 ms in seconds

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Asteroid") || c.gameObject.CompareTag("EnemyProjectile") || c.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Ship hit");

            // Stop the existing coroutines if they are running
            if (controlCoroutine != null)
            {
                StopCoroutine(controlCoroutine);
            }
            if (timeSlowCoroutine != null)
            {
                StopCoroutine(timeSlowCoroutine);
            }

            // Set temporary the is trigger from polygon collider to false
            GetComponent<PolygonCollider2D>().isTrigger = false;

            // Start a new coroutine and store a reference to it
            controlCoroutine = StartCoroutine(GiveControlOverShipAfterDelay(shipStats.falterDuration));

            // Apply time slow
            timeSlowCoroutine = StartCoroutine(TimeSlowCoroutine());
        }
    }

    public IEnumerator GiveControlOverShipAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Enable the trigger back
        GetComponent<PolygonCollider2D>().isTrigger = true;
        // Set rotation velocity to 0
        GetComponent<Rigidbody2D>().angularVelocity = 0f;

        // Clear the coroutine reference
        controlCoroutine = null;
    }

    public IEnumerator TimeSlowCoroutine()
    {
        float initialTimeScale = Time.timeScale;
        float targetTimeScale = timeSlowFactor;
        float elapsedTime = 0f;

        while (elapsedTime < smoothTime)
        {
            // Interpolate the time scale gradually
            Time.timeScale = Mathf.Lerp(initialTimeScale, targetTimeScale, elapsedTime / smoothTime);

            // Increment the elapsed time
            elapsedTime += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        // Ensure the final time scale is set to the target
        Time.timeScale = targetTimeScale;

        // Wait for the specified duration
        yield return new WaitForSeconds(timeSlowDuration);

        // Reset time scale to normal
        Time.timeScale = 1f;

        // Clear the coroutine reference
        timeSlowCoroutine = null;
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

        //Debug.Log("Shipstats attack " + shipStats.attackSpeed);

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
        OnProgressChanged.Raise(this, (float) shipStats.exp / (float) shipStats.expToNextLvl);

        if (shipStats.exp >= shipStats.expToNextLvl)
        {
            lvl++;
            shipStats.exp = 0;
            OnProgressChanged.Raise(this, (float)shipStats.exp);
            shipStats.expToNextLvl = (int)(shipStats.expToNextLvl * 1.2);

            Debug.Log("Level up! next " + shipStats.expToNextLvl);

            Skill[] skills = getRandomLearnableSkills(3);
            PauseGame();
            //Debug.Log("Learned skills: " + skills[0].name + " " + skills[1].name + " " + skills[2].name);
            OnLevelUp.Raise(this, skills);
           
            //learn(skills[0].name);
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
