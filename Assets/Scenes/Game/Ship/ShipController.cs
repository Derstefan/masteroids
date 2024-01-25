using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{


    public AudioClip damageSound;
    public AudioClip levelUpSound;


    public GameObject[] allWeaponPrefabs;
    [HideInInspector]
    public GameObject[] allWeapons;
    [HideInInspector]
    public GameObject[] weapons;

    private int currentWeaponIndex = 0;

    private float timeSinceLastShot = 99f;

    private GameController gameController;

    [Header("Events")]
    public GameEvent OnProgressChanged;
    public GameEvent OnLevelUp;
    public GameEvent OnWeaponChanged;

    [Header("Skill selection sprites")]
    public Sprite rotationSpeed_sprite;
    public Sprite speed_sprite;
    public Sprite health_sprite;
    public Sprite attackSpeed_sprite;


    public int lvl = 1;

    private SkillManager skillManager;
    [HideInInspector]
    public ShipStats shipStats;
    private bool gameOver = false;



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
        if (data is string)
        {
            skillManager.learnSkill((string)data);
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

    public void doDamage(float amount)
    {
        AudioSource.PlayClipAtPoint(damageSound, Camera.main.transform.position);
        shipStats.currentHealth -= amount;
        float h = shipStats.currentHealth / shipStats.maxHealth;
        GetComponent<SpriteRenderer>().color = new Color(1f, h, h);

        if (shipStats.currentHealth <= 0)
        {
            gameOver = true;
            StartCoroutine(ShakeScreen(1f, 1f));
            StartCoroutine(gotToMainManu(0.8f));

        }
        else
        {
            StartCoroutine(ShakeScreen(0.1f, 0.1f)); // Adjust duration and intensity as needed
        }
    }




    public void doHeal(float amount)
    {
        shipStats.currentHealth += amount;
        float h = shipStats.currentHealth / shipStats.maxHealth;
        GetComponent<SpriteRenderer>().color = new Color(1f, h, h);
    }

    IEnumerator gotToMainManu(float sec)
    {
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene("menu");
    }

    IEnumerator ShakeScreen(float duration, float intensity)
    {
        Vector3 originalPosition = Camera.main.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = originalPosition.x + Random.Range(-intensity, intensity);
            float y = originalPosition.y + Random.Range(-intensity, intensity);
            Camera.main.transform.position = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = originalPosition;
    }

    private Coroutine controlCoroutine;
    private Coroutine timeSlowCoroutine;
    private float timeSlowFactor = 0.9f;
    private float timeSlowDuration = 1f;
    private float smoothTime = 0.2f;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Asteroid") || c.gameObject.CompareTag("EnemyProjectile") || c.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Ship hit");

            doDamage(10);


            // Stop the existing coroutines if they are running
            if (controlCoroutine != null && Time.timeScale != 0)
            {
                StopCoroutine(controlCoroutine);
            }
            if (timeSlowCoroutine != null && Time.timeScale != 0)
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
            CycleWeapon(1);
            timeSinceLastShot = 99f;

        }
        else if (scrollWheel < 0f)
        {
            CycleWeapon(-1);
            timeSinceLastShot = 99f;

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
        GameObject weapon = weapons[currentWeaponIndex];
        Sprite skillImage = weapon.GetComponent<WeaponController>().sprite;
        OnWeaponChanged.Raise(this, skillImage);
    }

    private void shoot()
    {
        float halfHeight = GetComponent<Renderer>().bounds.extents.y;

        Vector3 shootingPosition = transform.position + (transform.up * halfHeight * 1.3f);
        if (weapons[currentWeaponIndex] == null) throw new System.Exception("No weapon available");
        if (weapons[currentWeaponIndex].GetComponent<WeaponController>() == null) throw new System.Exception("No weapon controller available");
        weapons[currentWeaponIndex].GetComponent<WeaponController>().shoot(shootingPosition, transform.rotation);
    }

    public void incrementExp()
    {
        shipStats.exp++;
        gameController.RaiseHighscore();
        OnProgressChanged.Raise(this, (float)shipStats.exp / (float)shipStats.expToNextLvl);

        if (shipStats.exp >= shipStats.expToNextLvl)
        {
            AudioSource.PlayClipAtPoint(levelUpSound, Camera.main.transform.position);
            lvl++;
            shipStats.exp = 0;
            OnProgressChanged.Raise(this, (float)shipStats.exp);
            shipStats.expToNextLvl = (int)(shipStats.expToNextLvl * 1.2);

            //Debug.Log("Level up! next " + shipStats.expToNextLvl);

            Skill[] skills = getRandomLearnableSkills(3);
            PauseGame();
            OnLevelUp.Raise(this, skills);

            //Debug.Log("Learned skills: " + skills[0].name + " " + skills[1].name + " " + skills[2].name);
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
