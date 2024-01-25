using UnityEngine;

public class ImplosionWeaponController : WeaponController
{
    public override void shoot(Vector3 pos, Quaternion direction)
    {
        createShoot(pos, direction);

    }

    public override Skill[] getWeaponSkills()
    {
        return new Skill[]{
            new UnlockSkill("Implosion Weapon", 30,null,this),

            };
    }


    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Implosion Weapon":
                this.activated = true;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }



}
