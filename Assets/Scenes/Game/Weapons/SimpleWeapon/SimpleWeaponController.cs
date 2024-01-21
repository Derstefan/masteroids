using UnityEngine;

public class SimpleWeaponController : WeaponController
{

    public override void shoot(Vector3 pos, Quaternion direction)
    {
        createShoot(pos, direction);
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
