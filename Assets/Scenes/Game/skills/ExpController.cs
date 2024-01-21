using UnityEngine;

public class ExpController : MonoBehaviour
{
    public GameObject ship;
    public float movementSpeed = 1f; // Adjust this value to control the movement speed
    public float minDistance = 2f;   // Adjust this value to set the minimum distance
    public float collectDistance = 0.01f; // Adjust this value to set the distance for collecting

    void FixedUpdate()
    {
        if (ship != null)
        {
            float distance = Vector3.Distance(transform.position, ship.transform.position);

            if (distance < minDistance)
            {
                // Calculate the direction to the ship
                Vector3 directionToShip = (ship.transform.position - transform.position).normalized;

                // Calculate the new position based on the movement speed
                Vector3 newPosition = transform.position + directionToShip * movementSpeed * Time.fixedDeltaTime;

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
        Destroy(gameObject);
        ship.GetComponent<ShipController>().incrementExp();
    }
}