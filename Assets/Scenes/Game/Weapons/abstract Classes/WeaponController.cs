using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{

    public float attackSpeed;
    public float damage;
    public float lifeTime;
    public float projectileSpeed;
    public GameObject projectilePrefab;

    abstract public void shoot(Vector3 pos, Quaternion direction);

    public void createShoot(Vector3 pos, Quaternion direction)
    {
        GameObject o = Instantiate(projectilePrefab, pos, direction);
        o.GetComponent<ProjectileController>().demage = damage;
        o.GetComponent<ProjectileController>().force = projectileSpeed;
        o.GetComponent<ProjectileController>().lifeTime = lifeTime;
        o.GetComponent<ProjectileController>().goProjectile();
    }
}
