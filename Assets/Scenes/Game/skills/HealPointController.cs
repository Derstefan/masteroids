using UnityEngine;

public class HealPointController : MonoBehaviour
{
    public AudioClip collectSound;

    public GameObject ship;
    public float baseMovementSpeed = 1f; // Adjust this value to set the base movement speed
                                         // Adjust this value to set the minimum distance
    public float collectDistance = 0.01f;  // Adjust this value to set the distance for collecting
    public float maxSpeedMultiplier = 4f;  // Adjust this value to set the maximum speed multiplier

    void FixedUpdate()
    {
        if (ship != null)
        {
            float minDistance = ship.GetComponent<ShipController>().shipStats.collectMinDistance;
            float distance = Vector3.Distance(transform.position, ship.transform.position);

            if (distance < minDistance)
            {
                // Calculate the direction to the ship
                Vector3 directionToShip = (ship.transform.position - transform.position).normalized;

                // Calculate the speed multiplier based on the distance
                float speedMultiplier = Mathf.Clamp01(1f - distance / minDistance) * maxSpeedMultiplier;

                // Calculate the new position based on the adjusted movement speed
                Vector3 newPosition = transform.position + directionToShip * (baseMovementSpeed * speedMultiplier) * Time.fixedDeltaTime;

                // Update the position of the ExpController towards the ship
                transform.position = newPosition;

                if (distance < collectDistance)
                {
                    Collect(); // Call the collect function if the distance is less than collectDistance
                }
            }
        }
    }

    void Collect()
    {
        AudioSource.PlayClipAtPoint(collectSound, Camera.main.transform.position);
        Destroy(gameObject);
        ship.GetComponent<ShipController>().doHeal(10);
    }
}
