using UnityEngine;

public class FiniteMapController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

        // Teleport the game object
        if (transform.position.x > MapConfig.XMax)
        {

            transform.position = new Vector3(MapConfig.XMin, transform.position.y, 0);

        }
        else if (transform.position.x < MapConfig.XMin)
        {
            transform.position = new Vector3(MapConfig.XMax, transform.position.y, 0);
        }

        else if (transform.position.y > MapConfig.YMax)
        {
            transform.position = new Vector3(transform.position.x, MapConfig.YMin, 0);
        }

        else if (transform.position.y < MapConfig.YMin)
        {
            transform.position = new Vector3(transform.position.x, MapConfig.XMax, 0);
        }
    }
}
