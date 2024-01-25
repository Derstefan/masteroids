using UnityEngine;

public class MenuFiniteMap2Controller : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

        // Teleport the game object
        if (transform.position.x > 12)
        {

            transform.position = new Vector3(-12, transform.position.y, 0);

        }
        else if (transform.position.x < -12)
        {
            transform.position = new Vector3(12, transform.position.y, 0);
        }

        else if (transform.position.y > 8)
        {
            transform.position = new Vector3(transform.position.x, -8, 0);
        }

        else if (transform.position.y < -8)
        {
            transform.position = new Vector3(transform.position.x, 8, 0);
        }
    }
}
