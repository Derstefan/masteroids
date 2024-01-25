using UnityEngine;

public class MineWeaponController : WeaponController
{
    public override void shoot(Vector3 pos, Quaternion direction)
    {
        createShoot(pos, direction);
    }

    public override Skill[] getWeaponSkills()
    {
        return new Skill[]{
            new UnlockSkill("Mine Weapon", 15,null,this),
            };
    }


    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Mine Weapon":
                this.activated = true;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }



}
