using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public GameObject projectilPrefab;
    public int numberOfSpikes = 7;


    public void CreateAndRotateSpikes()
    {
        float angleIncrement = 360f / numberOfSpikes; // Divide 360 degrees into 7 parts

        for (int i = 0; i < numberOfSpikes; i++)
        {
            // Create a new spike prefab
            GameObject spike = Instantiate(projectilPrefab, transform.position, Quaternion.identity);

            // Rotate the spike by a different angle for each iteration
            float angle = i * angleIncrement;
            spike.transform.Rotate(Vector3.forward, angle);

            // Get the ShotController component
            EnemyProjectileController shot = spike.GetComponent<EnemyProjectileController>();

            // Use the goProjectile in ShotController
            if (shot != null)
            {
                shot.goProjectile();
            }
            else
            {
                Debug.LogError("ShotController component not found on spike prefab!");
            }
        }
    }


}
