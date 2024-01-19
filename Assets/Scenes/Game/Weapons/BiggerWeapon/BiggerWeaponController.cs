using UnityEngine;

public class BiggerWeaponController : WeaponController
{
    public override void shoot(Vector3 pos, Quaternion direction)
    {
        createShoot(pos, direction);

    }

}
