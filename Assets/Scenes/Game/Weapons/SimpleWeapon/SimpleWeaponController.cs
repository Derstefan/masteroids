using UnityEngine;

public class SimpleWeaponController : WeaponController
{

    public override void shoot(Vector3 pos, Quaternion direction)
    {
        createShoot(pos, direction);
    }



    public override Skill[] getWeaponSkills()
    {
        return new Skill[] {
        new UnlockSkill("Simple Weapon",0,null,this),
        new LevelingSkill("Damage Simple Weapon",0,"Simple Weapon",this),
        };

    }

    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Simple Weapon":
                this.activated = true;
                break;
            case "Damage Simple Weapon":
                this.damage += 10;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }

}
