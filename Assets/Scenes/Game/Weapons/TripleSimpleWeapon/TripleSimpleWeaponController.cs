using UnityEngine;

public class TripleSimpleWeaponController : WeaponController
{
    public override void shoot(Vector3 pos, Quaternion direction)
    {
        createShoot(pos, direction * Quaternion.Euler(0, 0, -10));
        createShoot(pos, direction);
        createShoot(pos, direction * Quaternion.Euler(0, 0, 10));
    }

}
