using UnityEngine;

public class GranadeWeaponController : WeaponController
{
    public override void shoot(Vector3 pos, Quaternion direction)
    {
        createShoot(pos, direction);

    }

    public override Skill[] getWeaponSkills()
    {
        return new Skill[]{
            new UnlockSkill("Granade Weapon", 20,null,this),
            };
    }


    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Granade Weapon":
                this.activated = true;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }



}
