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
        return new Skill[] {
        new UnlockSkill("Triple Simple Weapon",0,null, this.sprite, this),
        new LevelingSkill("Damage Triple Simple Weapon",0,"Triple Simple Weapon", this.sprite, this),
        };
    }

    public override void learnSkill(string skillName)
    {
        switch (skillName)
        {
            case "Triple Simple Weapon":
                this.activated = true;
                break;
            case "Damage Triple Simple Weapon":
                this.damage += 10;
                break;
            default:
                throw new System.Exception("Skill not found");
        }
    }

}
