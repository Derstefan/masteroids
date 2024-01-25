using UnityEngine;

public class MenuAsteroidController : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(-50.0f, 2150.0f));
        GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-190.0f, 90.0f);
    }


}
