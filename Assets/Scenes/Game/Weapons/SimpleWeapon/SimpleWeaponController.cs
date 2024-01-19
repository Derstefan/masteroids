using UnityEngine;

public class SimpleWeaponController : WeaponController
{
    public override void shoot(Vector3 pos, Quaternion direction)
    {
        createShoot(pos, direction);
    }

}
