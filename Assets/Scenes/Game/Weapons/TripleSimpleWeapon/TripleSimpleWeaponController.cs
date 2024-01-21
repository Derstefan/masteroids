using UnityEngine;

public class TripleSimpleWeaponController : WeaponController
{

    public override void shoot(Vector3 pos, Quaternion direction)
    {
        createShoot(pos, direction * Quaternion.Euler(0, 0, -10));
        createShoot(pos, direction);
        createShoot(pos, direction * Quaternion.Euler(0, 0, 10));
    }


    public override Skill[] getWeaponSkills()
    {
        throw new System.NotImplementedException();
    }

    public override void learnSkill(string skillName)
    {
        throw new System.NotImplementedException();
    }

}
