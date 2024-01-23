using UnityEngine;

public class ShipAttackController : MonoBehaviour
{
    public GameObject shipPrefab;
    public float rotationForce = 5f;
    public float forwardForceMin = 150f;
    public float forwardForceMax = 300f;
    private float forwardForce;

    public float maxSpeed = 20f; // Set your desired maximum speed here

    private Rigidbody2D rb;

    private void Start()
    {

        // Set a random forward force
        forwardForce = Random.Range(forwardForceMin, forwardForceMax);
        rb = GetComponent<Rigidbody2D>();

        if (shipPrefab == null)
        {
            Debug.LogError("Ship prefab not assigned!");
        }
    }

    private void Update()
    {
        if (shipPrefab != null)
        {
            MoveTowardsShip();
        }

        LimitMaxSpeed();
    }

    private void MoveTowardsShip()
    {
        // Calculate the direction vector towards the shipPrefab
        Vector3 direction = (shipPrefab.transform.position - transform.position).normalized;

        // Calculate the angle to rotate towards the ship
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Apply rotation force to rotate towards the ship
        rb.rotation = Mathf.LerpAngle(rb.rotation, angle, rotationForce * Time.deltaTime);

        // Apply forward force to move towards the ship
        rb.AddForce(transform.up * forwardForce * Time.deltaTime);
    }

    private void LimitMaxSpeed()
    {
        // Limit the maximum speed of the ship
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
