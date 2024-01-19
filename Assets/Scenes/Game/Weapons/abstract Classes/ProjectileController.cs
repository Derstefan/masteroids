using UnityEngine;

public abstract class ProjectileController : MonoBehaviour
{
    [HideInInspector]
    public float demage = 3f;

    [HideInInspector]
    public float lifeTime = 8f;

    [HideInInspector]
    public float force = 400f;
    public abstract void goProjectile();

    public abstract void Explode();


}
